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
            Properties.Settings.Default.IsWorkTimeLimit = true;
            Properties.Settings.Default.Save();
        }

        public TimeSpan WorkTimeLimitCheck()
        {
            var prop = Properties.Settings.Default;
            prop.RestDateFrom = DateTime.Now.Date.Add(new TimeSpan(prop.RestHoursFrom, 00, 0));
            prop.RestDateTo = DateTime.Now.Date.Add(new TimeSpan(prop.RestHoursTo, 00, 0));
            prop.Save();


            if (Properties.Settings.Default.RestDateFrom.Hour == 0 && Properties.Settings.Default.RestDateTo.Hour == 0)
                return TimeSpan.Zero;
            else
            {
                if (Properties.Settings.Default.IsWorkTimeLimit == true)
                {
                    DateTime restdateto;
                    if (prop.RestDateFrom.Hour > prop.RestDateTo.Hour)
                    {
                        restdateto = new DateTime(prop.RestDateTo.Year, prop.RestDateTo.Month, prop.RestDateTo.Day + 1);
                        restdateto = restdateto.Date.Add(new TimeSpan(prop.RestHoursTo, 00, 0));
                        prop.RestDateTo = restdateto;
                        prop.Save();
                    }

                    if (DateTime.Now.Hour >= Properties.Settings.Default.RestDateFrom.Hour || DateTime.Now.Hour < Properties.Settings.Default.RestDateTo.Hour)
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
                }
                else
                    return TimeSpan.Zero;
            }

        }



    }
}
