using Instagram_Assistant.Model;
using Instagram_Assistant.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Instagram_Assistant.Helpers
{
    class FeedLikeHelper :ViewModelBase
    {
        private List<FeedModel> userfeed = new List<FeedModel>();
        private ObservableCollection<FeedLikeModel> feedactions = new ObservableCollection<FeedLikeModel>();

        private FeedLikeStatsModel _feedstats;

        private DispatcherTimer timer = new DispatcherTimer();
        private ImageHelpers imghelp = new ImageHelpers();
        private CommonHelper helper = new CommonHelper();
        private TimeHelper th = new TimeHelper();

        private LogsPageViewModel logs = LogsPageViewModel.Instanse;

        private int timepass { get; set; } = 0;
        private int delay;
        public int Delay
        {
            get { return delay; }
            set { delay = value; OnPropertyChanged(); }
        }

        private async Task<List<FeedModel>> GetFeed()
        {
            userfeed.Clear();

            var feed = await LogInHelper._instaApi.FeedProcessor.GetUserTimelineFeedAsync(null) ?? null;

            if (feed != null)
            {
                logs.Add($"Find {feed.Value.MediaItemsCount} posts to like!", MessageType.Type.DEBUGINFO);

                for (int i = 0; i < feed.Value.MediaItemsCount; i++)
                {
                    if (feed.Value.Medias[i].HasLiked != true)
                        try
                        {
                            userfeed.Add(new FeedModel
                            {
                                user = feed.Value.Medias[i].User.UserName,
                                userPict = imghelp.GetImage(feed.Value.Medias[i].User.ProfilePicture) ?? new BitmapImage(new Uri("Images/instagram.png", UriKind.Relative)),
                                postPreview = imghelp.GetImage(feed.Value.Medias[i].Images[0].Uri) ,
                                InstaIdentifier = feed.Value.Medias[i].InstaIdentifier,
                                MediaType = feed.Value.Medias[i].MediaType.ToString()
                            });
                        }
                        catch
                        {
                            userfeed.Add(new FeedModel
                            {
                                user = feed.Value.Medias[i].User.UserName,
                                userPict = imghelp.GetImage(feed.Value.Medias[i].User.ProfilePicture) ?? new BitmapImage(new Uri("Images/instagram.png", UriKind.Relative)),
                                postPreview = imghelp.GetImage(feed.Value.Medias[i].Carousel[0].Images[0].Uri),
                                InstaIdentifier = feed.Value.Medias[i].InstaIdentifier,
                                MediaType = feed.Value.Medias[i].MediaType.ToString()
                            });
                        }

                }
                return userfeed;
            }
            else
            {
                logs.Add($"Error while loading posts", MessageType.Type.ERROR);
                return null;
            }
        }

        public async Task<bool> BeginFeedLike()
        {
            CommonHelper.isFeedLikeInProgress = true;

            _feedstats = FeedLikePageViewModel.Instanse.FeedStats ?? new FeedLikeStatsModel();
            helper.startTime = DateTime.Now;
            helper.endTime = GetEndTime(helper.startTime,Properties.Settings.Default.Activity);
            helper.nextStartTime = helper.GetNextTime(helper.startTime, Properties.Settings.Default.Activity);

            timerStart();//запуск таймера


            if (Properties.Settings.Default.LikeGoal == 0)
                _feedstats.LikeGoal = "∞";
            else
                _feedstats.LikeGoal = Properties.Settings.Default.LikeGoal.ToString();

            do //делаем показапущено и время окончания меньше времени сейчас
            {
                FeedStatsUpdate("ON", Properties.Settings.Default.FeedLikesTotalCount, null, null, null, null, th.GetNormalTime(helper.nextStartTime));

                var feed = await GetFeed(); //получаем записи
                if (feed != null) //если все нормально и записи есть
                {
                    foreach (var a in feed)//каждую запись
                    {
                        //Проверка лайков
                        if (Properties.Settings.Default.LikeGoal != 0 && _feedstats.SessionLikes > Properties.Settings.Default.LikeGoal)
                            StopFeedLike();

                        //Проверка времени
                        TimeSpan dateTime = TimeSpan.Parse(_feedstats.TimeInWork);
                        if (Properties.Settings.Default.HourGoal != 0 && dateTime.TotalHours > Properties.Settings.Default.HourGoal)
                            StopFeedLike();

                        if (CommonHelper.isFeedLikeInProgress == true)
                        {
                            try
                            {
                                if (SkipPost() == true) //скипаем запись если вернулось true
                                {
                                    logs.Add($"Post by {a.user} was SKIPED", MessageType.Type.LIKE);
                                    Delay = 5000; //ставим задержку
                                    AddToFeedActions(a.userPict, a.user, a.postPreview, "Ban");
                                }
                                else //ставим лайк
                                {
                                    await LogInHelper._instaApi.MediaProcessor.LikeMediaAsync(a.InstaIdentifier); //ставим лайк

                                    Properties.Settings.Default.FeedLikesTotalCount++;
                                    Properties.Settings.Default.Save();
                                    FeedStatsUpdate(null, Properties.Settings.Default.FeedLikesTotalCount, _feedstats.SessionLikes + 1, null, null, null, null);

                                    logs.Add($"Post by {a.user} was successfully LIKED", MessageType.Type.LIKE);
                                    Delay = helper.GetLikeDelay();//ставим задержку - сделать норм

                                    AddToFeedActions(a.userPict, a.user, a.postPreview, "Heart");
                                }


                                if (helper.nextStartTime < DateTime.Now) //если время activity прошло то ждем некст сессии
                                {
                                    helper.passedActivities++;

                                    if (helper.passedActivities > Properties.Settings.Default.Activity) //Если активитис больше нужного
                                        {
                                        logs.Add($"Rest for next session", MessageType.Type.DEBUGINFO);
                                        Delay = 0;
                                        FeedStatsUpdate("REST", null, 0, null, null, null, null);

                                        while (CommonHelper.isFeedLikeInProgress == true && helper.endTime > DateTime.Now)//ждем некс сессии
                                        {
                                            await Task.Delay(1000);
                                            var rest = helper.nextStartTime - DateTime.Now;
                                            FeedStatsUpdate("REST", null, 0, null, null, null, th.GetNormalTime(rest));
                                        }
                                        logs.Add($"New session will statrt soon!", MessageType.Type.DEBUGINFO);
                                    }
                                    else //переход к следующий активити
                                    {
                                        logs.Add($"Rest for next activity", MessageType.Type.DEBUGINFO);
                                        Delay = 0;
                                        FeedStatsUpdate("REST", null, 0, null, null, null, null);

                                        while (CommonHelper.isFeedLikeInProgress == true && helper.nextStartTime > DateTime.Now)//ждем некс сессии
                                        {
                                            await Task.Delay(1000);
                                            var rest = helper.nextStartTime - DateTime.Now;
                                            FeedStatsUpdate("REST", null, 0, null, null, null, th.GetNormalTime(rest));
                                        }
                                    }

                                    helper.startTime = DateTime.Now;
                                    helper.endTime = GetEndTime(helper.startTime, Properties.Settings.Default.Activity);
                                    helper.nextStartTime = helper.GetNextTime(helper.startTime, Properties.Settings.Default.Activity);
                                    FeedStatsUpdate("ON", Properties.Settings.Default.FeedLikesTotalCount, null, null, null, null, th.GetNormalTime(helper.nextStartTime));
                                }
                                else
                                {
                                    logs.Add($"Next like in {TimeSpan.FromMilliseconds(Delay).TotalSeconds} sec", MessageType.Type.DEBUGINFO);
                                    await Task.Delay(Delay);//производим задержку
                                }

                            }
                            catch (Exception e) { logs.Add($"Post by {a.user} CAN`T be liked! ERROR: {e.Message}", MessageType.Type.ERROR); }
                        }
                        else
                        {
                            StopFeedLike();
                            return false;
                        }
                    }
                }
            } while (CommonHelper.isFeedLikeInProgress == true || helper.endTime > DateTime.Now);

            
            return false;
        }

        public void StopFeedLike()
        {
            CommonHelper.isFeedLikeInProgress = false;
            timerStop();
            FeedStatsUpdate("OFF", Properties.Settings.Default.FeedLikesTotalCount, 0, 0, "-", "00:00:00","00:00:00") ;
            FeedLikePageViewModel.Instanse.ButtonContent = "Start";
        }

        private DateTime GetEndTime(DateTime start, int activities)
        {
            if (activities == 1)
                return start.AddMinutes(30);
            else if (activities == 2)
                return start.AddMinutes(20);
            else if (activities == 3)
                return start.AddMinutes(15);
            else
                return start.AddMinutes(15);
        }


        private void AddToFeedActions(BitmapImage _accountImage, string _accountName, BitmapImage _postImage,string _action)
        {
            feedactions.Add(new FeedLikeModel
            {
                AccountImage = _accountImage,
                AccountName = _accountName,
                PostImage = _postImage,
                Action = _action,
                likeTime = DateTime.Now
            });
            if (feedactions.Count > 5)
                feedactions.RemoveAt(0);

            FeedLikePageViewModel.Instanse.FeedLikeActions = feedactions;
        }
        private void FeedStatsUpdate(string _status, int? _likes, int? _sessionlikes, int? _nextlikein, string _likegoal, string _timeinwork, string _nextsessionin)
        {
            var _feed = _feedstats;
            _feedstats = new FeedLikeStatsModel(){
                Status = _status?? _feed.Status,
                SessionLikes = _sessionlikes ?? _feed.SessionLikes,
                NextSessionIn = _nextsessionin ?? _feed.NextSessionIn,
                NextLikeIn = _nextlikein ?? _feed.NextLikeIn,
                LikeGoal = _likegoal ?? _feed.LikeGoal,
                Likes = _likes ?? _feed.Likes,
                TimeInWork = _timeinwork ?? _feed.TimeInWork
            };
            FeedLikePageViewModel.Instanse.FeedStats = _feedstats;
        }

        private bool SkipPost()
        {
            Random random = new Random();
            int a = random.Next(0, 100);
            if (a < Properties.Settings.Default.SkipChance)
                return true;
            else
                return false;
        }
        private bool isHourPass()
        {
            var a = DateTime.Now - helper.startTime;
            if (a.TotalHours < 1.0)
                return false;
            else
                return true;
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

                Delay--;   //работает с запазданием на 4 сек      
                FeedStatsUpdate(null, null, null, Delay, null, null, null);
            }
        }

    }
}
