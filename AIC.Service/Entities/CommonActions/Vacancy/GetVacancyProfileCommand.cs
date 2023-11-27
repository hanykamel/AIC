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
    public class GetVacancyProfileCommand : IRequest<VacancyViewModel>
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Date { get; set; }
        [Required]
        public string VacancyId { get; set; }
        public string Lang { get; set; }
    }
    public class GetVacancyProfileHandler : IRequestHandler<GetVacancyProfileCommand, VacancyViewModel>
    {
        private readonly IVacancyBusiness _vacancyBusiness;
        public GetVacancyProfileHandler(IVacancyBusiness vacancyBusiness)
        {
            _vacancyBusiness = vacancyBusiness;
        }
        public async Task<VacancyViewModel> Handle(GetVacancyProfileCommand request, CancellationToken cancellationToken)
        {
            return await _vacancyBusiness.GetProfileVacancy(request);
        }
    }

}
