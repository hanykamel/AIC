using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace AIC.Service.Helper
{
    public static class Validations
    {
        public static bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        public static bool IsValidMobileNumber(string mobileNumber)
        {
            var mobileRegix = "(01)[0-9]{9}";
            Match match = Regex.Match(mobileNumber, mobileRegix, RegexOptions.IgnoreCase);
            return match.Success && mobileNumber.Length == 11;
        }

        public static bool IsValidNationalID(string nationalID)
        {
            var nationalIDRegix = "(2|3)[0-9][0-9][0-1][0-9][0-3][0-9]{8}";
            Match match = Regex.Match(nationalID, nationalIDRegix, RegexOptions.IgnoreCase);
            return match.Success && nationalID.Length == 14;
        }
        public static bool IsValidHomePhone(string homeNumber)
        {
            var mobileRegix = "[0-9]{7}";
            Match match = Regex.Match(homeNumber, mobileRegix, RegexOptions.IgnoreCase);
            return match.Success && (homeNumber.Length >= 7 || homeNumber.Length <= 10);
        }
        public static bool IsValidDate(string date, bool optional = false)
        {
            DateTime datetime;
            if(DateTime.TryParse(date, out datetime))
            {
                if(datetime < DateTime.Now.AddYears(150) && datetime > DateTime.Now.AddYears(-150))
                {
                    return true;
                }
            }
            return false || (string.IsNullOrEmpty(date) && optional);
        }
    }
}
