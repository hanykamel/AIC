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

namespace AIC.Service.Entities.CommonActions.Join_Us
{
    public class GetJoinUsByIdCommand : IRequest<JoinUsViewModel>
    {
        [Required]
        public Guid Id { get; set; }
    }

    public class GetJoinUsByIdCommandHandler : IRequestHandler<GetJoinUsByIdCommand, JoinUsViewModel>
    {
        private readonly IJoinUsBusiness _joinUsBusiness;

        public GetJoinUsByIdCommandHandler(IJoinUsBusiness joinUsBusiness)
        {
            _joinUsBusiness = joinUsBusiness;
        }
        public Task<JoinUsViewModel> Handle(GetJoinUsByIdCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_joinUsBusiness.GetJoinUsById(request));
        }
    }
}
