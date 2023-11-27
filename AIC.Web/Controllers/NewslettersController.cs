using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AIC.Data.Permissions;
using AIC.Data.ViewModels;
using AIC.Service.Entities.CommonActions.Newsletter.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIC.Web.Controllers
{
    public class NewslettersController : BaseController
    {
        readonly IMediator _mediator;
        public NewslettersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> List(GetAllSubscriberCommand getAllSubscriberCommand)
        {

            var result = await _mediator.Send(getAllSubscriberCommand);
            return Ok(result);

        }
        [HttpPost]
        public async Task<ActionResult> Unsubscribe(DeleteSubscriberCommand subscriber)
        {
            
            var result = await _mediator.Send(subscriber);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] SubscriberViewModel subscriber)
        {
            var result = await _mediator.Send(new AddSubscriberCommand(subscriber));
            return Ok(result);
        }


        [HttpPost]
        public async Task<ActionResult> Send()
        {
            var result = await _mediator.Send(new SendNewsletterCommand());
            return Ok(result);
        }



    }
}
