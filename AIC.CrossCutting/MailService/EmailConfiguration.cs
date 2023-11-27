using System;
using System.Collections.Generic;
using System.Text;

namespace AIC.CrossCutting.MailService
{
	public interface IEmailConfiguration
	{
		string SmtpServer { get; }
		int SmtpPort { get; }
		string SmtpUsername { get; set; }
		string SmtpPassword { get; set; }
		string Sender { get; set; }

		string PopServer { get; }
		int PopPort { get; }
		string PopUsername { get; }
		string PopPassword { get; }

		string filePath { get; }
	}

	public class EmailConfiguration : IEmailConfiguration
	{
		public string SmtpServer { get; set; }
		public int SmtpPort { get; set; }
		public string SmtpUsername { get; set; }
		public string SmtpPassword { get; set; }
		public string Sender { get; set; }

		public string PopServer { get; set; }
		public int PopPort { get; set; }
		public string PopUsername { get; set; }
		public string PopPassword { get; set; }
		public string filePath { get; set; }
	}
}
