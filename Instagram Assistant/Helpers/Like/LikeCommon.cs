using Instagram_Assistant.Enums;
using Instagram_Assistant.Model;
using Instagram_Assistant.ViewModel.BaseModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Instagram_Assistant.Helpers.Like
{
    abstract class LikeCommon : CommonForAll
    {
        //STATUS: +OK

        protected List<string> seenMediasMassive;

        protected ObservableCollection<ActionModel> feedactions;
        protected List<FeedModel> userfeed; //List For Feeds from user Instagram
        protected StatsModelBase _feedstats; //for overview - likes and time stats

        protected LikeViewModelBase mainInstanse;



        protected async Task WorkTimeCheck(TimeSpan wtlc)
        {
            if (wtlc != TimeSpan.Zero)
            {
                logs.Add($"It`s time to have some rest. Rest time until {Properties.Settings.Default.RestDateTo.Hour}:00. Total rest time {wtlc.Hours}h {wtlc.Minutes}m", MessageType.Type.DEBUGINFO, this.GetType().Name);
                double sec = wtlc.TotalSeconds;
                do
                {
                    await Task.Delay(1000);
                    _feedstats = du.StatsUpdate(_feedstats, this.GetType().Name, "REST", null, 0, 0, null, th.GetNormalTime(sec--));
                } while (sec > 0);
                TimeInit();
                _feedstats = du.StatsUpdate(_feedstats, this.GetType().Name, "ON", Properties.Settings.Default.UnfollowCount, 0, null, null, th.GetNormalTime(EndTime));
            }
        }
        protected async Task HourPassedCheck()
        {
            if (EndTime < DateTime.Now)
            {
                logs.Add($"It`s time to have some rest. Rest time {Properties.Settings.Default.RestTimeMinutes} minutes", MessageType.Type.DEBUGINFO, this.GetType().Name);
                Delay = 0;
                double resttime = TimeSpan.FromMinutes(Properties.Settings.Default.RestTimeMinutes).TotalSeconds;
                while (mainVars.IsHashtagLikeInProgres == true && resttime > 0)
                {
                    await Task.Delay(1000);
                    resttime--;
                    _feedstats = du.StatsUpdate(_feedstats, this.GetType().Name, "REST", null, 0, 0, null, th.GetNormalTime(resttime));
                }
                TimeInit();
                _feedstats = du.StatsUpdate(_feedstats, this.GetType().Name, "ON", Properties.Settings.Default.UnfollowCount, 0, null, null, th.GetNormalTime(EndTime));
            }
        }
        protected async Task MaxCountPassedCheck()
        {
            if (Int32.Parse(_feedstats.SessionCount) >= Properties.Settings.Default.MaxLikePerHour && Properties.Settings.Default.MaxLikePerHour != 0)
            {
                logs.Add($"It`s time to have some rest. Rest time {Properties.Settings.Default.RestTimeMinutes} minutes", MessageType.Type.DEBUGINFO, this.GetType().Name);
                Delay = 0;
                double resttime = TimeSpan.FromMinutes(Properties.Settings.Default.RestTimeMinutes).TotalSeconds;
                while (mainVars.IsHashtagLikeInProgres == true && resttime > 0)
                {
                    await Task.Delay(1000);
                    resttime--;
                    _feedstats = du.StatsUpdate(_feedstats, this.GetType().Name, "REST", null, 0, 0, null, th.GetNormalTime(resttime));
                }
                TimeInit();
                _feedstats = du.StatsUpdate(_feedstats, this.GetType().Name, "WORKING", Properties.Settings.Default.FeedLikesTotalCount, 0, null, null, th.GetNormalTime(EndTime));
            }
        }

        protected bool InitCommonData(LikeViewModelBase likeModel)
        {
            Account = accountInfoHelper.GetMainAccount();
            if (Account != null)
            {
                accountInfoHelper.UpdateAccountStatus(Account, AccountStatus.Type.WORKING);
                mainVars.ChangeLikeProgressToTrue(likeModel);

                _feedstats = likeModel.LikeStats ?? new StatsModelBase();

                TimeInit();
                _feedstats = du.StatsUpdate(_feedstats, this.GetType().Name, "WORKING", Properties.Settings.Default.FeedLikesTotalCount, null, null, null, th.GetNormalTime(EndTime));
                return true;
            }
            else
                return false;
        }

        public void StopLike(LikeViewModelBase likeModel)
        {
            timerStop();
            timepass = 0;
            mainVars.ChangeLikeProgressToFalse(likeModel);
            _feedstats = helper.StatsReset();
            accountInfoHelper.UpdateAccountStatus(Account, AccountStatus.Type.REST);
            _feedstats = du.StatsUpdate(_feedstats, likeModel, "OFF", Properties.Settings.Default.FeedLikesTotalCount, 0, 0, "00:00:00", "00:00:00");
            likeModel.ButtonContent = "Start";
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
            userfeed.Clear();
            timer.Stop();
            timer.IsEnabled = false;
        }
        protected void timerTick(object sender, EventArgs e)
        {
            timepass++;
            _feedstats.TimeInWork = th.GetNormalTime(timepass);
            if (Delay > 0)
            {
                if (Delay > 1000)
                    Delay = Delay / 1000;

                Delay--;
                _feedstats = du.StatsUpdate(_feedstats, mainInstanse, null, null, null, Delay, null, null);
            }
        }
    }
}
