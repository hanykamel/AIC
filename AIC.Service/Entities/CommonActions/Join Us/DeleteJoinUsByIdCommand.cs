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
    public class DeleteJoinUsByIdCommand : IRequest<bool>
    {
        [Required]
        public Guid Id { get; set; }
    }

    public class DeleteJoinUsByIdCommandHandler : IRequestHandler<DeleteJoinUsByIdCommand, bool>
    {
        private readonly IJoinUsBusiness _joinUsBusiness;

        public DeleteJoinUsByIdCommandHandler(IJoinUsBusiness joinUsBusiness)
        {
            _joinUsBusiness = joinUsBusiness;
        }
        public Task<bool> Handle(DeleteJoinUsByIdCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_joinUsBusiness.DeleteById(request));
        }
    }
}
