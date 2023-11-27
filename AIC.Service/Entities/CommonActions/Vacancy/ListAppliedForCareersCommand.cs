using AIC.CrossCutting.Models.MvcModels;
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
    public class ListAppliedForCareersCommand : ListQuery, IRequest<ListAppliedForCareersListViewModel>
    {

    }

    public class ListAppliedForCareersCommandHandler : IRequestHandler<ListAppliedForCareersCommand, ListAppliedForCareersListViewModel>
    {
        private readonly IVacancyBusiness _vacancyBusiness;
        public ListAppliedForCareersCommandHandler(IVacancyBusiness vacancyBusiness)
        {
            _vacancyBusiness = vacancyBusiness;
        }
        public Task<ListAppliedForCareersListViewModel> Handle(ListAppliedForCareersCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_vacancyBusiness.ListAppliedForCareers(request));
        }
    }
}
