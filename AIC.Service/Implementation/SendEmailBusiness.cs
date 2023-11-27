using AIC.CrossCutting.MailService;
using AIC.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using AIC.CrossCutting.ExceptionHandling;
using AIC.Data.Enums;
using AIC.SP.Middleware.Interfaces;
using AIC.SP.Middleware.SPViewModels;
using AIC.SP.Middleware.Models;
using AIC.CrossCutting.Constant;
using System.Linq;

namespace AIC.Service.Implementation
{
    public class SendEmailBusiness : ISendEmail
    {
        readonly private IConfiguration _config;
        private readonly IWebHostEnvironment _env;
        private IEmailService _emailService;
        private readonly IService<MailsViewModel> _mailsService;

        public SendEmailBusiness(IConfiguration config, IWebHostEnvironment env, IEmailService emailService, IService<MailsViewModel> mailsService)
        {
            _config = config;
            _env = env;
            _emailService = emailService;
            _mailsService = mailsService;
        }

        public void GetMailBody(Dictionary<string, string> placholders, MailTypesEnum mailType, out string body)
        {
            string errMsg = string.Empty;
            body = GetTemplate(mailType, out errMsg);
            foreach (var item in placholders)
            {
                string placeholder =  item.Key ;
                body = body.Replace(placeholder, item.Value, true, System.Globalization.CultureInfo.CurrentCulture);
            }
        }

        public string GetTemplate(MailTypesEnum mailType, out string errmsg)
        {
            string content = "";
            errmsg = "";
            var filePath = $"/MailTemplates/MailTemplate.html";

            try
            {
                var query = new Query
                {
                    Filters = new List<Filter>()
                };
                query.Filters.Add(new Filter
                {
                    Field = Constant.Fields.MailType,
                    Operator = "Eq",
                    Value = ((int)mailType).ToString(),
                    FieldValueType = "Lookup"
                });
                var email = _mailsService.GetAll(query).Items.FirstOrDefault();
                if(email is not null)
                {
                    string mailTemplate;
                    using (StreamReader sr = new StreamReader(_env.WebRootPath + filePath))
                    {
                        mailTemplate = sr.ReadToEnd();
                    }
                    if(mailTemplate is not null)
                    {
                        var emialContent = email.Content.Split("​#StartAr#");
                        if(emialContent is not null && emialContent.Count() > 0)
                        {
                            mailTemplate = mailTemplate.Replace("{BodyAr}", emialContent[1]);
                            mailTemplate = mailTemplate.Replace("{BodyEn}", emialContent[0]);
                            return mailTemplate;
                        }
                        else
                        {
                            content = email.Content.ToString();
                        }
                    }
                    else
                    {
                        content = email.Content.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                errmsg = ex.Message;
                return null;
            }
            return content;
        }

        public EmailMessage InstantiateEmailMessage(List<EmailAddress> toAddresses, string body, string subject, string fileURL = "")
        {
            List<EmailAddress> ccEmails = new List<EmailAddress>();
            string CCNames = _config.GetValue<string>("CCName:Email");
            if(CCNames != null && CCNames != string.Empty)
            {
                List<string> Addresses = _config.GetValue<string>("CCName:Email").Split(',').ToList();
                foreach (var address in Addresses)
                {
                    ccEmails.Add(new EmailAddress() { Address = address });
                }
            }
           
            EmailMessage emailMessage = new EmailMessage
            {
                ToAddresses = toAddresses,
                Content = body,
                Subject = subject,
                CCAddresses = ccEmails,
                FileURL = fileURL
            };
            return emailMessage;
        }

        public void SendMail(List<EmailAddress> toAddresses, string mailSubject, MailTypesEnum mailType, Dictionary<string, string> data,string fileURL = "" , string errorMsg=null)
        {
            try
            {
                string subject = _config.GetValue<string>("MailSubjects:" + mailSubject) == null? mailSubject : _config.GetValue<string>("MailSubjects:" + mailSubject);
                GetMailBody(data, mailType, out string body);
                EmailMessage emailMessage = InstantiateEmailMessage(toAddresses, body, subject, fileURL);
                _emailService.Send(emailMessage);
            }
            catch (Exception ex)
            {
                throw new EmailNotSentException(errorMsg?? "Email Failed");
            }

        }


    }
}
