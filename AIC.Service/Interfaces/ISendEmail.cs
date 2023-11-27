using AIC.CrossCutting.MailService;
using AIC.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Service.Interfaces
{
    public interface ISendEmail
    {
         void SendMail(List<EmailAddress> toAddresses ,string mailSubject, MailTypesEnum mailType, Dictionary<string, string> data,string fileURL = "", string errorMsg = null);
         void GetMailBody(Dictionary<string, string> placholders, MailTypesEnum mailType, out string body);
         string GetTemplate(MailTypesEnum mailType, out string errmsg);
         EmailMessage InstantiateEmailMessage(List<EmailAddress> toAddresses, string body, string subject, string fileURL = "");



    }
}
