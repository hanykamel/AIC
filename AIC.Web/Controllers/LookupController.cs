using AIC.SP.Middleware.Interfaces;
using AIC.SP.Middleware.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MediatR;
using AIC.Data.ViewModels.Lookups;
using AIC.Service.Entities;
using AIC.Service.Entities.CommonActions.RequestsLookup;
using AIC.SP.Middleware.SPViewModels;

namespace AIC.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    public class LookupController : ControllerBase
    {
        readonly IMediator _mediator;
        private readonly IService<CountryViewModel> _countryService;
        private readonly IService<CityViewModel> _cityService;

        public LookupController(
            IMediator mediator, IService<CountryViewModel> countryService, IService<CityViewModel> cityService)
        {
            _mediator = mediator;
            _countryService = countryService;
            _cityService = cityService;
        }

        //[HttpPost]
        //public ActionResult<HttpResponseMessage> ListGovernorates([FromBody] Query query)
        //{
        //    return Ok("");
        //}

        [HttpGet]
        public async Task<ActionResult> DegreeLevels()
        {
            var result = await _mediator.Send(new ListDegreeLevelCommand());
            return Ok(result);
        }
        [HttpGet]
        public async Task<ActionResult> JobTypes()
        {
            var result = await _mediator.Send(new ListJobTypesCommand());
            return Ok(result);
        }
        [HttpGet]
        public ActionResult<HttpResponseMessage> Countries()
        {
            var result = _countryService.GetAll(new Query()).Items;
            return Ok(result);
        }

        [HttpGet]
        public ActionResult<HttpResponseMessage> Cities()
        {
            var result = _cityService.GetAll(new Query()).Items;
            return Ok(result);
        }

    }
}
