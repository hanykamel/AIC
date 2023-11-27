using AIC.SP.Middleware.Interfaces;
using AIC.SP.Middleware.Models;
using AIC.SP.Middleware.SPViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace AIC.Web.Controllers
{
   
    public class BodAndScientificController : BaseController
    {
        private readonly IService<BodAndScientificViewModel> _bodAndScientificService;

        public BodAndScientificController(IService<BodAndScientificViewModel> bodAndScientificService)
        {
            _bodAndScientificService = bodAndScientificService;
        }

        [HttpPost]
        public ActionResult<HttpResponseMessage> List([FromBody] Query query)
        {
            var items = _bodAndScientificService.GetAll(query);
            return Ok(items);
        }

        [HttpGet]
        public ActionResult<HttpResponseMessage> Get([FromQuery] int id)
        {
            var items = _bodAndScientificService.GetById(_lang, id);
            return Ok(items);
        }
    }
}
