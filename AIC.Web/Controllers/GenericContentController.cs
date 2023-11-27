using AIC.CrossCutting.ExceptionHandling;
using AIC.SP.Middleware.Interfaces;
using AIC.SP.Middleware.Models;
using AIC.SP.Middleware.SPViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using static AIC.CrossCutting.Constant.Constant;

namespace AIC.Web.Controllers
{
    public class GenericContentController : BaseController
    {
        private readonly IService<GenericContentViewModel> _genericContentTypeService;

        public GenericContentController(IService<GenericContentViewModel> genericContentTypeService)
        {
            _genericContentTypeService = genericContentTypeService;
        }

        [HttpGet]
        public ActionResult<HttpResponseMessage> Get([FromQuery] string Type)
        {
            Query query = new Query() { Lang = _lang, PageSize = 1 };
            query.Filters = new List<Filter>(){
                new Filter() { Field = Fields.GenericContentType, FieldValueType = "Lookup", Operator = "Eq", Value = Type }};
            var items = _genericContentTypeService.GetAll(query);
            if (items == null || items.Items.Count == 0)
                throw new NotFoundException("Not Found");
            return Ok(items);
        }

    }
}
