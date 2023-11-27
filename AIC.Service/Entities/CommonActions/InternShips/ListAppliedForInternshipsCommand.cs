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

namespace AIC.Service.Entities.CommonActions.InternShips
{
    public class ListAppliedForInternshipsCommand : ListQuery, IRequest<AppliedForInternshipsListViewModel>
    {
    }

    public class ListAppliedForInternshipsCommandHandler : IRequestHandler<ListAppliedForInternshipsCommand, AppliedForInternshipsListViewModel>
    {
        private readonly IInternShipsBusiness _internshipBusiness;
        public ListAppliedForInternshipsCommandHandler(IInternShipsBusiness internshipBusiness)
        {
            _internshipBusiness = internshipBusiness;
        }
        public Task<AppliedForInternshipsListViewModel> Handle(ListAppliedForInternshipsCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_internshipBusiness.ListAppliedForInternships(request));
        }
    }
}
