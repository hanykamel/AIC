using AIC.Service.Entities.CommonActions.InternShips;
using AIC.Service.Entities.CommonActions.Vacancy;
using AIC.SP.Middleware.Interfaces;
using AIC.SP.Middleware.Models;
using AIC.SP.Middleware.SPViewModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace AIC.Web.Controllers
{
    
    public class InternshipsController : BaseController
    {
        private readonly IService<InternshipsViewModel> _internshipsService;
        readonly IMediator _mediator;
        public InternshipsController(IService<InternshipsViewModel> internshipsService,
                                       IMediator mediator)
        {
            _internshipsService = internshipsService;
            _mediator = mediator;
        }

        [HttpPost]
        public ActionResult<HttpResponseMessage> List([FromBody] Query query)
        {
            query.Lang = "";
            var items = _internshipsService.GetAll(query);
            return Ok(items);
        }

        [HttpGet]
        public ActionResult<HttpResponseMessage> GetById(int id)
        {
            var items = _internshipsService.GetById("", id);
            return Ok(items);
        }
        [HttpPost]
        public async Task<IActionResult> AddProfile([FromBody] AddUserProfileInternshipCommand addUserProfileInternship)
        {
            addUserProfileInternship.lang = _lang;
            var result = await _mediator.Send(addUserProfileInternship);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetInternshipProfileCommand command)
        {
            command.lang = _lang;
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] AddIntershipsCommand intershipsCommand)
        {
            var result = await _mediator.Send(intershipsCommand);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> ListAppliedForInternships([FromBody] ListAppliedForInternshipsCommand query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetInternshipProfileById([FromQuery] GetInternshipProfileByIdCommand getInternshipProfileByIdCommand)
        {
            var result = await _mediator.Send(getInternshipProfileByIdCommand);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteById(DeleteInternshipProfileByIdCommand deleteInternshipProfileByIdCommand)
        {
            var result = await _mediator.Send(deleteInternshipProfileByIdCommand);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllInternshipsCommand());
            return Ok(result);
        }
    }
}
