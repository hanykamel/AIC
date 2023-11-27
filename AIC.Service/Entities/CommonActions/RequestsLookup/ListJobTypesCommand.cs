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
    public class ListJobTypesCommand : IRequest<List<LookupsViewModel>>
    {
    }
    public class ListJobTypesHandler : IRequestHandler<ListJobTypesCommand, List<LookupsViewModel>>
    {
        private readonly IRequestLookupBusiness _requestsLookupsBusiness;

        public ListJobTypesHandler(IRequestLookupBusiness requestsLookupsBusiness)
        {
            _requestsLookupsBusiness = requestsLookupsBusiness;
        }
        public async Task<List<LookupsViewModel>> Handle(ListJobTypesCommand request, CancellationToken cancellationToken)
        {
            var result = _requestsLookupsBusiness.ListJobTypes(request);
            return await Task.FromResult(result);
        }
    }
}
