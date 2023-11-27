using AIC.SP.Middleware.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using AIC.Data.Permissions;
using AIC.SP.Middleware;

namespace AIC.Web.Controllers
{
    public class SearchController : BaseController
    {
        private ISearchService _searchService;
        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }
        [HttpGet]
        public ActionResult<HttpResponseMessage> Search(string key, string category, DateTime? from = null, DateTime? to = null, int pageIndex = 0, int pageSize = 9)
        {
                CacheListViewModel result = _searchService.Search(_lang, key, category, from, to, pageIndex, pageSize);
                return Ok(result);
        }
    }
}
