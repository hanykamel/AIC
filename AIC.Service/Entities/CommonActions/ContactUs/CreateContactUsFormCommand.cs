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
    public class CreateContactUsFormCommand : IRequest<bool>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public int TypeId { get; set; }
        public string WorkEducationalOrganization { get; set; }
        public string Message { get; set; }
    }

    public class CreateContactUsFormCommandHandler : IRequestHandler<CreateContactUsFormCommand, bool>
    {
        private readonly IContactUsBusiness _contactUsBusiness;
        public CreateContactUsFormCommandHandler(IContactUsBusiness contactUsBusiness)
        {
            _contactUsBusiness = contactUsBusiness;
        }
        public async Task<bool> Handle(CreateContactUsFormCommand request, CancellationToken cancellationToken)
        {
            return await _contactUsBusiness.CreateContactUsForm(request);
        }
    }
}
