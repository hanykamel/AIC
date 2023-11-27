using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Configuration;
using Microsoft.SharePoint;
using System.Web.Configuration;
using System.Diagnostics;

namespace AIC.Sharepoint.TimerJobs
{
    class CacheTimerJob : SPJobDefinition
    {
        public CacheTimerJob() : base()
        {
        }
        public string path { get; set; }
        public CacheTimerJob(string jobName, SPWebApplication webapp) : base(jobName, webapp, null, SPJobLockType.ContentDatabase)
        {
            this.Title = "Cache Timer Job Daily";
        }
        public override void Execute(Guid targetInstanceId)
        {
            try
            {
                var webconfig = WebConfigurationManager.OpenWebConfiguration("/", new SPSite(WebApplication.Sites[0].ID).WebApplication.Name);
                this.path = Convert.ToString(webconfig.AppSettings.Settings["CacheApiPath"].Value);
                EventLog.WriteEntry("Cache TJ", path, EventLogEntryType.Information);
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("lang", "ar");
                EventLog.WriteEntry("Cache TJ", client.DefaultRequestHeaders.Count().ToString(), EventLogEntryType.Information);
                var response = client.PostAsync(path, null).Result;
                EventLog.WriteEntry("Cache TJ", response.StatusCode.ToString(), EventLogEntryType.Information);
                base.Execute(targetInstanceId);
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("Cache TJ", ex.Message, EventLogEntryType.Error);
            }
           
        }
    }

}
