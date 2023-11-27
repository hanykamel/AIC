using AIC.Service.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AIC.Service.Entities.CommonActions.Newsletter.Commands
{
    public class SendNewsletterCommand :IRequest<bool>
    {
    }

    public class SendNewsletterHandler : IRequestHandler<SendNewsletterCommand, bool>
    {
        private readonly INewsletterBusiness _newsletterBusiness;

        public SendNewsletterHandler(INewsletterBusiness newsletterBusiness)
        {
            _newsletterBusiness = newsletterBusiness;
        }

        public async Task<bool> Handle(SendNewsletterCommand request, CancellationToken cancellationToken)
        {
            return await _newsletterBusiness.SendNewsLetter();
        }
    }
}
