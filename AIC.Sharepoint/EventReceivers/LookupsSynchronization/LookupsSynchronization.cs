using System;
using System.Configuration;
using System.Diagnostics;
using System.Net.Http;
using System.Security.Permissions;
using System.Text;
using System.Web.Configuration;
using System.Web.Script.Serialization;
using APA.Sharepoint.Enums;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AIC.Sharepoint.EventReceivers.LookupsSynchronization
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class LookupsSynchronization : SPItemEventReceiver
    {
        /// <summary>
        /// An item is being updated.
        /// </summary>
        public override void ItemUpdated(SPItemEventProperties properties)
        {
            try
            {
                EventLog.WriteEntry("sync E", "start sync", EventLogEntryType.Information);
                var newFormUrl = properties.List.DefaultNewFormUrl.ToLower();
                var isArLang = newFormUrl.Contains("/ar/");
                var isRoot = !newFormUrl.Contains("/ar/") && !newFormUrl.Contains("/en/");

                if ((newFormUrl.Contains("careers") && properties.ListItem.ModerationInformation.Status == SPModerationStatusType.Approved)
                    || (newFormUrl.Contains("internships") && properties.ListItem.ModerationInformation.Status == SPModerationStatusType.Approved)
                    || newFormUrl.Contains("degreelevel") || newFormUrl.Contains("jobtype"))
                {
                    SynchronizationEnum type = SynchronizationEnum.Careers;
                    if (newFormUrl.Contains("careers"))
                        type = SynchronizationEnum.Careers;
                    else if (newFormUrl.Contains("internships"))
                        type = SynchronizationEnum.Internships;
                    else if (newFormUrl.Contains("degreelevel"))
                        type = SynchronizationEnum.DegreeLevels;
                    else if (newFormUrl.Contains("jobtype"))
                        type = SynchronizationEnum.JobTypes;

                    string path = Convert.ToString(ConfigurationManager.AppSettings["SyncApiPath"]);

                    HttpClient client = new HttpClient();
                    object obj;
                    //if (type == SynchronizationEnum.JobTypes || type == SynchronizationEnum.DegreeLevels)
                    //{
                        EventLog.WriteEntry("sync E", type.ToString(), EventLogEntryType.Information);
                        obj = new
                        {
                            Id = int.Parse(properties.ListItem["ID"].ToString()),
                            Type = Convert.ToInt32(type),
                            Title = properties.ListItem["Title"].ToString(),
                            TitleAr = properties.ListItem["AICTitleAr"].ToString(),
                            ReferenceNumber = type == SynchronizationEnum.Careers ? properties.ListItem["AICJobReferenceNumber"].ToString(): type == SynchronizationEnum.Internships? properties.ListItem["AICInternshipReferenceNumber"].ToString() : "",
                            IsDeleted = false
                        };

                    //}
                    //else
                    //{
                    //    EventLog.WriteEntry("sync E", "careers internships", EventLogEntryType.Information);
                    //    EventLog.WriteEntry("sync E", "Reference Number= " + properties.ListItem["AICJobReferenceNumber"].ToString(), EventLogEntryType.Information);
                    //    obj = new
                    //    {
                    //        Id = int.Parse(properties.ListItem["ID"].ToString()),
                    //        Type = Convert.ToInt32(type),
                    //        Title = !isArLang ? properties.ListItem["Title"].ToString() : "",
                    //        TitleAr = isArLang ? properties.ListItem["Title"].ToString() : "",
                    //        IsDeleted = false,
                    //        ReferenceNumber = properties.ListItem["AICJobReferenceNumber"].ToString()
                    //    };
                    //}
                    EventLog.WriteEntry("sync E", path , EventLogEntryType.Information);
                    string postBody = JsonConvert.SerializeObject(obj);
                    EventLog.WriteEntry("sync E", postBody, EventLogEntryType.Information);
                    var httpContent = new StringContent(postBody, Encoding.UTF8, "application/json");

                    client.DefaultRequestHeaders.Add("lang", isArLang ? "ar" : "en");
                    var response = client.PostAsync(path, httpContent).Result;
                    properties.ErrorMessage = response.IsSuccessStatusCode.ToString();
                    EventLog.WriteEntry("sync E", response.StatusCode.ToString(), EventLogEntryType.Information);
                    

                }
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("sync E", ex.Message, EventLogEntryType.Error);
                throw ex;
            }
        }


        /// <summary>
        /// An item is being deleted
        /// </summary>
        public override void ItemDeleting(SPItemEventProperties properties)
        {
            try
            {
                var newFormUrl = properties.List.DefaultNewFormUrl.ToLower();
                var isArLang = newFormUrl.Contains("/ar/");
                if (newFormUrl.Contains("careers") || newFormUrl.Contains("internships")
                    || newFormUrl.Contains("degreelevel") || newFormUrl.Contains("jobtype"))
                {

                    SynchronizationEnum type = SynchronizationEnum.Careers;
                    if (newFormUrl.Contains("careers"))
                        type = SynchronizationEnum.Careers;
                    else if (newFormUrl.Contains("internships"))
                        type = SynchronizationEnum.Internships;
                    else if (newFormUrl.Contains("degreelevel"))
                        type = SynchronizationEnum.DegreeLevels;
                    else if (newFormUrl.Contains("jobtype"))
                        type = SynchronizationEnum.JobTypes;

                    string path = Convert.ToString(ConfigurationManager.AppSettings["SyncApiPath"]);

                    HttpClient client = new HttpClient();
                    object obj = new
                    {
                        Id = int.Parse(properties.ListItem["ID"].ToString()),
                        IsDeleted = true,
                        Type = Convert.ToInt32(type),
                    };
                    string postBody = JsonConvert.SerializeObject(obj);
                    var httpContent = new StringContent(postBody, Encoding.UTF8, "application/json");

                    client.DefaultRequestHeaders.Add("lang", isArLang ? "ar" : "en");

                    var response = client.PostAsync(path, httpContent).Result;
                    if (!response.IsSuccessStatusCode)
                    {
                        if (response.Content.ReadAsStringAsync().Result.Contains("CannotDeleteDepartment"))
                        {
                            properties.ErrorMessage = isArLang ? "لا يمكن حذف هذا العنصر لإرتباطه بعناصر أخرى" : "Can't delete this item as it related to other entities";
                        }
                        else
                        {
                            properties.ErrorMessage = isArLang ? "خطأ في النظام الداخلي" : "Internal server error";
                        }

                        properties.Status = SPEventReceiverStatus.CancelWithError;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public override void ItemAdded(SPItemEventProperties properties)
        {
            try
            {
                var newFormUrl = properties.List.DefaultNewFormUrl.ToLower();
                if (newFormUrl.Contains("degreelevel") || newFormUrl.Contains("jobtype"))
                {
                    SynchronizationEnum type = SynchronizationEnum.DegreeLevels;
                    if (newFormUrl.Contains("degreelevel"))
                        type = SynchronizationEnum.DegreeLevels;
                    else if (newFormUrl.Contains("jobtype"))
                        type = SynchronizationEnum.JobTypes;

                    string path = Convert.ToString(ConfigurationManager.AppSettings["SyncApiPath"]);

                    HttpClient client = new HttpClient();
                    object obj = new
                    {
                        Id = int.Parse(properties.ListItem["ID"].ToString()),
                        Type = Convert.ToInt32(type),
                        Title = properties.ListItem["Title"].ToString(),
                        TitleAr = properties.ListItem["AICTitleAr"].ToString()
                    };

                    string postBody = JsonConvert.SerializeObject(obj);
                    var httpContent = new StringContent(postBody, Encoding.UTF8, "application/json");

                    client.DefaultRequestHeaders.Add("lang", "ar");
                    var response = client.PostAsync(path, httpContent).Result;
                    properties.ErrorMessage = response.IsSuccessStatusCode.ToString();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}