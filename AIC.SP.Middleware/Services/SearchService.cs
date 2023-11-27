
using AIC.SP.Middleware.Helpers;
using AIC.SP.Middleware.Interfaces;
using AIC.SP.Middleware.Mapper;
using AutoMapper;
using NinjaNye.SearchExtensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Caching;
using System.Text;

namespace AIC.SP.Middleware.Services
{
    public class SearchService : ISearchService
    {
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        private static MemoryCache _cache = MemoryCache.Default;
        public SearchService(ICacheService cacheService)
        {
            _mapper = AutoMapperConfig.Mapper;
            _cacheService = cacheService;
        }
        public CacheListViewModel Search(string lang, string key, string category, DateTime? from, DateTime? to, int pageIndex = 0, int pageSize = 9)
        {
            IEnumerable<CacheViewModel> result = GetCacheData(key, lang);
            result = result.Where(c => (string.IsNullOrEmpty(category) || c.ListName.ToLower() == category.ToLower()) && (c.Date == DateTime.MinValue || (from == null || c.Date >= from)) && (c.Date == DateTime.MinValue || (to == null || c.Date <= to))).ToList();
            //result = result.Select(x => {x.Description =  RemoveSympolsFromStringHelper.StringHandler(x.Description); return x; });
            if (!string.IsNullOrWhiteSpace(key))
            {
                return Search(key, pageIndex, pageSize, result);
            }
            return new CacheListViewModel { Records = result.Skip(pageIndex * pageSize).Take(pageSize).ToList(), TotalRecordCount = result.Count() };
        }
        private IEnumerable<CacheViewModel> GetCacheData(string key,string lang)
        {
            IEnumerable<CacheViewModel> result = null;
            if (string.IsNullOrWhiteSpace(key))
            {
                 result = _cache.Get("CacheData" + lang.ToUpper()) as IEnumerable<CacheViewModel>;
                if (result == null)
                {
                    _cacheService.RefreshCache();
                    result = _cache.Get("CacheData" + lang.ToUpper()) as List<CacheViewModel>;
                }
            }
            else
            {
                 result = _cache.Get("AllCacheData") as IEnumerable<CacheViewModel>;
                if (result == null)
                {
                    _cacheService.RefreshCache();
                    result = _cache.Get("AllCacheData") as List<CacheViewModel>;
                }
            }
            return result;
        }
        private CacheListViewModel Search(string key, int pageIndex, int pageSize, IEnumerable<CacheViewModel> result)
        {
            CacheListViewModel searchobj = new CacheListViewModel();
            var keyWords = RemoveDiacritics(key).Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            var searchRes = result.Search(x => RemoveDiacritics(x.Title),
                x => RemoveDiacritics(x.Brief),
                x => RemoveDiacritics(x.DomainName),
                x => RemoveDiacritics(x.CategoryName),
                x => RemoveDiacritics(x.Description),
                x => RemoveDiacritics(x.DemoBrief)).ContainingAll(keyWords).ToRanked().OrderByDescending(r => r.Item.Date);
            var count = searchRes.Count();
            if (searchRes != null)
            {
                searchobj.Records = _mapper.Map<List<CacheViewModel>>(searchRes.Skip(pageIndex * pageSize).Take(pageSize).ToList());
                searchobj.TotalRecordCount = count;
            }
            return searchobj;
        }
        private string RemoveDiacritics(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return key;
            key = key.Replace('ي', 'ى').Replace('ة', 'ه');
            key = key.Normalize(NormalizationForm.FormD);
            var chars = key.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
            return new string(chars).Normalize(NormalizationForm.FormC).ToLower();
        }
    }
}
