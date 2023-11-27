using AIC.Data.Enums;
using AIC.Service.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AIC.Service.Entities.CommonActions.ContactUs
{
    public class SendReplysCommand : IRequest<bool>
    {
        public ContactUsFormsEnum Type { get; set; }
    }

    public class SendReplysCommandHandler : IRequestHandler<SendReplysCommand, bool>
    {
        private readonly IContactUsBusiness _contactUsBusiness;
        public SendReplysCommandHandler(IContactUsBusiness contactUsBusiness)
        {
            _contactUsBusiness = contactUsBusiness;
        }
        public async Task<bool> Handle(SendReplysCommand request, CancellationToken cancellationToken)
        {
            return await _contactUsBusiness.SendReplys(request);
        }
    }
}
