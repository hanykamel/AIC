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
    public class GetJoinUsCommand : IRequest<JoinUsViewModel>
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Date { get; set; }
    }
    public class GetJoinUsHandler : IRequestHandler<GetJoinUsCommand, JoinUsViewModel>
    {
        private readonly IJoinUsBusiness _joinUsBusiness;

        public GetJoinUsHandler(IJoinUsBusiness joinUsBusiness)
        {
            _joinUsBusiness = joinUsBusiness;
        }
        public async Task<JoinUsViewModel> Handle(GetJoinUsCommand request, CancellationToken cancellationToken)
        {
            return await _joinUsBusiness.GetJoinUs(request.Email , request.Date);
        }
    }
}
