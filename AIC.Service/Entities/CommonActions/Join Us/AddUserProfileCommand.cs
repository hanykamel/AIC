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
    public class AddUserProfileCommand : IRequest<bool>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
    public class AddUserProfileHandler : IRequestHandler<AddUserProfileCommand, bool>
    {
        private readonly IJoinUsBusiness _joinUsBusiness;

        public AddUserProfileHandler(IJoinUsBusiness joinUsBusiness)
        {
            _joinUsBusiness = joinUsBusiness;
        }
        public async Task<bool> Handle(AddUserProfileCommand request, CancellationToken cancellationToken)
        {
            return await _joinUsBusiness.AddUserProfile(request.Email);
        }
    }
}
