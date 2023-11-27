using AIC.Service.Entities.CommonActions.Commands;
using AIC.Service.Entities.CommonActions.Vacancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Service.Interfaces
{
    public interface IRecaptchaBusiness
    {
        Task<bool> ValidateRecaptcha(ValidateRecaptchaCommand command);
    }
}
