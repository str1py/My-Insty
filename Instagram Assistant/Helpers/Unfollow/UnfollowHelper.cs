using Instagram_Assistant.Enums;
using Instagram_Assistant.Model;
using Instagram_Assistant.ViewModel;
using InstagramApiSharp;
using InstagramApiSharp.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Instagram_Assistant.Helpers.Unfollow
{
    class UnfollowHelper
    {
        #region VARS INIT
        private IInstaApi Account;
        private DateTime StartTime { get; set; }
        private DateTime EndTime { get; set; }
        //Delay per like for stats overview
        private int Delay { get; set; }
        private int timepass { get; set; } = 0;
        private string username { get; set; }
        private string fileName = @"followinglist";
        private string path { get; set; }
        private List<UnfollowModel> userunfollow = new List<UnfollowModel>(); //List For Feeds from user Instagram
        private ObservableCollection<ActionModel> unfollowactions = new ObservableCollection<ActionModel>(); //What app doing 
        private StatsModelBase _unfollowstats;
        #endregion

        #region HELPERS init
        private DispatcherTimer timer = new DispatcherTimer();
        private CommonHelper helper = new CommonHelper();
        private LogsPageViewModel logs = LogsPageViewModel.Instanse;
        private MainVars mainVars = new MainVars();
        private Limits limits = new Limits();
        private TimeHelper th = new TimeHelper();
        private ImageHelpers imageHelper = new ImageHelpers();
        private DataUpdate du = new DataUpdate();
        private AccountInfoHelper accountInfoHelper = new AccountInfoHelper();
        private TextFileHelper textFileHelper = new TextFileHelper();
        #endregion

        public UnfollowHelper()
        {
            path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        }

        private async Task<List<UnfollowModel>> GetUserFollowing()
        {
            Account = accountInfoHelper.GetMainAccount();
            int count = 0;
            if (Account != null)
            {
                userunfollow.Clear();
                username = Account.GetLoggedUser().LoggedInUser.UserName;
                fileName = fileName + "-" + username + ".txt";

                var unfollow = await textFileHelper.GetExistUnfollowList(fileName);

                if(unfollow != null)
                {
                    return unfollow;
                }
                else
                {
                    File.Create(path + "\\" + fileName);
                    try
                    {
                        var result = await Account.UserProcessor.GetUserFollowingAsync(username, PaginationParameters.MaxPagesToLoad(1));
                        if (result.Succeeded == true)
                        {
                            using (StreamWriter file = new StreamWriter(path + "\\" + fileName))
                            {
                                foreach (var user in result.Value)
                                {
                                    await Task.Factory.StartNew(() =>
                                    {
                                        count++;
                                        UnfollowPageViewModel.Instanse.LastActionTextHelper = $"Getting followers... ({count})";
                                    });
                                    await Task.Delay(100);
                                    userunfollow.Add(new UnfollowModel
                                    {
                                        user = user.UserName,
                                        isfollowback = false,
                                        userid = user.Pk,
                                        userPict = imageHelper.GetImage(user.ProfilePicture)
                                    });
                                    await file.WriteLineAsync($"{user.UserName};{user.Pk};{user.ProfilePicture}");
                                }
                            }
                            return userunfollow;
                        }
                        else
                        {
                            logs.Add($"{result.Info}", MessageType.Type.ERROR, this.GetType().Name);
                            return null;
                        }
                    }
                    catch (Exception e)
                    {
                        logs.Add($"{e.Message}", MessageType.Type.ERROR, this.GetType().Name);
                        return null;
                    }
                }
            }
            else
            {
                MessageBox.Show($"There is no main account. Please log in and try again", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                StopUnfollow();
                return null;
            }
        }
        private async Task<bool> IsUserFollowBack(long userid)
        {
            try
            {
                var result = await Account.UserProcessor.GetFriendshipStatusAsync(userid);
                if (!result.Succeeded)
                {
                    logs.Add($"{result.Info.Message}", MessageType.Type.ERROR, this.GetType().Name);
                    StopUnfollow();
                    return false;
                }
                else
                {
                    return result.Value.FollowedBy;
                }
            }
            catch (Exception e) 
            { 
                logs.Add($"{e.Message}", MessageType.Type.ERROR, this.GetType().Name); 
                return false; 
            }
        }
        public async Task BeginUnfollow()
        {
            Account = accountInfoHelper.GetMainAccount();
            accountInfoHelper.UpdateAccountStatus(Account, AccountStatus.Type.WORKING);
            mainVars.IsUnfollowInProgress = true;

            TimeInit();
            timerStart();

            _unfollowstats = UnfollowPageViewModel.Instanse.UnfollowStats ?? new StatsModelBase();
            _unfollowstats = du.StatsUpdate(_unfollowstats, this.GetType().Name, "ON", Properties.Settings.Default.UnfollowCount, 0, null, null, th.GetNormalTime(EndTime));

            var following = await GetUserFollowing();

            if(following != null && mainVars.IsUnfollowInProgress == true)
            {
                logs.Add($"Find {following.Count} users you follow", MessageType.Type.UNFOLLOW, this.GetType().Name);
                foreach (var user in following)
                {
                    #region WorkTimeCheck
                    var wtlc = limits.WorkTimeLimitCheck();
                    if (wtlc != TimeSpan.Zero)
                    {
                        logs.Add($"It`s time to have some rest. Rest time until {Properties.Settings.Default.RestDateTo.Hour}:00. Total rest time {wtlc.Hours}h {wtlc.Minutes}m", MessageType.Type.DEBUGINFO, this.GetType().Name);
                        double sec = wtlc.TotalSeconds;
                        do
                        {
                            await Task.Delay(1000);
                            _unfollowstats = du.StatsUpdate(_unfollowstats, this.GetType().Name, "REST", Properties.Settings.Default.UnfollowCount, 0, 0, null, th.GetNormalTime(sec--));
                        } while (sec > 0);
                        TimeInit();
                        _unfollowstats = du.StatsUpdate(_unfollowstats, this.GetType().Name, "ON", Properties.Settings.Default.UnfollowCount, 0, null, null, th.GetNormalTime(EndTime));
                    }
                    #endregion

                    #region HourPassedCheck
                    if (EndTime < DateTime.Now)
                    {
                        logs.Add($"It`s time to have some rest. Rest time {Properties.Settings.Default.RestTimeMinutes} minutes", MessageType.Type.DEBUGINFO, this.GetType().Name);
                        Delay = 0;
                        double resttime = TimeSpan.FromMinutes(Properties.Settings.Default.RestTimeMinutes).TotalSeconds;
                        while (mainVars.IsUnfollowInProgress == true && resttime > 0)
                        {
                            await Task.Delay(1000);
                            resttime--;
                            _unfollowstats = du.StatsUpdate(_unfollowstats, this.GetType().Name, "REST", Properties.Settings.Default.UnfollowCount, 0, 0, null, th.GetNormalTime(resttime));
                        }
                        TimeInit();
                        _unfollowstats = du.StatsUpdate(_unfollowstats, this.GetType().Name, "ON", Properties.Settings.Default.UnfollowCount, 0, null, null, th.GetNormalTime(EndTime));
                    }
                    #endregion

                    if (Properties.Settings.Default.IsUnfollowIfFollowing == true)
                    {
                        var result = await Account.UserProcessor.UnFollowUserAsync(user.userid);
                        if (result.Succeeded)                       
                            await Unfollow(user, "Unfollow All Mode");  
                    }
                    else
                    {
                        if (await IsUserFollowBack(user.userid) == false)
                        {
                            var result = await Account.UserProcessor.UnFollowUserAsync(user.userid);
                            if (result.Succeeded)                       
                                await Unfollow(user, "Not Following");      
                            else
                                logs.Add($"Error while unfollow {user.user}", MessageType.Type.ERROR, this.GetType().Name);
                            
                        }
                        else
                        {
                            du.UpdateActions(unfollowactions, this.GetType().Name, 50, user.userPict, user.user, "Follower", "UserCheck");
                            logs.Add($"{user.user} is your follower", MessageType.Type.UNFOLLOW, this.GetType().Name);
                            UpdateFileAndList(user.userid);
                            await Task.Delay(5000);
                        }
                    }
                }
                StopUnfollow();
                File.Delete(path + "\\" + fileName);
                logs.Add($"Unfollow list ended. Unfollow txt file has been deleted. To make a new one press start.", MessageType.Type.UNFOLLOW, this.GetType().Name);
            }
            else
            {
                logs.Add($"There is no user list loaded. Unfollow was stopped", MessageType.Type.UNFOLLOW, this.GetType().Name);
                StopUnfollow();
            }
        }

        public void StopUnfollow()
        {
            accountInfoHelper.UpdateAccountStatus(Account, AccountStatus.Type.REST);
            mainVars.IsUnfollowInProgress = false;
            timerStop();
            _unfollowstats = du.StatsUpdate(_unfollowstats, this.GetType().Name, "OFF", Properties.Settings.Default.UnfollowCount, 0, 0, "00:00:00", "00:00:00");
            UnfollowPageViewModel.Instanse.ButtonContent = "Start";
        }
        private void TimeInit()
        {
            StartTime = DateTime.Now;
            EndTime = helper.GetNextTime(StartTime); // time to next session after four
        }

        private void UpdateFileAndList(long removestr)
        {

            List<string> linesList = File.ReadAllLines(path +"\\"+ fileName).ToList();

            foreach(var line in linesList)
            {
                bool cont = line.Contains(removestr.ToString());
                if (cont)
                {
                    linesList.Remove(line);
                    break;
                }
            }

            File.WriteAllLines((path + "\\" + fileName), linesList.ToArray());

        }
        private async Task Unfollow(UnfollowModel user,string _status)
        {
            logs.Add($"Unfollow from {user.user}", MessageType.Type.UNFOLLOW, this.GetType().Name);

            Properties.Settings.Default.UnfollowCount++;
            Properties.Settings.Default.Save();
            du.StatsUpdate(_unfollowstats, this.GetType().Name, null, Properties.Settings.Default.UnfollowCount, Convert.ToInt32(_unfollowstats.SessionCount) + 1, null, null, null);

            du.UpdateActions(unfollowactions, this.GetType().Name, 50, user.userPict, user.user, _status, "UserTimes");
            await Task.Delay(1000);

            Delay = helper.GetUnfollowDelay();
            logs.Add($"Next unfollow in {Delay / 1000} seconds", MessageType.Type.DEBUGINFO, this.GetType().Name);

            await Task.Delay(Delay);
            UpdateFileAndList(user.userid);
        }

        private void timerStart()
        {
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            timer.Start();
        }
        private void timerStop()
        {
            timer.Stop();
        }
        private void timerTick(object sender, EventArgs e)
        {

            timepass++;
            _unfollowstats.TimeInWork = th.GetNormalTime(timepass);
            if (Delay > 0)
            {
                if (Delay > 1000)
                    Delay = Delay / 1000;

                Delay--;   //работает с запазданием на 4 сек      
                _unfollowstats = du.StatsUpdate(_unfollowstats, this.GetType().Name, null, Properties.Settings.Default.UnfollowCount, Convert.ToInt32(_unfollowstats.SessionCount), Delay, null, null);
            }
        }
    }
}
