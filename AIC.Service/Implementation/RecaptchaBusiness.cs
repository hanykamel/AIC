using AIC.Service.Entities.CommonActions.Commands;
using AIC.Service.Interfaces;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace AIC.Service.Implementation
{
    public class RecaptchaBusiness : IRecaptchaBusiness
    {
        private readonly IConfiguration _config;
        public RecaptchaBusiness(IConfiguration config) {
            _config = config;
        }
        public async Task<bool> ValidateRecaptcha(ValidateRecaptchaCommand command)
        {
            bool isHuman = true;

            try
            {
                string googleAPI = _config.GetValue<string>("ReCaptcha:GoogleAPI");
                string secretKey = _config.GetValue<string>("ReCaptcha:Secret");
                googleAPI = googleAPI.Replace("{{secret}}", secretKey).Replace("{{response}}", command.UserResponse);
                Uri uri = new Uri(googleAPI);
                HttpWebRequest request = WebRequest.CreateHttp(uri);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = 0;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream);
                string result = streamReader.ReadToEnd();
                ReCaptchaResponse reCaptchaResponse = JsonConvert.DeserializeObject<ReCaptchaResponse>(result);
                if (reCaptchaResponse.Success)
                    isHuman = reCaptchaResponse.Success;
                else
                    throw new Exception("Error: " + reCaptchaResponse.ErrorCodes);

            }
            catch (Exception ex)
            {
                //Trace.WriteLine("reCaptcha error: " + ex);
                //isHuman = false;
                throw new Exception(ex.Message);
            }

            return isHuman;
        }
    }

    public class ReCaptchaResponse
    {
        public bool Success;
        public string ChallengeTs;
        public string Hostname;
        public object[] ErrorCodes;
    }
}
