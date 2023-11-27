using AIC.Service.Entities.CommonActions.Commands;
using AIC.Service.Entities.CommonActions.ContactUs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AIC.Web.Controllers
{

    public class RecaptchaController : BaseController
    {
        readonly IMediator _mediator;

        public RecaptchaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> ValidateRecaptcha([FromBody] ValidateRecaptchaCommand validateRecaptchaCommand)
        {
            var result = await _mediator.Send(validateRecaptchaCommand);
            return Ok(result);
        }
    }
}
