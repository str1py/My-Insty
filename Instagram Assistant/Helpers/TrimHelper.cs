using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Instagram_Assistant.Helpers
{
    class TrimHelper
    {

        public string PersonalPhoneToStars(string data)
        {
            string securedata = (Regex.Replace(data, @"\d(?!\d{0,3}$)", "*"));
            return securedata;
        }

        public string PersonalEmailToStars(string data)
        { 
            string pattern = @"(?<=[\w]{1})[\w-\._\+%]*(?=[\w]{1}@)";
            string result = Regex.Replace(data, pattern, m => new string('*', m.Length));
            return result;
        }
    }
}
