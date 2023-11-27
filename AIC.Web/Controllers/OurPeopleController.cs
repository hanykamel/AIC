using AIC.SP.Middleware.Interfaces;
using AIC.SP.Middleware.Models;
using AIC.SP.Middleware.SPViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace AIC.Web.Controllers
{
    public class OurPeopleController : BaseController
    {
        private readonly IService<OurPeopleViewModel> _ourPeopleService;

        public OurPeopleController(IService<OurPeopleViewModel> ourPeopleService)
        {
            _ourPeopleService = ourPeopleService;
        }

        [HttpPost]
        public ActionResult<HttpResponseMessage> List([FromBody] Query query)
        {
            var items = _ourPeopleService.GetAll(query);
            return Ok(items);
        }
        [HttpGet]
        public ActionResult<HttpResponseMessage> Get([FromQuery] int id)
        {
            var items = _ourPeopleService.GetById(_lang,id);
            return Ok(items);
        }
    }
}
