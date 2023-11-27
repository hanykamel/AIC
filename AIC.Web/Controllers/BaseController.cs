using AIC.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIC.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ServiceFilter(typeof(LanguageFilter))]
    [ServiceFilter(typeof(ModelStateFilter))]
    public class BaseController : ControllerBase
    {
        public string _lang { get { return GetLanguage(); } }

        [HttpGet]
        public string GetLanguage()

        {
            if (Request != null && Request.Headers != null)
            {
                Request.Headers.TryGetValue("lang", out var lang);
                return lang;
            }
            return string.Empty;
        }
    }
}
