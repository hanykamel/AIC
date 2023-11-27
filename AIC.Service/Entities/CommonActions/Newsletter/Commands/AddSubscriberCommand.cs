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
    public class AddSubscriberCommand : IRequest<bool>
    {
        public SubscriberViewModel _subscriberViewModel { get; set; }

        public AddSubscriberCommand(SubscriberViewModel subscriberViewModel)
        {
            _subscriberViewModel = subscriberViewModel;
        }
        
    }

    public class AddSubscriberHandler : IRequestHandler<AddSubscriberCommand, bool>
    {
        private readonly INewsletterBusiness _newsletterBusiness;

        public AddSubscriberHandler(INewsletterBusiness newsletterBusiness)
        {
            _newsletterBusiness = newsletterBusiness;
        }

        public async Task<bool> Handle(AddSubscriberCommand request, CancellationToken cancellationToken)
        {
            return await _newsletterBusiness.AddSubscriber(request._subscriberViewModel);
        }
    }
}
