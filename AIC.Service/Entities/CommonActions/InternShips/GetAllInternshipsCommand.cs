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
    public class GetAllInternshipsCommand : IRequest<List<InternshipItemViewModel>>
    {
    }

    public class GetAllInternshipsCommandHandler : IRequestHandler<GetAllInternshipsCommand, List<InternshipItemViewModel>>
    {
        private readonly IInternShipsBusiness _internshipBusiness;
        public GetAllInternshipsCommandHandler(IInternShipsBusiness internshipBusiness)
        {
            _internshipBusiness = internshipBusiness;
        }

        public Task<List<InternshipItemViewModel>> Handle(GetAllInternshipsCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_internshipBusiness.GetAll(request));
        }
    }
}
