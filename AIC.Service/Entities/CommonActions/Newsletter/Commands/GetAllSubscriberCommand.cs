using AIC.CrossCutting.Models.MvcModels;
using AIC.Data.ViewModels;
using AIC.Data.ViewModels.Lookups;
using AIC.Service.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AIC.Service.Entities.CommonActions.Newsletter.Commands
{
   public class GetAllSubscriberCommand : ListQuery, IRequest<ListSubscribersViewModel>
    {
        
    }

    public class GetAllSubscriberHandler : IRequestHandler<GetAllSubscriberCommand, ListSubscribersViewModel>
    {
        private readonly INewsletterBusiness _newsletterBusiness;

        public GetAllSubscriberHandler(INewsletterBusiness newsletterBusiness)
        {
            _newsletterBusiness = newsletterBusiness;
        }

        public async Task<ListSubscribersViewModel> Handle(GetAllSubscriberCommand request, CancellationToken cancellationToken)
        {
            var result = _newsletterBusiness.Get(request);
            return await Task.FromResult(result);

        }
    }
}
