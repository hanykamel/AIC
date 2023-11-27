using AIC.Sharepoint.TimerJobs;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace AIC.Sharepoint.Features.Newsletter.Timerjob
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("445ac2e8-229e-4e5f-8d50-aea087ff63d3")]
    public class NewsletterEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        const string JobName = "News Letter Timer Job";
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    SPWebApplication parentWebApp = (SPWebApplication)properties.Feature.Parent;
                    DeleteExistingJob(JobName, parentWebApp);
                    CreateJob(parentWebApp);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Uncomment the method below to handle the event raised before a feature is deactivated.
        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            lock (this)
            {
                try
                {
                    SPSecurity.RunWithElevatedPrivileges(delegate ()
                    {
                        SPWebApplication parentWebApp = (SPWebApplication)properties.Feature.Parent;
                        DeleteExistingJob(JobName, parentWebApp);
                    });
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private bool CreateJob(SPWebApplication site)
        {
            bool jobCreated = false;
            try
            {
                NewsLetterMailTimerJob job = new NewsLetterMailTimerJob(JobName, site);
                SPDailySchedule schedule = new SPDailySchedule();
                schedule.BeginHour = 1;
                schedule.BeginMinute = 1;
                schedule.EndHour = 3;
                schedule.EndMinute = 15;
                job.Schedule = schedule;

                job.Update();
            }
            catch (Exception e)
            {
                throw e;
            }
            return jobCreated;
        }
        public bool DeleteExistingJob(string jobName, SPWebApplication site)
        {
            bool jobDeleted = false;
            try
            {
                foreach (SPJobDefinition job in site.JobDefinitions)
                {
                    if (job.Name == jobName)
                    {
                        job.Delete();
                        jobDeleted = true;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return jobDeleted;
        }



        // Uncomment the method below to handle the event raised after a feature has been installed.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}
    }
}
