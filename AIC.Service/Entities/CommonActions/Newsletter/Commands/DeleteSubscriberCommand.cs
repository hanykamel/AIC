using AIC.Data.ViewModels;
using AIC.Data.ViewModels.Lookups;
using AIC.Service.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AIC.Service.Entities.CommonActions.Newsletter.Commands
{
    public class DeleteSubscriberCommand : IRequest<bool>
    {
        public string  Email { get; set; }
        
    }

    public class DeleteSubscriberHandler : IRequestHandler<DeleteSubscriberCommand, bool>
    {
        private readonly INewsletterBusiness _newsletterBusiness;

        public DeleteSubscriberHandler(INewsletterBusiness newsletterBusiness)
        {
            _newsletterBusiness = newsletterBusiness;
        }
        public async Task<bool> Handle(DeleteSubscriberCommand request, CancellationToken cancellationToken)
        {
            return await _newsletterBusiness.UnSubscrib(request.Email);
        }


    }
}
