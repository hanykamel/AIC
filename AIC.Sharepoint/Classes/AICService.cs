using Microsoft.SharePoint.Client.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Script.Serialization;

namespace AIC.Sharepoint.Classes
{
    [BasicHttpBindingServiceMetadataExchangeEndpointAttribute]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [System.ServiceModel.ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class AICService : IAICService
    {
        public string GetName()
        {
            return "name";
        }

        public string GetSubscribersList(int PageSize = 20, int PageIndex = 0, string Email = "")
        {
            try
            {
                //EventLog.WriteEntry("GetSubscribersList","start ", EventLogEntryType.Information);
                var webconfig = WebConfigurationManager.OpenWebConfiguration("/");
                string path = Convert.ToString(webconfig.AppSettings.Settings["GetSubscribersApi"].Value);

                string lang = "ar";
                if (HttpContext.Current.Request.Headers["lang"] != null)
                    lang = HttpContext.Current.Request.Headers["lang"];

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("lang", lang);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                HttpContent content;
                if (!string.IsNullOrEmpty(Email))
                    content = new StringContent("{\"PageSize\":" + PageSize + ",\"PageIndex\":" + PageIndex + ",\"Filters\":[{\"Field\":\"Email\",\"Value\":\"" + Email + "\"}]}", Encoding.UTF8, "application/json");
                else
                    content = new StringContent("{\"PageSize\":" + PageSize + ",\"PageIndex\":" + PageIndex + "}", Encoding.UTF8, "application/json");
                var response = client.PostAsync(path, content).Result.Content.ReadAsStringAsync();
                return new JavaScriptSerializer().Serialize(new { Result = "OK", Records = response });

            }
            catch (Exception ex)
            {
                return new JavaScriptSerializer().Serialize(new { Result = "ERROR", Message = ex.Message + " inner: " + ex.InnerException?.Message });
            }
        }

        public string GetJoinUsList(string pageSize, string PageIndex, string Email, string Universty
            , string Age, string AppliedDateTo, string AppliedDateFrom)
        {
            try
            {
                //EventLog.WriteEntry("Get join us lists","step 1",EventLogEntryType.Information);
                var webconfig = WebConfigurationManager.OpenWebConfiguration("/");
                string path = Convert.ToString(webconfig.AppSettings.Settings["GetJoinUsApi"].Value);

                string lang = "ar";
                if (HttpContext.Current.Request.Headers["lang"] != null)
                    lang = HttpContext.Current.Request.Headers["lang"];

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("lang", lang);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                HttpContent content;
                //if (!string.IsNullOrEmpty(Email))
                    content = new StringContent("{\"PageSize\":" + pageSize + ",\"PageIndex\":" + PageIndex + ",\"Filters\":[{\"Field\":\"Email\",\"Value\":\"" + Email + "\"},{\"Field\":\"AppliedDateFrom\",\"Value\":\"" + AppliedDateFrom + "\"},{\"Field\":\"AppliedDateTo\",\"Value\":\"" + AppliedDateTo + "\"},{\"Field\":\"University\",\"Value\":\"" + Universty + "\"},{\"Field\":\"Age\",\"Value\":\"" + Age + "\"}]}", Encoding.UTF8, "application/json");
                ////else
                ////    content = new StringContent("{\"PageSize\":" + pageSize + ",\"PageIndex\":" + PageIndex + "}", Encoding.UTF8, "application/json");
                var response = client.PostAsync(path, content).Result.Content.ReadAsStringAsync();
               // EventLog.WriteEntry("Get join us list response", response.Result);
                return new JavaScriptSerializer().Serialize(new { Result = "OK", Records = response });

            }
            catch (Exception ex)
            {
                //throw e;
                //EventLog.WriteEntry("Get join us list", e.Message, EventLogEntryType.Error);
                return new JavaScriptSerializer().Serialize(new { Result = "ERROR", Message = ex.Message + " inner: " + ex.InnerException?.Message });
            }
        }

        public string GetAppliedJoinUsById(string Id)
        {
            try
            {
                var webconfig = WebConfigurationManager.OpenWebConfiguration("/");
                string path = Convert.ToString(webconfig.AppSettings.Settings["GetAppliedForJoinUsByIdApi"].Value);
                path += $"?Id={Id}";
                string lang = "ar";
                if (HttpContext.Current.Request.Headers["lang"] != null)
                    lang = HttpContext.Current.Request.Headers["lang"];

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("lang", lang);
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                var response = client.GetAsync(path).Result.Content.ReadAsStringAsync();
                return new JavaScriptSerializer().Serialize(new { Result = "OK", Records = response });

            }
            catch (Exception ex)
            {
                // EventLog.WriteEntry("Get join us user", ex.Message, EventLogEntryType.Error);
                return new JavaScriptSerializer().Serialize(new { Result = "ERROR", Message = ex.Message + " inner: " + ex.InnerException?.Message });
            }
        }

        public string DeleteAppliedJoinUsById(string Id)
        {
            try
            {
                var webconfig = WebConfigurationManager.OpenWebConfiguration("/");
                string path = Convert.ToString(webconfig.AppSettings.Settings["DeleteAppliedForJoinUsByIdApi"].Value);
                //path += $"?Id={Id}";
                string lang = "ar";
                if (HttpContext.Current.Request.Headers["lang"] != null)
                    lang = HttpContext.Current.Request.Headers["lang"];

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("lang", lang);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                HttpContent content = new StringContent("{\"Id\":\"" + Id + "\"}", Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(path, content).Result;
                return new JavaScriptSerializer().Serialize(new { Result = "OK", Records = response });

            }
            catch (Exception ex)
            {
                //EventLog.WriteEntry("Delete Join us User", ex.Message, EventLogEntryType.Error);
                return new JavaScriptSerializer().Serialize(new { Result = "ERROR", Message = ex.Message + " inner: " + ex.InnerException?.Message });
            }
        }

        public string GetAppliedForCareersList(string pageSize, string PageIndex, string Email, string Universty
            , string Age, string AppliedDateTo, string AppliedDateFrom, string VacancyId, string RefNum)
        {
            try
            {
               // EventLog.WriteEntry("parameters", " "+ Email +" " + Universty + " " + Age + " " + AppliedDateFrom + " " + AppliedDateTo + " " +VacancyId);
                var webconfig = WebConfigurationManager.OpenWebConfiguration("/");
                string path = Convert.ToString(webconfig.AppSettings.Settings["GetAppliedForCareersApi"].Value);

                string lang = "ar";
                if (HttpContext.Current.Request.Headers["lang"] != null)
                    lang = HttpContext.Current.Request.Headers["lang"];

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("lang", lang);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                HttpContent content;
                //if (!string.IsNullOrEmpty(Email))
                    content = new StringContent("{\"PageSize\":" + pageSize + ",\"PageIndex\":" + PageIndex + ",\"Filters\":[{\"Field\":\"Email\",\"Value\":\"" + Email + "\"},{\"Field\":\"AppliedDateFrom\",\"Value\":\"" + AppliedDateFrom + "\"},{\"Field\":\"AppliedDateTo\",\"Value\":\"" + AppliedDateTo + "\"},{\"Field\":\"University\",\"Value\":\"" + Universty + "\"},{\"Field\":\"Age\",\"Value\":\"" + Age + "\"},{\"Field\":\"VacancyId\",\"Value\":\"" + VacancyId + "\"},{\"Field\":\"RefNum\",\"Value\":\"" + RefNum + "\"}]}", Encoding.UTF8, "application/json");
                //else
                //    content = new StringContent("{\"PageSize\":" + pageSize + ",\"PageIndex\":" + PageIndex + "}", Encoding.UTF8, "application/json");
                var response = client.PostAsync(path, content).Result.Content.ReadAsStringAsync();
                return new JavaScriptSerializer().Serialize(new { Result = "OK", Records = response });

            }
            catch (Exception ex)
            {
                return new JavaScriptSerializer().Serialize(new { Result = "ERROR", Message = ex.Message + " inner: " + ex.InnerException?.Message });
            }

        }

        public string GetAppliedForCareerById(string Id)
        {
            try
            {
                var webconfig = WebConfigurationManager.OpenWebConfiguration("/");
                string path = Convert.ToString(webconfig.AppSettings.Settings["GetAppliedForCareerByIdApi"].Value);
                path += $"?Id={Id}";

                string lang = "ar";
                if (HttpContext.Current.Request.Headers["lang"] != null)
                    lang = HttpContext.Current.Request.Headers["lang"];

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("lang", lang);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var response = client.GetAsync(path).Result.Content.ReadAsStringAsync();
                return new JavaScriptSerializer().Serialize(new { Result = "OK", Records = response });

            }
            catch (Exception ex)
            {
                return new JavaScriptSerializer().Serialize(new { Result = "ERROR", Message = ex.Message + " inner: " + ex.InnerException?.Message });
            }
        }

        public string DeleteAppliedForCareerById(string Id)
        {
            try
            {
                var webconfig = WebConfigurationManager.OpenWebConfiguration("/");
                string path = Convert.ToString(webconfig.AppSettings.Settings["DeleteAppliedForCareerByIdApi"].Value);
                //path += $"?Id={Id}";

                string lang = "ar";
                if (HttpContext.Current.Request.Headers["lang"] != null)
                    lang = HttpContext.Current.Request.Headers["lang"];

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("lang", lang);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                HttpContent content = new StringContent("{\"Id\":\"" + Id + "\"}", Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(path, content).Result;
                return new JavaScriptSerializer().Serialize(new { Result = "OK", Records = response });

            }
            catch (Exception ex)
            {
                //EventLog.WriteEntry("Delete career User", ex.Message, EventLogEntryType.Error);
                return new JavaScriptSerializer().Serialize(new { Result = "ERROR", Message = ex.Message + " inner: " + ex.InnerException?.Message });
            }
        }

        public string GetCareersList()
        {
            try
            {
                var webconfig = WebConfigurationManager.OpenWebConfiguration("/");
                string path = Convert.ToString(webconfig.AppSettings.Settings["GetCareersApi"].Value);

                string lang = "ar";
                if (HttpContext.Current.Request.Headers["lang"] != null)
                    lang = HttpContext.Current.Request.Headers["lang"];

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("lang", lang);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var response = client.GetAsync(path).Result.Content.ReadAsStringAsync();
                return new JavaScriptSerializer().Serialize(new { Result = "OK", Records = response });

            }
            catch (Exception ex)
            {
                return new JavaScriptSerializer().Serialize(new { Result = "ERROR", Message = ex.Message + " inner: " + ex.InnerException?.Message });
            }

        }

        public string GetAppliedForInternshipsList(string pageSize, string PageIndex, string Email,string Universty
            , string Age, string AppliedDateTo,string AppliedDateFrom, string InternshipId, string RefNum)
        {
            try
            {
                var webconfig = WebConfigurationManager.OpenWebConfiguration("/");
                string path = Convert.ToString(webconfig.AppSettings.Settings["GetAppliedForInternshipsApi"].Value);

                string lang = "ar";
                if (HttpContext.Current.Request.Headers["lang"] != null)
                    lang = HttpContext.Current.Request.Headers["lang"];

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("lang", lang);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                HttpContent content;
                //if (!string.IsNullOrEmpty(Email))
                    content = new StringContent("{\"PageSize\":" + pageSize + ",\"PageIndex\":" + PageIndex + ",\"Filters\":[{\"Field\":\"Email\",\"Value\":\"" + Email + "\"},{\"Field\":\"AppliedDateFrom\",\"Value\":\"" + AppliedDateFrom + "\"},{\"Field\":\"AppliedDateTo\",\"Value\":\"" + AppliedDateTo + "\"},{\"Field\":\"University\",\"Value\":\"" + Universty + "\"},{\"Field\":\"Age\",\"Value\":\"" + Age + "\"},{\"Field\":\"InternshipId\",\"Value\":\"" + InternshipId + "\"},{\"Field\":\"RefNum\",\"Value\":\"" + RefNum + "\"}]}", Encoding.UTF8, "application/json");
                //else
                //    content = new StringContent("{\"PageSize\":" + pageSize + ",\"PageIndex\":" + PageIndex + "}", Encoding.UTF8, "application/json");
                var response = client.PostAsync(path, content).Result.Content.ReadAsStringAsync();
                return new JavaScriptSerializer().Serialize(new { Result = "OK", Records = response });

            }
            catch (Exception ex)
            {
                return new JavaScriptSerializer().Serialize(new { Result = "ERROR", Message = ex.Message + " inner: " + ex.InnerException?.Message });
            }
        }

        public string GetAppliedForInternshipById(string Id)
        {
            try
            {
                var webconfig = WebConfigurationManager.OpenWebConfiguration("/");
                string path = Convert.ToString(webconfig.AppSettings.Settings["GetAppliedForInternshipByIdApi"].Value);
                path += $"?Id={Id}";

                string lang = "ar";
                if (HttpContext.Current.Request.Headers["lang"] != null)
                    lang = HttpContext.Current.Request.Headers["lang"];

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("lang", lang);
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                var response = client.GetAsync(path).Result.Content.ReadAsStringAsync();
                return new JavaScriptSerializer().Serialize(new { Result = "OK", Records = response });

            }
            catch (Exception ex)
            {
                return new JavaScriptSerializer().Serialize(new { Result = "ERROR", Message = ex.Message + " inner: " + ex.InnerException?.Message });
            }
        }

        public string DeleteAppliedForInternshipById(string Id)
        {
            try
            {
                var webconfig = WebConfigurationManager.OpenWebConfiguration("/");
                string path = Convert.ToString(webconfig.AppSettings.Settings["DeleteAppliedForInternshipByIdApi"].Value);
                //path += $"?Id={Id}";

                string lang = "ar";
                if (HttpContext.Current.Request.Headers["lang"] != null)
                    lang = HttpContext.Current.Request.Headers["lang"];

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("lang", lang);
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                HttpContent content = new StringContent("{\"Id\":\"" + Id + "\"}", Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(path, content).Result;
                return new JavaScriptSerializer().Serialize(new { Result = "OK", Records = response });

            }
            catch (Exception ex)
            {
                //EventLog.WriteEntry("Delete Internship User", ex.Message, EventLogEntryType.Error);
                return new JavaScriptSerializer().Serialize(new { Result = "ERROR", Message = ex.Message + " inner: " + ex.InnerException?.Message });
            }

        }

        public string GetInternshipsList()
        {
            try
            {
                var webconfig = WebConfigurationManager.OpenWebConfiguration("/");
                string path = Convert.ToString(webconfig.AppSettings.Settings["GetInternshipsApi"].Value);

                string lang = "ar";
                if (HttpContext.Current.Request.Headers["lang"] != null)
                    lang = HttpContext.Current.Request.Headers["lang"];

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("lang", lang);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var response = client.GetAsync(path).Result.Content.ReadAsStringAsync();
                return new JavaScriptSerializer().Serialize(new { Result = "OK", Records = response });

            }
            catch (Exception ex)
            {
                return new JavaScriptSerializer().Serialize(new { Result = "ERROR", Message = ex.Message + " inner: " + ex.InnerException?.Message });
            }

        }

    }
}
