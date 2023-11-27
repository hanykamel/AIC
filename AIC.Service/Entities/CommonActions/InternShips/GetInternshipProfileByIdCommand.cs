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
    public class GetInternshipProfileByIdCommand : IRequest<InternshipProfileViewModel>
    {
        [Required]
        public Guid Id { get; set; }
    }

    public class GetInternshipProfileByIdCommandHandler : IRequestHandler<GetInternshipProfileByIdCommand, InternshipProfileViewModel>
    {
        private readonly IInternShipsBusiness _internshipBusiness;
        public GetInternshipProfileByIdCommandHandler(IInternShipsBusiness internshipBusiness)
        {
            _internshipBusiness = internshipBusiness;
        }

        public Task<InternshipProfileViewModel> Handle(GetInternshipProfileByIdCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_internshipBusiness.GetInternshipProfileById(request));
        }
    }
}
