using Instagram_Assistant.Enums;
using Instagram_Assistant.Model;
using Instagram_Assistant.Model.Base;
using Instagram_Assistant.ViewModel;
using Instagram_Assistant.ViewModel.BaseModels;
using InstagramApiSharp.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Instagram_Assistant.Helpers
{
    class HelperBase : VarsCommon
    {
        protected CommonViewModel mainInstanse;
        protected ObservableCollection<ActionModel> actions; //What app doing 
        protected StatsModelBase stats; //for overview - likes and time stats

        private int requests;
        protected int Requests
        {
            get { return requests; }
            set
            {
                requests = value;
            }
        }

        protected async Task WorkTimeCheck(TimeSpan wtlc)
        {
            if (wtlc != TimeSpan.Zero)
            {
                logs.Add($"Going sleep. Rest time until {Properties.Settings.Default.RestDateTo.Hour}:00. Total rest time {wtlc.Hours}h {wtlc.Minutes}m", MessageType.Type.DEBUGINFO, this.GetType().Name);
                double sec = wtlc.TotalSeconds;
                do
                {
                    await Task.Delay(1000);
                    stats = du.StatsUpdate(stats, mainInstanse, AccountStatus.Type.REST.ToString(), null, 0, 0, null, th.GetNormalTime(sec--));
                } while (sec > 0);
                TimeInit();
                stats = du.StatsUpdate(stats, mainInstanse, AccountStatus.Type.WORKING.ToString(), mainVars.GetTotalCountFromProperties(mainInstanse), 0, null, null, th.GetNormalTime(EndTime));
            }
        }
        protected async Task HourPassedCheck()
        {
            if (EndTime < DateTime.Now)
            {
                logs.Add($"Hour passed. It`s time to have some rest. Rest time {Properties.Settings.Default.RestTimeMinutes} minutes", MessageType.Type.DEBUGINFO, this.GetType().Name);
                Delay = 0;
                double resttime = TimeSpan.FromMinutes(Properties.Settings.Default.RestTimeMinutes).TotalSeconds;
                while (mainVars.GetProgressStatus(mainInstanse) == true && resttime > 0)
                {
                    await Task.Delay(1000);
                    resttime--;
                    stats = du.StatsUpdate(stats, mainInstanse, AccountStatus.Type.REST.ToString(), null, 0, 0, null, th.GetNormalTime(resttime));
                }
                TimeInit();

                stats = du.StatsUpdate(stats, mainInstanse, AccountStatus.Type.WORKING.ToString(), mainVars.GetTotalCountFromProperties(mainInstanse), 0, null, null, th.GetNormalTime(EndTime));
            }
        }
        protected async Task MaxCountPassedCheck()
        {
            int maxCount = mainVars.GetMaxCount(mainInstanse);
            if (Int32.Parse(stats.SessionCount) >= maxCount && maxCount != 0)
            {
                logs.Add($"Max actions per hour. It`s time to have some rest. Rest time {Properties.Settings.Default.RestTimeMinutes} minutes", MessageType.Type.DEBUGINFO, this.GetType().Name);
                Delay = 0;
                double resttime = TimeSpan.FromMinutes(Properties.Settings.Default.RestTimeMinutes).TotalSeconds;
                while (mainVars.GetProgressStatus(mainInstanse) && resttime > 0)
                {
                    await Task.Delay(1000);
                    resttime--;
                    stats = du.StatsUpdate(stats, mainInstanse, AccountStatus.Type.REST.ToString(), null, 0, 0, null, th.GetNormalTime(resttime));
                }
                TimeInit();
                stats = du.StatsUpdate(stats, mainInstanse, AccountStatus.Type.WORKING.ToString(), mainVars.GetTotalCountFromProperties(mainInstanse), 0, null, null, th.GetNormalTime(EndTime));
            }
        }

        protected void TimeInit()
        {
            timepass = 0;
            timer = new DispatcherTimer();
            StartTime = DateTime.Now;
            EndTime = GetNextTime(StartTime); // time to next session after four
        }
        protected async Task<bool> InitCommonData(CommonViewModel model)
        {
            Account = await accountInfoHelper.GetMainAccountAsync();
            if (Account != null)
            {
                accountInfoHelper.UpdateAccountStatus(Account, AccountStatus.Type.WORKING);
                mainVars.ChangeProgressToTrue(model);

                stats = model.Stats ?? new StatsModelBase();

                TimeInit();
                stats = du.StatsUpdate(stats, mainInstanse, "WORKING", mainVars.GetTotalCountFromProperties(model), null, null, null, th.GetNormalTime(EndTime));
                return true;
            }
            else
                return false;
        }

        //MUST BE PUBLIC 
        public void Stop(CommonViewModel model)
        {
            timerStop();
            timepass = 0;
            mainVars.ChangeProgressToFalse(model);
            stats = du.StatsReset();
            accountInfoHelper.UpdateAccountStatus(Account, AccountStatus.Type.REST);
            stats = du.StatsUpdate(stats, model, AccountStatus.Type.OFF.ToString(), mainVars.GetTotalCountFromProperties(model), 0, 0, "00:00:00", "00:00:00");
            model.ButtonContent = "Start";
        }

        //Skip chance
        public bool Skip()
        {
            Random random = new Random();
            int a = random.Next(0, 100);
            if (a < Properties.Settings.Default.SkipChance)
                return true;
            else
                return false;
        }

        //Calculate Delay
        protected int GetLikeDelay()
        {
            int delay = Properties.Settings.Default.DelayValue; // standart delay
            Random rnd = new Random();
            //if 0 - standart delay + random else - 
            if (rnd.Next(0, 1) == 0)
                delay += rnd.Next(0, Properties.Settings.Default.RandomDelayValue);
            else
                delay -= rnd.Next(0, Properties.Settings.Default.RandomDelayValue);

            return delay * 1000; // * for miliseconds for Task.Delay()
        }
        protected int GetStoryDelay()
        {
            int delay = Properties.Settings.Default.StoriesDelay; // standart delay
            Random rnd = new Random();
            //if 0 - standart delay + random else - 
            if (rnd.Next(0, 1) == 0)
                delay += rnd.Next(0, Properties.Settings.Default.RandomStoriesDelay);
            else
                delay -= rnd.Next(0, Properties.Settings.Default.RandomStoriesDelay);

            return delay * 1000; // * for miliseconds for Task.Delay()
        }
        protected int GetUnfollowDelay()
        {
            int delay = 120; // standart delay
            Random rnd = new Random();
            //if 0 - standart delay + random else - 
            if (rnd.Next(0, 1) == 0)
                delay += rnd.Next(0, 20);
            else
                delay -= rnd.Next(0, 20);

            return delay * 1000; // * for miliseconds for Task.Delay()
        }

        //Get next activity time
        protected DateTime GetNextTime(DateTime start)
        {
            var time = start.AddMinutes(60);

            string hours = time.Hour.ToString();
            string minutes = time.Minute.ToString();
            string seconds = time.Second.ToString();

            if (hours.Length < 2)
                hours = "0" + hours;

            if (minutes.Length < 2)
                minutes = "0" + minutes;

            if (seconds.Length < 2)
                seconds = "0" + seconds;


            var date = hours + ":" + minutes + ":" + seconds;
            DateTime myDate;
            if (!DateTime.TryParse(date, out myDate))
            {
                // handle parse failure
            }
            return myDate;
        }

        protected void timerStart()
        {
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            timer.Start();
        }
        protected void timerStop()
        {
            Delay = 0;
            timer?.Stop();
        }
        protected virtual void timerTick(object sender, EventArgs e)
        {
            timepass++;
            stats.TimeInWork = th.GetNormalTime(timepass);

            //Дергется делей
            stats = du.StatsUpdate(stats, mainInstanse, null, null, null, null, null, null);
            if (Delay > 0)
            {
                if (Delay > 1000)
                    Delay /=  1000;

                Delay--;
                stats = du.StatsUpdate(stats, mainInstanse, null, null, null, Delay, null, null);
            }
        }
    }
}
