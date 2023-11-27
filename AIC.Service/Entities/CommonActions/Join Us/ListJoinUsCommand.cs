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

namespace AIC.Service.Entities.CommonActions.Join_Us
{
    public class ListJoinUsCommand : ListQuery, IRequest<JoinUsApplicationListViewModel>
    {
    }

    public class ListJoinUsCommandHandler : IRequestHandler<ListJoinUsCommand, JoinUsApplicationListViewModel>
    {
        private readonly IJoinUsBusiness _joinUsBusiness;

        public ListJoinUsCommandHandler(IJoinUsBusiness joinUsBusiness)
        {
            _joinUsBusiness = joinUsBusiness;
        }
        public Task<JoinUsApplicationListViewModel> Handle(ListJoinUsCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_joinUsBusiness.ListJoinUs(request));
        }
    }
}
