﻿using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace AIC.Sharepoint.TimerJobs
{
    public class SendFeedbackReplaysTimerJob : SPJobDefinition
    {
        public SendFeedbackReplaysTimerJob() : base()
        { }
        public string path { get; set; }
        public SendFeedbackReplaysTimerJob(string jobName, SPWebApplication webapp) : base(jobName, webapp, null, SPJobLockType.ContentDatabase)
        {
            this.Title = "Send Feedback Replays Timer Job";
        }
        public override void Execute(Guid targetInstanceId)
        {
            HttpResponseMessage response;
            try
            {
                var webconfig = WebConfigurationManager.OpenWebConfiguration("/", new SPSite(WebApplication.Sites[0].ID).WebApplication.Name);
                this.path = Convert.ToString(webconfig.AppSettings.Settings["SendReplaysApiPath"].Value);
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("lang", "ar");
                HttpContent content = new StringContent("{\"Type\":3}", Encoding.UTF8, "application/json");
                response = client.PostAsync(path, content).Result;
                EventLog.WriteEntry("replay feedback", response.StatusCode.ToString(), EventLogEntryType.Information);
                base.Execute(targetInstanceId);

            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("replay feedback", ex.Message, EventLogEntryType.Error);
            }
        }
    }
}
