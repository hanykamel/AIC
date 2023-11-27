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

namespace AIC.Service.Entities.CommonActions.Vacancy
{
    public class GetProfileVersionByIdCommand : IRequest<VacancyViewModel>
    {
        [Required]
        public Guid Id { get; set; }
    }

    public class GetProfileVersionByIdCommandHandler : IRequestHandler<GetProfileVersionByIdCommand, VacancyViewModel>
    {
        private readonly IVacancyBusiness _vacancyBusiness;
        public GetProfileVersionByIdCommandHandler(IVacancyBusiness vacancyBusiness)
        {
            _vacancyBusiness = vacancyBusiness;
        }
        public Task<VacancyViewModel> Handle(GetProfileVersionByIdCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_vacancyBusiness.GetProfileVersionById(request));
        }
    }
}
