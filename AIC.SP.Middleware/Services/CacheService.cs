using AIC.CrossCutting.Constant;
using AIC.SP.Middleware.Interfaces;
using AIC.SP.Middleware.Mapper;
using AIC.SP.Middleware.Models;
using AIC.SP.Middleware.SPViewModels;
using Asset.SharePoint.Middleware.Helpers;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Xml;

namespace AIC.SP.Middleware.Services
{
    public class CacheService : ICacheService
    {
        private static MemoryCache _cache = MemoryCache.Default;
        private readonly IMapper _mapper;
        private readonly IService<ProjectsViewModel> _projectsService;
        private readonly IService<HPCProjectsViewModel> _hpcProjectsService;
        private readonly IService<NewsViewModel> _newsService;
        private readonly IService<EventsViewModel> _eventsService;
        private readonly IService<CareersViewModel> _careersService;
        private readonly IService<InternshipsViewModel> _internshipsService;
        private readonly IService<WhitePaperViewModel> _whitePapersService;
        private readonly IService<BestPracticeViewModel> _bestPractiseService;
        private readonly IService<AICMaterialsViewModel> _aicMaterialsService;
        private readonly IService<BodAndScientificViewModel> _bodAndScientificService;
        private readonly IService<OurPeopleViewModel> _ourPeopleService;
        private readonly IService<PhotoAlbumViewModel> _photoAlbumService;
        private readonly IService<VideoAlbumViewModel> _videoAlbumService;
        private readonly IService<GenericContentViewModel> _genericContentService;
        private readonly IService<ApplicationsDomainsViewModel> _applicationDomainService;
        private readonly IService<TechnologyDomainsViewModel> _technologyDomainService;
        private List<CareersViewModel> _careersItems = new List<CareersViewModel>();
        private List<InternshipsViewModel> _internShipsItems = new List<InternshipsViewModel>();

        public CacheService(
            IService<ProjectsViewModel> projectsService, IService<HPCProjectsViewModel> hpcProjectsService,
            IService<NewsViewModel> newsService, IService<EventsViewModel> eventsService,
            IService<CareersViewModel> careersService, IService<InternshipsViewModel> internshipsService,
            IService<WhitePaperViewModel> whitePapersService,
            IService<BestPracticeViewModel> bestPractiseService,
            IService<BodAndScientificViewModel> bodAndScientificService,
            IService<OurPeopleViewModel> ourPeopleService,
            IService<PhotoAlbumViewModel> photoAlbumService,
            IService<VideoAlbumViewModel> videoAlbumService,
            IService<AICMaterialsViewModel> aicMaterialsService,
            IService<GenericContentViewModel> genericContentService,
            IService<ApplicationsDomainsViewModel> applicationDomainService,
            IService<TechnologyDomainsViewModel> technologyDomainService
             )
        {
            _mapper = AutoMapperConfig.Mapper;
            _projectsService = projectsService;
            _hpcProjectsService = hpcProjectsService;
            _newsService = newsService;
            _eventsService = eventsService;
            _careersService = careersService;
            _internshipsService = internshipsService;
            _whitePapersService = whitePapersService;
            _bestPractiseService = bestPractiseService;
            _bodAndScientificService = bodAndScientificService;
            _ourPeopleService = ourPeopleService;
            _photoAlbumService = photoAlbumService;
            _videoAlbumService = videoAlbumService;
            _aicMaterialsService = aicMaterialsService;
            _genericContentService = genericContentService;
            _applicationDomainService = applicationDomainService;
            _technologyDomainService = technologyDomainService;
            RegisterRootLists();
        }
        public void RefreshCache()
        {
            try
            {
                GetCache();
            }
            catch (Exception e)
            {
                throw e;
            }

        }


