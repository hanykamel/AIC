using System;
using System.Collections.Generic;
using System.Text;

namespace AIC.CrossCutting.MailService
{
	public interface IEmailService
	{
		void Send(EmailMessage emailMessage);
		List<EmailMessage> ReceiveEmail(int maxCount = 10);
		string GetTemplate(string strFile, out string errmsg);
	}
}
