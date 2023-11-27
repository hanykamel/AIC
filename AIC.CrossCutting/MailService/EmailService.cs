using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Hosting;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Text.Unicode;
using Microsoft.Extensions.Logging;
using System.Net.Mail;
using System.Net.Mime;

namespace AIC.CrossCutting.MailService
{
    public class EmailService : IEmailService
    {
        private readonly IEmailConfiguration _emailConfiguration;
        private readonly IHostingEnvironment _env;
        ILogger<EmailService> _logger;

        public EmailService(IEmailConfiguration emailConfiguration, IHostingEnvironment env, ILogger<EmailService> logger)
        {
            _emailConfiguration = emailConfiguration;
            _env = env;
            _logger = logger;
        }

        public List<EmailMessage> ReceiveEmail(int maxCount = 10)
        {
            throw new NotImplementedException();
        }

        public void Send(EmailMessage emailMessage)
        {
            try
            {
                _logger.LogInformation("inside send email");
                var message = new MimeMessage();
                message.To.AddRange(emailMessage.ToAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
                message.Cc.AddRange(emailMessage.CCAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));

                message.From.Add(new MailboxAddress(_emailConfiguration.Sender, _emailConfiguration.Sender));
                message.Subject = emailMessage.Subject;

                _logger.LogInformation("FileURL: " + emailMessage.FileURL);
                var builder = new BodyBuilder();
                if (emailMessage.FileURL != "")
                {
                    builder.HtmlBody = emailMessage.Content;
                    builder.Attachments.Add(_emailConfiguration.filePath + emailMessage.FileURL);
                    message.Body = builder.ToMessageBody();
                }
                else
                {
                    message.Body = new TextPart(TextFormat.Html)
                    {
                        Text = emailMessage.Content
                    };
                }
                using (var emailClient = new MailKit.Net.Smtp.SmtpClient())
                {
                    if (emailClient.IsConnected)
                        emailClient.Disconnect(true);

                    emailClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    //The last parameter here is to use SSL (Which you shou5ld!)
                    _logger.LogInformation("time before connect to SMTP: " + DateTime.Now.ToString());
                    emailClient.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, SecureSocketOptions.StartTls);
                    _logger.LogInformation("time after connect to SMTP: " + DateTime.Now.ToString());

                    //Remove any OAuth functionality as we won't be using it. 
                    //emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
                    _logger.LogInformation("time before Authenticate to SMTP: " + DateTime.Now.ToString());
                    emailClient.Authenticate(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);
                    _logger.LogInformation("time after Authenticate to SMTP: " + DateTime.Now.ToString());

                    _logger.LogInformation("time before sending email: " + DateTime.Now.ToString());
                    emailClient.Send(message);
                    _logger.LogInformation("time after sending email: " + DateTime.Now.ToString());

                    emailClient.Disconnect(true);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GetTemplate(string strFile, out string errmsg)
        {
            string content = "";
            errmsg = "";
            try
            {
                using (StreamReader sr = new StreamReader(_env.WebRootPath + strFile))
                {
                    content = sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                errmsg = ex.Message;
                return null;
            }
            return content;
        }
    }
}
