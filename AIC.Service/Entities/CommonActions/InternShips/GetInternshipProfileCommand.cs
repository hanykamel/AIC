using AIC.Data.ViewModels;
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
    public class GetInternshipProfileCommand : IRequest<InternshipProfileViewModel>
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Date { get; set; }
        [Required]
        public string InternshipId { get; set; }
        public string lang { get; set; }
    }
    public class GetInternshipProfileHandler : IRequestHandler<GetInternshipProfileCommand, InternshipProfileViewModel>
    {
        private readonly IInternShipsBusiness _internshipBusiness;
        public GetInternshipProfileHandler(IInternShipsBusiness internshipBusiness)
        {
            _internshipBusiness = internshipBusiness;
        }

        public async Task<InternshipProfileViewModel> Handle(GetInternshipProfileCommand request, CancellationToken cancellationToken)
        {
            return await _internshipBusiness.GetProfileInternship(request);
        }
    }
}
