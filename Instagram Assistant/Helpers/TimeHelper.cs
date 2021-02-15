using System;
using System.Reflection;

namespace Instagram_Assistant.Helpers
{
    class TimeHelper
    {
        //STATUS: OK
        public string GetNormalTime(int seconds)
        {
            TimeSpan result = TimeSpan.FromSeconds(seconds);
            return result.ToString("hh':'mm':'ss");
        }
        public string GetNormalTime(double seconds)
        {
            TimeSpan result = TimeSpan.FromSeconds(seconds);
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
        public long GetInixTime()
        {
            DateTime foo = DateTime.Now;
            long unixTime = ((DateTimeOffset)foo).ToUnixTimeSeconds();
            return unixTime;
        }

        public string GetExecuteTime(int count)
        {
            int sec = count % 60;
            int min = count / 60;    
            return $"{min} min {sec} sec";
        }
        public int GetExecuteTimeSeconds(int count)
        {
            var rnd = new Random();
            int sec = 0;
            for (int i = 0; i < count; i++)
                sec += rnd.Next(5, 11);
            return sec;
        }
        public int GetExecuteTimeForCheck(int count)
        {
            var rnd = new Random();
            int sec = 0;
            for (int i = 0; i < count; i++)
                sec += rnd.Next(2, 3);
            return sec;
        }

    }
}
