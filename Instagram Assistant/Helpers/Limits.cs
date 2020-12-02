using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram_Assistant.Helpers
{
    class Limits
    {
        //STATUS: OK
        public int MaxActionPerHour { get; } = 30;
        public int MaxActionsPerDay { get; } = 300;

        public int MaxLikesPerHour { get; } = 50;
        public int MaxLikesPerDay { get; } = 1200;

        public int RestAfterChange { get; } = 40000;


        public Limits()
        {
            Properties.Settings.Default.RestDateFrom = DateTime.Now.Date.Add(new TimeSpan(Properties.Settings.Default.RestHoursFrom, 00, 0));
            Properties.Settings.Default.RestDateTo = DateTime.Now.Date.Add(new TimeSpan(Properties.Settings.Default.RestHoursTo, 00, 0));
        }

        public TimeSpan WorkTimeLimitCheck()
        {
            if (Properties.Settings.Default.IsWorkTimeLimit == true)
            {
                if (DateTime.Now.Date != Properties.Settings.Default.RestDateFrom.Date)
                    Properties.Settings.Default.RestDateFrom = DateTime.Now.Date.Add(new TimeSpan(Properties.Settings.Default.RestHoursFrom, 00, 0));
                if (Properties.Settings.Default.RestDateTo.Day != DateTime.Now.Day + 1)
                {
                    DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1);
                    date = date.Add(new TimeSpan(Properties.Settings.Default.RestHoursTo, 00, 0));
                    Properties.Settings.Default.RestDateTo = date;
                }

                if (Properties.Settings.Default.RestDateFrom.Hour == 0 && Properties.Settings.Default.RestDateTo.Hour == 0)
                    return TimeSpan.Zero;
                else if (DateTime.Now.Hour >= Properties.Settings.Default.RestDateFrom.Hour || DateTime.Now.Hour < Properties.Settings.Default.RestDateTo.Hour)
                {
                    if (DateTime.Now.Day == Properties.Settings.Default.RestDateFrom.Day && DateTime.Now.Day + 1 == Properties.Settings.Default.RestDateTo.Day)
                    {
                        return Properties.Settings.Default.RestDateTo - DateTime.Now;
                    }
                    else
                        return TimeSpan.Zero;
                }
                else
                    return TimeSpan.Zero;
            }else
                return TimeSpan.Zero;

        }



    }
}
