using AIC.Service.Entities.CommonActions.Synchronization;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AIC.Web.Controllers
{
   
    public class SynchronizationController : BaseController
    {
        readonly IMediator _mediator;
        public SynchronizationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> SynchronizToDB([FromBody] SychronizeCommand sychronizeCommand)
        {
            var result = await _mediator.Send(sychronizeCommand);
            return Ok(result);
        }
    }
}
