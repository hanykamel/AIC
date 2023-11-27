using AIC.Service.Entities.CommonActions.ContactUs;
using AIC.SP.Middleware.Interfaces;
using AIC.SP.Middleware.Models;
using AIC.SP.Middleware.SPViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace AIC.Web.Controllers
{
    public class ContactUsController : BaseController
    {
        private readonly IService<SocialMediaViewModel> _socialMediaService;
        private readonly IService<ContactUsViewModel> _contactUsService;
        readonly IMediator _mediator;

        public ContactUsController(IService<SocialMediaViewModel> socialMediaService, IService<ContactUsViewModel> contactUsService,
                IMediator mediator)
        {
            _socialMediaService = socialMediaService;
            _contactUsService = contactUsService;
            _mediator = mediator;
        }


        [HttpGet]
        public ActionResult<HttpResponseMessage> GetSocialMedia()
        {
            var items = _socialMediaService.GetAll(new Query());
            return Ok(items);
        }

        [HttpGet]
        public ActionResult<HttpResponseMessage> GetContactUs()
        {
            var items = _contactUsService.GetAll(new Query());
            return Ok(items);
        }

        [HttpPost]
        public async Task<ActionResult> SendReplys(SendReplysCommand sendReplysCommand)
        {
            var result = await _mediator.Send(sendReplysCommand);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateContactUsForm(CreateContactUsFormCommand createContactUsFormCommand)
        {
            var result = await _mediator.Send(createContactUsFormCommand);
            return Ok(result);
        }

    }
}
