using System;
using System.Collections.Generic;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Diagnostics;

namespace AIC.Sharepoint.TimerJobs
{
    class NewsLetterMailTimerJob : SPJobDefinition
    {
        public NewsLetterMailTimerJob() : base()
        { }
        public string path { get; set; }
        public NewsLetterMailTimerJob(string jobName, SPWebApplication webapp) : base(jobName, webapp, null, SPJobLockType.ContentDatabase)
        {
            this.Title = "News Letter Timer Job";
        }
        public override void Execute(Guid targetInstanceId)
        {
            try
            {
                var webconfig = WebConfigurationManager.OpenWebConfiguration("/", new SPSite(WebApplication.Sites[0].ID).WebApplication.Name);
                this.path = Convert.ToString(webconfig.AppSettings.Settings["NewsLetterApiPath"].Value);
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("lang", "ar");
                var response = client.PostAsync(path, null).Result;
                EventLog.WriteEntry("Newsletter TJ", response.StatusCode.ToString(), EventLogEntryType.Information);
                base.Execute(targetInstanceId);
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("Newsletter TJ", ex.Message, EventLogEntryType.Error);
            }
           
        }
    }
}
