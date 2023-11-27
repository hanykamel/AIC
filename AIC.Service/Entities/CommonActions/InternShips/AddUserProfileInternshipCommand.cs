using AIC.Service.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AIC.Service.Entities.CommonActions.InternShips
{
    public class AddUserProfileInternshipCommand : IRequest<bool>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public int InternShipId { get; set; }
        public string lang { get; set; }
    }
    public class AddUserProfileInternshipHandler : IRequestHandler<AddUserProfileInternshipCommand, bool>
    {
        private readonly IInternShipsBusiness _internshipBusiness;
        public AddUserProfileInternshipHandler(IInternShipsBusiness internshipBusiness)
        {
            _internshipBusiness = internshipBusiness;
        }
        public async Task<bool> Handle(AddUserProfileInternshipCommand request, CancellationToken cancellationToken)
        {
            return await _internshipBusiness.AddUserProfile(request);
        }
    }
}
