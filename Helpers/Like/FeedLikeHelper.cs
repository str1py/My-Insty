using Instagram_Assistant.Enums;
using Instagram_Assistant.Model;
using Instagram_Assistant.ViewModel;
using InstagramApiSharp.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Instagram_Assistant.Helpers
{
    class FeedLikeHelper
    {
        //STATUS: +-OK

        #region VARS INIT
        private IInstaApi Account;
        private DateTime StartTime { get; set; }
        private DateTime EndTime { get; set; }

        //Delay per like for stats overview
        private int Delay { get; set; }
        private int timepass { get; set; } = 0;

        private List<FeedModel> userfeed = new List<FeedModel>(); //List For Feeds from user Instagram
        private ObservableCollection<ActionModel> feedactions = new ObservableCollection<ActionModel>(); //What app doing 
        private StatsModelBase _feedstats; //for overview - likes and time stats
        private List<string> seenMediasMassive = new List<string>();
        #endregion

        #region HELPERS INIT
        private DispatcherTimer timer = new DispatcherTimer();
        private ImageHelpers imghelp = new ImageHelpers();
        private CommonHelper helper = new CommonHelper();
        private TimeHelper th = new TimeHelper();
        private LogsPageViewModel logs = LogsPageViewModel.Instanse;
        private MainVars mainVars = new MainVars();
        private Limits limits = new Limits();
        private DataUpdate du = new DataUpdate();
        private AccountInfoHelper accountInfoHelper = new AccountInfoHelper();
        #endregion
       
        public FeedLikeHelper()
        {
        }

        private async Task<List<FeedModel>> GetFeed()
        {
            Account = accountInfoHelper.GetMainAccount();
            if (Account != null)
            {
                //clear previous posts
                userfeed.Clear();
                //get new one
                var feed = await Account.FeedProcessor.GetUserTimelineFeedAsync(null, seenMediasMassive.ToArray()) ?? null;

                if (feed != null)
                {
                    logs.Add($"Find {feed.Value.MediaItemsCount} posts to like!", MessageType.Type.DEBUGINFO, this.GetType().Name);
                    //Every posts
                    foreach (var media in feed.Value.Medias)
                    {
                        if (media.HasLiked != true && seenMediasMassive.Contains(media.InstaIdentifier) != true)
                        {
                            try
                            {
                                //ADD if images or video
                                userfeed.Add(new FeedModel
                                {
                                    user = media.User.UserName,
                                    userPict = imghelp.GetImage(media.User.ProfilePicture) ?? new BitmapImage(new Uri("Images/instagram.png", UriKind.Relative)),
                                    postPreview = imghelp.GetImage(media.Images[0].Uri),
                                    InstaIdentifier = media.InstaIdentifier,
                                    MediaType = media.MediaType.ToString()
                                });
                            }
                            catch
                            {
                                //ADD if images or Carousel
                                userfeed.Add(new FeedModel
                                {
                                    user = media.User.UserName,
                                    userPict = imghelp.GetImage(media.User.ProfilePicture) ?? new BitmapImage(new Uri("Images/instagram.png", UriKind.Relative)),
                                    postPreview = imghelp.GetImage(media.Carousel[0].Images[0].Uri),
                                    InstaIdentifier = media.InstaIdentifier,
                                    MediaType = media.MediaType.ToString()
                                });
                            }
                        }
                    }
                    return userfeed;
                }
                else
                {
                    logs.Add($"Error while loading posts", MessageType.Type.ERROR, this.GetType().Name);
                    return null;
                }
            }
            else
            {
                MessageBox.Show($"There is no main account. Please log in and try again", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                StopFeedLike();
                return null;
            }

        }
        public async Task<bool> BeginFeedLike()
        {
            Account = accountInfoHelper.GetMainAccount();
            if (Account != null)
            {
                accountInfoHelper.UpdateAccountStatus(Account, AccountStatus.Type.WORKING);
                mainVars.IsFeedLikeInProgress = true;

                _feedstats = FeedLikePageViewModel.Instanse.FeedStats ?? new StatsModelBase();
                TimeInit();

                timerStart();
                _feedstats = du.StatsUpdate(_feedstats, this.GetType().Name, "ON", Properties.Settings.Default.FeedLikesTotalCount, null, null, null, th.GetNormalTime(EndTime));

                do //do while feedlike is on and time isnt endtime
                {
                    //get user feed
                    var feed = await GetFeed();
                    if (feed != null)
                    {
                        foreach (var a in feed)
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
                                    _feedstats = du.StatsUpdate(_feedstats, this.GetType().Name, "REST", null, 0, 0, null, th.GetNormalTime(sec--));
                                } while (sec > 0);
                                TimeInit();
                                _feedstats = du.StatsUpdate(_feedstats, this.GetType().Name, "ON", Properties.Settings.Default.UnfollowCount, 0, null, null, th.GetNormalTime(EndTime));
                            }
                            #endregion

                            #region HourPassedCheck
                            if (EndTime < DateTime.Now)
                            {
                                logs.Add($"It`s time to have some rest. Rest time {Properties.Settings.Default.RestTimeMinutes} minutes", MessageType.Type.DEBUGINFO, this.GetType().Name);
                                Delay = 0;
                                double resttime = TimeSpan.FromMinutes(Properties.Settings.Default.RestTimeMinutes).TotalSeconds;
                                while (mainVars.IsFeedStoriesWatching == true && resttime > 0)
                                {
                                    await Task.Delay(1000);
                                    resttime--;
                                    _feedstats = du.StatsUpdate(_feedstats, this.GetType().Name, "REST", null, 0, 0, null, th.GetNormalTime(resttime));
                                }
                                TimeInit();
                                _feedstats = du.StatsUpdate(_feedstats, this.GetType().Name, "ON", Properties.Settings.Default.UnfollowCount, 0, null, null, th.GetNormalTime(EndTime));
                            }
                            #endregion

                            if (mainVars.IsFeedLikeInProgress == true)
                            {
                                try
                                {
                                    //skip post if true
                                    if (SkipPost() == true || seenMediasMassive.Contains(a.InstaIdentifier))
                                    {
                                        logs.Add($"Post by {a.user} was SKIPED", MessageType.Type.LIKE, this.GetType().Name);
                                        Delay = 5000;
                                        du.UpdateActions(feedactions, this.GetType().Name, 5, a.userPict, a.user, a.postPreview, "Ban");
                                    }
                                    //like
                                    else
                                    {
                                        await Account.MediaProcessor.LikeMediaAsync(a.InstaIdentifier); //ставим лайк

                                        Properties.Settings.Default.FeedLikesTotalCount++;
                                        Properties.Settings.Default.Save();
                                        _feedstats = du.StatsUpdate(_feedstats, this.GetType().Name, null, Properties.Settings.Default.FeedLikesTotalCount, int.Parse(_feedstats.SessionCount) + 1, null, null, null);

                                        logs.Add($"Post by {a.user} was successfully LIKED", MessageType.Type.LIKE, this.GetType().Name);
                                        Delay = helper.GetLikeDelay();//ставим задержку - сделать норм

                                        du.UpdateActions(feedactions, this.GetType().Name, 5, a.userPict, a.user, a.postPreview, "Heart");
                                        seenMediasMassive.Add(a.InstaIdentifier);
                                    }
                                    await Task.Delay(Delay);
                                }
                                catch (Exception e) { logs.Add($"Post by {a.user} CAN`T be liked! ERROR: {e.Message}", MessageType.Type.ERROR, this.GetType().Name); }
                            }
                            else
                            {
                                StopFeedLike();
                                return false;
                            }
                        }
                    }
                } while (mainVars.IsFeedLikeInProgress == true);
                StopFeedLike();
                return false;
            }
            else
            {
                MessageBox.Show($"There is no main account. Please log in and try again", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                StopFeedLike();
                return false;
            }
        }
        public void StopFeedLike()
        {
            accountInfoHelper.UpdateAccountStatus(Account, AccountStatus.Type.REST);
            mainVars.IsFeedLikeInProgress = false;
            timerStop();
            _feedstats = du.StatsUpdate(_feedstats, this.GetType().Name, "OFF", Properties.Settings.Default.FeedLikesTotalCount, 0, 0, "00:00:00","00:00:00") ;
            FeedLikePageViewModel.Instanse.ButtonContent = "Start";
        }
        public bool SkipPost()
        {
            Random random = new Random();
            int a = random.Next(0, 100);
            if (a < Properties.Settings.Default.SkipChance)
                return true;
            else
                return false;
        }

        private void TimeInit()
        {
            StartTime = DateTime.Now;
            EndTime = helper.GetNextTime(StartTime); // time to next session after four
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
            _feedstats.TimeInWork = th.GetNormalTime(timepass);
            if (Delay > 0)
            {
                if (Delay > 1000)
                    Delay = Delay / 1000;

                Delay--;
                _feedstats = du.StatsUpdate(_feedstats, this.GetType().Name, null, null, null, Delay, null, null);
            }
        }

    }
}
