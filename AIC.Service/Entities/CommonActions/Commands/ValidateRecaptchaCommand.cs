using AIC.Service.Entities.Commands;
using AIC.Service.Implementation;
using AIC.Service.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AIC.Service.Entities.CommonActions.Commands
{
    public class ValidateRecaptchaCommand : IRequest<bool>
    {
        [Required]
        public string UserResponse { get; set; }
    }

    public class ValidateRecaptchaCommandHandler : IRequestHandler<ValidateRecaptchaCommand, bool>
    {
        private readonly IRecaptchaBusiness _recaptchaBusiness;

        public ValidateRecaptchaCommandHandler(IRecaptchaBusiness recaptchaBusiness)
        {
            _recaptchaBusiness = recaptchaBusiness;
        }
        public async Task<bool> Handle(ValidateRecaptchaCommand request, CancellationToken cancellationToken)
        {
            return await _recaptchaBusiness.ValidateRecaptcha(request);
        }
    }
}
