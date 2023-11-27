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
    public class AddUserProfileVacancyCommand : IRequest<bool>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public int VacancyId { get; set; }
        public string lang { get; set; }

    }
    public class AddUserProfileVacancyHandler : IRequestHandler<AddUserProfileVacancyCommand, bool>
    {
        private readonly IVacancyBusiness _vacancyBusiness;
        public AddUserProfileVacancyHandler(IVacancyBusiness vacancyBusiness)
        {
            _vacancyBusiness = vacancyBusiness;
        }

        public async Task<bool> Handle(AddUserProfileVacancyCommand request, CancellationToken cancellationToken)
        {
            return await _vacancyBusiness.AddUserProfile(request);
        }
    }
}
