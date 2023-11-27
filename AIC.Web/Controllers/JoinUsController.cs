using AIC.Service.Entities.CommonActions.Join_Us;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace AIC.Web.Controllers
{
    
    public class JoinUsController : BaseController
    {
        readonly IMediator _mediator;
        public JoinUsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] AddJoinUsCommand joinUsCommand)
        {
            var result = await _mediator.Send(joinUsCommand);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetJoinUsCommand joinUsCommand)
        {
            var result = await _mediator.Send(joinUsCommand);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddProfile([FromBody] AddUserProfileCommand addUserProfile)
        {
            var result = await _mediator.Send(addUserProfile);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> ListJoinUs([FromBody] ListJoinUsCommand query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetJoinUsById([FromQuery] GetJoinUsByIdCommand getJoinUsByIdCommand)
        {
            var result = await _mediator.Send(getJoinUsByIdCommand);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteById(DeleteJoinUsByIdCommand deleteJoinUsByIdCommand)
        {
            var result = await _mediator.Send(deleteJoinUsByIdCommand);
            return Ok(result);
        }

    }
}
