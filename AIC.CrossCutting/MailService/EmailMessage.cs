using System;
using System.Collections.Generic;
using System.Text;

namespace AIC.CrossCutting.MailService
{
	public class EmailMessage
	{
		public EmailMessage()
		{
			ToAddresses = new List<EmailAddress>();
			FromAddresses = new List<EmailAddress>();
			CCAddresses = new List<EmailAddress>();
		}

		public List<EmailAddress> ToAddresses { get; set; }
		public List<EmailAddress> CCAddresses { get; set; }
		public List<EmailAddress> FromAddresses { get; set; }
		public string Subject { get; set; }
		public string Content { get; set; }
		public string FileURL { get; set; }
	}
}
