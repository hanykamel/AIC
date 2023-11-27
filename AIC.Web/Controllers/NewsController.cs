using AIC.CrossCutting.Constant;
using AIC.SP.Middleware;
using AIC.SP.Middleware.Interfaces;
using AIC.SP.Middleware.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace AIC.Web.Controllers
{
    public class NewsController : BaseController
    {
        private readonly IService<NewsViewModel> _newsService;

        public NewsController(IService<NewsViewModel> newsService)
        {
            _newsService = newsService;
        }
        [HttpPost]
        public ActionResult<HttpResponseMessage> List([FromBody] Query query)
        {
            //query.Filters.Add(new Filter
            //{
            //    Field = Constant.Fields.IsActive,
            //    Operator = "Eq",
            //    Value = "Active",
            //    FieldValueType = "Text"
            //});
            query.Filters.Add(new Filter
            {
                Field = Constant.Fields.IsActive,
                Operator = "Geq",
                Value = "<Today />",
                FieldValueType = "DateTime"
            });
            var items = _newsService.GetAll(query);

            return Ok(items);
        }

        [HttpGet]
        public ActionResult<HttpResponseMessage> GetById(int id)
        {
            var items = _newsService.GetById(_lang, id);
            return Ok(items);
        }
        [HttpPost]
        public ActionResult<HttpResponseMessage> GetArchivedList([FromBody] Query query)
        {
            //query.Filters.Add(new Filter
            //{
            //    Field = Constant.Fields.IsActive,
            //    Operator = "Eq",
            //    Value = "Not",
            //    FieldValueType = "Text"
            //});
            query.Filters.Add(new Filter
            {
                Field = Constant.Fields.IsActive,
                Operator = "Lt",
                Value = "<Today />",
                FieldValueType = "DateTime"
            });
            var items = _newsService.GetAll(query);
            return Ok(items);
        }
    }
}
