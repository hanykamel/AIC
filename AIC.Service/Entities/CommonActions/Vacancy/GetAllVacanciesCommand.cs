using AIC.Data.ViewModels;
using AIC.Service.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AIC.Service.Entities.CommonActions.Vacancy
{
    public class GetAllVacanciesCommand : IRequest<List<VacancyItemViewModel>>
    {

    }
    public class GetAllVacanciesCommandHandler : IRequestHandler<GetAllVacanciesCommand, List<VacancyItemViewModel>>
    {
        private readonly IVacancyBusiness _vacancyBusiness;
        public GetAllVacanciesCommandHandler(IVacancyBusiness vacancyBusiness)
        {
            _vacancyBusiness = vacancyBusiness;
        }
        public Task<List<VacancyItemViewModel>> Handle(GetAllVacanciesCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_vacancyBusiness.GetAll(request));
        }
    }
}
