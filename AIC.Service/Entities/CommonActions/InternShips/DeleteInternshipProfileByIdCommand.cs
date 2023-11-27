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
    public class DeleteInternshipProfileByIdCommand : IRequest<bool>
    {
        [Required]
        public Guid Id { get; set; }
    }

    public class DeleteInternshipProfileByIdCommandHandler : IRequestHandler<DeleteInternshipProfileByIdCommand, bool>
    {
        private readonly IInternShipsBusiness _internshipBusiness;
        public DeleteInternshipProfileByIdCommandHandler(IInternShipsBusiness internshipBusiness)
        {
            _internshipBusiness = internshipBusiness;
        }
        public Task<bool> Handle(DeleteInternshipProfileByIdCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_internshipBusiness.DeleteById(request));
        }
    }
}