        private List<CacheViewModel> GetCacheListsData(string lang)
        {
            List<CacheViewModel> cache = new List<CacheViewModel>();
            Query query = new Query() { Lang = lang, PageSize = 0 };
            Query queryMediaAlbum = new Query() { Lang = lang, PageSize = 0 };
            queryMediaAlbum.Filters.Add(new Filter() { Field = "FSObjType", FieldValueType = "Integer", Operator = "Eq", Value = "1" });
            //getProjects
            cache.AddRange(_mapper.Map<List<CacheViewModel>>(_projectsService.GetAll(query).Items));
            //getHPCProjects
            cache.AddRange(_mapper.Map<List<CacheViewModel>>(_hpcProjectsService.GetAll(query).Items));
            //getNews
            cache.AddRange(_mapper.Map<List<CacheViewModel>>(_newsService.GetAll(query).Items));
            //getEvents
            cache.AddRange(_mapper.Map<List<CacheViewModel>>(_eventsService.GetAll(query).Items));
            //getCareers
            cache.AddRange(lang == "ar" ? _mapper.Map<List<CacheViewModel>>(_mapper.Map<List<CareersViewModelAR>>(_careersItems)) : _mapper.Map<List<CacheViewModel>>(_mapper.Map<List<CareersViewModelEN>>(_careersItems)));
            //getInternships
            cache.AddRange(lang == "ar" ? _mapper.Map<List<CacheViewModel>>(_mapper.Map<List<InternShipsViewModelAR>>(_internShipsItems)) : _mapper.Map<List<CacheViewModel>>(_mapper.Map<List<InternShipsViewModelEN>>(_internShipsItems)));
            //getWhitePapers
            cache.AddRange(_mapper.Map<List<CacheViewModel>>(_whitePapersService.GetAll(query).Items));
            //getBestPractise
            cache.AddRange(_mapper.Map<List<CacheViewModel>>(_bestPractiseService.GetAll(query).Items));
            //getBodAndScientifiv
            cache.AddRange(_mapper.Map<List<CacheViewModel>>(_bodAndScientificService.GetAll(query).Items));
            //getOurPeople
            cache.AddRange(_mapper.Map<List<CacheViewModel>>(_ourPeopleService.GetAll(query).Items));
            //getAicMaterials
            cache.AddRange(_mapper.Map<List<CacheViewModel>>(_aicMaterialsService.GetAll(query).Items));
            //getAboutUs
            cache.AddRange(_mapper.Map<List<CacheViewModel>>(_genericContentService.GetAll(query).Items));
            //getPhotoAlbum
            cache.AddRange(_mapper.Map<List<CacheViewModel>>(_photoAlbumService.GetAllDocuments(queryMediaAlbum).Items));
            //getVideoAlbum
            cache.AddRange(_mapper.Map<List<CacheViewModel>>(_videoAlbumService.GetAllDocuments(queryMediaAlbum).Items));
            //getApplicationDomain
            var applicationDomains = _applicationDomainService.GetAll(new Query()).Items;
            //getTechnologyDomain
            var technologyDomains = _technologyDomainService.GetAll(new Query()).Items;
            if (lang == "ar")
            {
                applicationDomains = applicationDomains.Select(a => { a.Title = a.TitleAr; return a; }).ToList();
                technologyDomains = technologyDomains.Select(a => { a.Title = a.TitleAr; return a; }).ToList();
            }
            cache.AddRange(_mapper.Map<List<CacheViewModel>>(applicationDomains));
            cache.AddRange(_mapper.Map<List<CacheViewModel>>(technologyDomains));
            cache = cache.Select(c => { c.Lang = lang; return c; }).ToList();
            return cache.OrderByDescending(x => x.Date).ToList();
        }

        private void GetCache()
        {
            CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
            cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddDays(1);
            if (_cache.Contains("CacheDataEN"))
            {
                _cache.Remove("CacheDataEN");
            }
            if (_cache.Contains("CacheDataAR"))
            {
                _cache.Remove("CacheDataAR");
            }
            List<CacheViewModel> cacheAr = GetCacheListsData("ar");
            List<CacheViewModel> cacheEn = GetCacheListsData("en");
            List<CacheViewModel> allCache = cacheAr.Concat(cacheEn).ToList();

            _cache.Add("CacheDataEN", cacheEn, cacheItemPolicy);
            _cache.Add("CacheDataAR", cacheAr, cacheItemPolicy);
            _cache.Add("AllCacheData", allCache, cacheItemPolicy);

        }

        public void RegisterRootLists()
        {
            Query queryRootLists = new Query() { PageSize = 0 };
            _careersItems = _careersService.GetAll(queryRootLists).Items;
            _internShipsItems = _internshipsService.GetAll(queryRootLists).Items;
        }

    }
}
