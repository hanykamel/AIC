using AIC.SP.Middleware.Interfaces;
using AIC.SP.Middleware.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AIC.Web.Controllers
{
    public class CacheController : BaseController
    {
        public ICacheService _cacheService { get; set; }
        public CacheController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
        [HttpPost]
        public ActionResult<HttpResponseMessage> Cache()
        {
            _cacheService.RefreshCache();
            return Ok();
        }
    }
}
