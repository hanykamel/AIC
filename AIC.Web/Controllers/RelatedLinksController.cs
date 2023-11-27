using AIC.SP.Middleware.Interfaces;
using AIC.SP.Middleware.SPViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;

namespace AIC.Web.Controllers
{

    public class RelatedLinksController : BaseController
    {
        private readonly IService<MediaLinksViewModel> _mediaLinksService;

        public RelatedLinksController(IService<MediaLinksViewModel> mediaLinksService)
        {
            _mediaLinksService = mediaLinksService;
        }

        [HttpPost]
        public ActionResult<HttpResponseMessage> ListByIds([FromBody] int[] ids)
        {
            var links = new List<MediaLinksViewModel>();
            foreach (var id in ids)
            {
                links.Add(_mediaLinksService.GetById("", id));
            }
            return Ok(links);
        }
    }
}
