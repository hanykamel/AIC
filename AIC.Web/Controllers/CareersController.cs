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

    public class CareersController : BaseController
    {
        private readonly IService<CareersViewModel> _careersService;
        readonly IMediator _mediator;

        public CareersController(IService<CareersViewModel> careersService,
                                IMediator mediator)
        {
            _careersService = careersService;
            _mediator = mediator;
        }

        [HttpPost]
        public ActionResult<HttpResponseMessage> List([FromBody] Query query)
        {
            query.Lang = "";
            var items = _careersService.GetAll(query);
            return Ok(items);
        }

        [HttpGet]
        public ActionResult<HttpResponseMessage> GetById(int id)
        {
            var items = _careersService.GetById("", id);
            return Ok(items);
        }


        [HttpPost]
        public async Task<IActionResult> AddProfile([FromBody] AddUserProfileVacancyCommand addUserProfile)
        {
            addUserProfile.lang = _lang;
            var result = await _mediator.Send(addUserProfile);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetVacancyProfileCommand joinUsCommand)
        {
            joinUsCommand.Lang = _lang;
            var result = await _mediator.Send(joinUsCommand);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] AddVacancyCommand vacancyCommand)
        {
            var result = await _mediator.Send(vacancyCommand);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> ListAppliedForCareers([FromBody] ListAppliedForCareersCommand query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetProfileVersion([FromQuery] GetProfileVersionByIdCommand getProfileVersionByIdCommand)
        {
            var result = await _mediator.Send(getProfileVersionByIdCommand);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteById(DeleteVacancyProfileByIdCommand deleteVacancyProfileByIdCommand)
        {
            var result = await _mediator.Send(deleteVacancyProfileByIdCommand);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllVacanciesCommand());
            return Ok(result);
        }
    }
}
