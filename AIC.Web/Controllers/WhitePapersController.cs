using AIC.CrossCutting.ExceptionHandling;
using AIC.SP.Middleware.Interfaces;
using AIC.SP.Middleware.Models;
using AIC.SP.Middleware.SPViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace AIC.Web.Controllers
{
    public class WhitePapersController : BaseController
    {
        private readonly IService<WhitePaperViewModel> _whitePapersService;
        public WhitePapersController(IService<WhitePaperViewModel> whitePapersService)
        {
            _whitePapersService = whitePapersService;
        }

        [HttpPost]
        public ActionResult<HttpResponseMessage> List([FromBody] Query query)
        {
            var items = _whitePapersService.GetAll(query);
            return Ok(items);
        }

        [HttpGet]
        public ActionResult<HttpResponseMessage> GetById(int id)
        {
            var item = _whitePapersService.GetById(_lang, id);
            if (item == null)
                throw new NotFoundException("Not Found");
            return Ok(item);
        }
    }
}
