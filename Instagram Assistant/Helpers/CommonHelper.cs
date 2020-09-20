using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram_Assistant.Helpers
{
    class CommonHelper
    {
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; } // 1 hour
        public DateTime nextStartTime { get; set; } // Next activity start time


        private int likesPerSession{ get; } = 60;
        public int passedActivities { get; set; } = 0;


        //Activities time
        private int timePerOneActivity { get; } = 30; //x1
        private int timePerTwoActivity { get; } = 20; //x2
        private int timePerTreeActivity { get; } = 10; //x3



        private int delayForStories { get; } = 15000;
        private int rndDelayForStories { get; } = 5000;

        public int rest { get; } = 3600000; //after 4-5 activities

        private string hashtagToLike { get; set; } = "салонкрасотыреутов";
        private string hashTagToWatch { get; set; } = "салонкрасотыреутов";
        private bool followByHashTah { get; set; } = false;


        public static bool isFeedLikeInProgress { get; set; } = false;
        public static bool isHashTagLikeInProgress { get; set; } = false;
        public static bool isStoriesWatching { get; set; } = false;
        public static bool isStoriesWatchingByHt { get; set; } = false;


        public long GetInixTime()
        {
            DateTime foo = DateTime.Now;
            long unixTime = ((DateTimeOffset)foo).ToUnixTimeSeconds();
            return unixTime;
        }

        public int GetLikeDelay()
        {
            int delay = Properties.Settings.Default.DelayValue;
            Random rnd = new Random();
            if (rnd.Next(0, 1) == 0)
                delay += rnd.Next(0, Properties.Settings.Default.RandomDelayValue);
            else
                delay -= rnd.Next(0, Properties.Settings.Default.RandomDelayValue);

            return delay * 1000;
        }

        public DateTime GetNextTime(DateTime start, int activities)
        {
            if (activities == 1)
                return start.AddMinutes(timePerOneActivity);
            else if (activities == 2)
                return start.AddMinutes(timePerTwoActivity);
            else if (activities == 3)
                return start.AddMinutes(timePerTreeActivity);
            else
                return start.AddMinutes(15);
        }
    }
}
