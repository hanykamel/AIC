using AIC.Service.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AIC.Service.Entities.CommonActions.Vacancy
{
    public class DeleteVacancyProfileByIdCommand : IRequest<bool>
    {
        [Required]
        public Guid Id { get; set; }
    }

    public class DeleteVacancyProfileByIdCommandHandler : IRequestHandler<DeleteVacancyProfileByIdCommand, bool>
    {
        private readonly IVacancyBusiness _vacancyBusiness;
        public DeleteVacancyProfileByIdCommandHandler(IVacancyBusiness vacancyBusiness)
        {
            _vacancyBusiness = vacancyBusiness;
        }
        public Task<bool> Handle(DeleteVacancyProfileByIdCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_vacancyBusiness.DeleteById(request));
        }
    }
}
