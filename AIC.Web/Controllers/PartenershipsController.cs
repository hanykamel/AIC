using AIC.CrossCutting.ExceptionHandling;
using AIC.SP.Middleware;
using AIC.SP.Middleware.Interfaces;
using AIC.SP.Middleware.Models;
using AIC.SP.Middleware.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using static AIC.CrossCutting.Constant.Constant;

namespace AIC.Web.Controllers
{
    public class PartenershipsController : BaseController
    {
        private readonly IService<PartenershipsViewModel> _partenershipsService;

        public PartenershipsController(IService<PartenershipsViewModel> partenershipsService)
        {
            _partenershipsService = partenershipsService;
        }

        [HttpPost]
        public ActionResult<HttpResponseMessage> List([FromBody] Query query)
        {
            var items = _partenershipsService.GetAll(query);
            return Ok(items);
        }

    }
}
