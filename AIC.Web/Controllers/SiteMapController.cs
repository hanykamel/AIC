using AIC.SP.Middleware.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AIC.Web.Controllers
{
    [AllowAnonymous]
    public class SiteMapController : BaseController
    {
        private readonly ISitemapService _sitemapService;
        public SiteMapController(ISitemapService sitemapService)
        {
            _sitemapService = sitemapService;
        }
        [HttpGet]
        public ActionResult<HttpResponseMessage> GetSiteMap()
        {
            
                string result = _sitemapService.GetSiteMap(_lang);
            return Ok(result);
           
        }
    }
}
