using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AIC.SP.Middleware.Helpers
{
    public static class RemoveSympolsFromStringHelper
    {
        public static string StringHandler(string text)
        {
            if (string.IsNullOrEmpty(text) || text == "null")
                return null;
            var trimmedText = Regex.Replace(text, "\"|{|}|<div|<div>|</div>|class=|<p|<p>|</p>|&nbsp;|\n|/|\np|/np/|<span>|<span|<br>|style=color", string.Empty);
            trimmedText = trimmedText.Replace("null", string.Empty);
            return trimmedText;
        }

    }
}
