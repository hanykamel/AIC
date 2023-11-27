using AIC.Service.Entities.CommonActions.ContactUs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Service.Interfaces
{
    public interface IContactUsBusiness
    {
        Task<bool> SendReplys(SendReplysCommand sendReplysCommand);
        Task<bool> CreateContactUsForm(CreateContactUsFormCommand createContactUsFormCommand);
    }
}
