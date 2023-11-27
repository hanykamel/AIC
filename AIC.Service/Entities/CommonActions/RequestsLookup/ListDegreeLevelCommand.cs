using AIC.Data.ViewModels.Lookups;
using AIC.Service.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AIC.Service.Entities.CommonActions.RequestsLookup
{
    public class ListDegreeLevelCommand : IRequest<List<LookupsViewModel>>
    {
    }
    public class ListDegreeLevelHandler : IRequestHandler<ListDegreeLevelCommand, List<LookupsViewModel>>
    {
        private readonly IRequestLookupBusiness _requestsLookupsBusiness;

        public ListDegreeLevelHandler(IRequestLookupBusiness requestsLookupsBusiness)
        {
            _requestsLookupsBusiness = requestsLookupsBusiness;
        }
        public async Task<List<LookupsViewModel>> Handle(ListDegreeLevelCommand request, CancellationToken cancellationToken)
        {
            var result = _requestsLookupsBusiness.ListDegreeLevel(request);
            return await Task.FromResult(result);
        }
    }
}
