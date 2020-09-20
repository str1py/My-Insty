using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Instagram_Assistant.Helpers
{
    class TimeHelper
    {
        public string GetNormalTime(int time)
        {
            TimeSpan result = TimeSpan.FromSeconds(time);
            return result.ToString("hh':'mm':'ss");
        }
        public string GetNormalTime(TimeSpan time)
        {
            return time.ToString("hh':'mm':'ss");
        }
        public string GetNormalTime(DateTime time)
        {
            string t = time.Hour + ":" + time.Minute + ":" + time.Second;
            return t;
        }

        public string GetTimeNow()
        {
            try
            {
                var date = DateTime.Now;
                string seconds;
                string minutes;
                if (date.Second < 10)
                    seconds = "0" + date.Second.ToString();
                else
                    seconds = date.Second.ToString();

                if (date.Minute < 10)
                    minutes = "0" + date.Minute.ToString();
                else
                    minutes = date.Minute.ToString();

                return $"{date.Hour}:{minutes}:{seconds}";
            }
            catch (Exception ex)
            {
                //logService.Add(ex, MessageType.Type.ERROR,
                //     MethodBase.GetCurrentMethod().DeclaringType);
                Console.WriteLine(ex.Message + "in " + MethodBase.GetCurrentMethod().DeclaringType);
                return null;
            }
        }
    }
}
