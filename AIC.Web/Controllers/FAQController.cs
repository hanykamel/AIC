
using AIC.SP.Middleware;
using AIC.SP.Middleware.Interfaces;
using AIC.SP.Middleware.Models;
using AIC.SP.Middleware.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using static AIC.CrossCutting.Constant.Constant;

namespace AIC.Web.Controllers
{
    public class FAQController : BaseController
    {
        private readonly IService<FAQViewModel> _faqService;
        private IConfiguration _config;

        public FAQController(IService<FAQViewModel> faqService, IConfiguration config)
        {
            _faqService = faqService;
            _config = config;
        }
        [HttpPost]
        public ActionResult<HttpResponseMessage> List([FromBody] Query query)
        {
            var items = _faqService.GetAll(query);
            return Ok(items);

        }
        


    }
}
