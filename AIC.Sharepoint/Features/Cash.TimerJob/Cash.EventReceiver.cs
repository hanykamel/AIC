using AIC.Sharepoint.TimerJobs;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace AIC.Sharepoint.Features.Cash.TimerJob
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("c32e79d0-3fc3-4df9-ba83-a110f9ada58b")]
    public class CashEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        const string JobName = "Cache Timer Job";
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

                CacheTimerJob job = new CacheTimerJob(JobName, site);
                SPMinuteSchedule schedule = new SPMinuteSchedule();
                schedule.BeginSecond = 0;
                schedule.EndSecond = 59;
                schedule.Interval = 15;
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
