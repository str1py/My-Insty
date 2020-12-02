using Instagram_Assistant.Enums;
using Instagram_Assistant.Model;
using Instagram_Assistant.Model.Stories;
using Instagram_Assistant.ViewModel;
using InstagramApiSharp.API;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Instagram_Assistant.Helpers.Story
{
    class StoriesFeedHelper
    {
        //STATUS: +-OK
        #region VARS INIT
        private IInstaApi Account;
        private DateTime StartTime { get; set; }
        private DateTime EndTime { get; set; }
        //Delay per like for stats overview
        private int Delay { get; set; }
        private int timepass { get; set; } = 0;

        private List<StoryModel> userstoriesfeed = new List<StoryModel>(); //List For Feeds from user Instagram
        private ObservableCollection<ActionModel> storiesactions = new ObservableCollection<ActionModel>(); //What app doing 
        private StatsModelBase _storiesstats; //for overview - likes and time stats
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

        public StoriesFeedHelper()
        {

        }

        public async Task<bool> BeginWatchStories()
        {
            Account = accountInfoHelper.GetMainAccount();
            accountInfoHelper.UpdateAccountStatus(Account,AccountStatus.Type.WORKING);
            if (Account != null)
            {
                mainVars.IsFeedStoriesWatching = true;
                _storiesstats = FeedStoriesPageViewModel.Instanse.StoryFeedStats ?? new StatsModelBase();
                TimeInit();
                timerStart();//запуск таймера
                _storiesstats = du.StatsUpdate(_storiesstats, this.GetType().Name, "ON", Properties.Settings.Default.FeedStoriesTotalCount, 0, null, null, th.GetNormalTime(EndTime));

                do
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
                            _storiesstats = du.StatsUpdate(_storiesstats, this.GetType().Name, "REST", null, 0, 0, null, th.GetNormalTime(sec--));
                        } while (sec > 0);
                        TimeInit();
                        _storiesstats = du.StatsUpdate(_storiesstats, this.GetType().Name, "ON", null, 0, 0, null, null);
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
                            _storiesstats = du.StatsUpdate(_storiesstats, this.GetType().Name, "REST", null, 0, 0, null, th.GetNormalTime(resttime));
                        }
                        TimeInit();
                        _storiesstats = du.StatsUpdate(_storiesstats, this.GetType().Name, "ON", null, 0, 0, null, null);
                    }
                    #endregion

                    var stories = await GetStories();
                    if (stories != null && stories?.Count != 0)
                    {
                        foreach (var story in stories)
                        {
                            if (mainVars.IsFeedStoriesWatching == true)
                            {
                                try
                                {
                                    story.DeviceTimestamp = DateTime.UtcNow;
                                    await Account.StoryProcessor.MarkStoryAsSeenAsync(story.StoryId, th.GetInixTime());

                                    Properties.Settings.Default.FeedStoriesTotalCount++;
                                    Properties.Settings.Default.Save();
                                    _storiesstats = du.StatsUpdate(_storiesstats, this.GetType().Name, null, Properties.Settings.Default.FeedStoriesTotalCount, Convert.ToInt32(_storiesstats.SessionCount) + 1, null, null, null);

                                    logs.Add($"Story by {story.user} was successfully watched.", MessageType.Type.STORY, this.GetType().Name);
                                    Delay = helper.GetStoryDelay();//ставим задержку 

                                    du.UpdateActions(storiesactions, this.GetType().Name, 5, story.userPict, story.user, story.postPreview, "Eye");

                                    await Task.Delay(Delay);
                                }
                                catch (Exception e)
                                {
                                    logs.Add($"Story by {story.user} wasn`t watched. {e.Message}", MessageType.Type.ERROR, this.GetType().Name);
                                }
                            }
                        }
                    }
                    else
                    {
                        logs.Add($"Seems to be there are no stories. Waiting 30 sec and try one again", MessageType.Type.STORY, this.GetType().Name);
                        await Task.Delay(30000);
                    }
                } while (mainVars.IsFeedStoriesWatching == true);

                StopStoryWatch();
                return false;
            }
            else
            {
                MessageBox.Show($"There is no main account. Please log in and try again", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                StopStoryWatch();
                return false;
            }
        }
        public void StopStoryWatch()
        {
            accountInfoHelper.UpdateAccountStatus(Account, AccountStatus.Type.REST);
            mainVars.IsFeedStoriesWatching = false;
            timerStop();
            _storiesstats = du.StatsUpdate(_storiesstats, this.GetType().Name, "OFF", Properties.Settings.Default.FeedStoriesTotalCount, 0, 0, "00:00:00", "00:00:00");
            FeedStoriesPageViewModel.Instanse.ButtonContent = "Start";
        }

        public async Task<List<StoryModel>> GetStories()
        {

            userstoriesfeed.Clear();
            var result = await Account.StoryProcessor.GetStoryFeedAsync();

            var storyFeed = result.Value;
            foreach (var feedItem in storyFeed.Items)
            {
                if (feedItem.Seen == 0)
                {
                    foreach (var story in feedItem.Items)
                    {
                        story.DeviceTimestamp = DateTime.Now;
                        try
                        {
                            userstoriesfeed.Add(new StoryModel
                            {
                                user = feedItem.User.UserName,
                                userPict = imghelp.GetImage(feedItem.User.ProfilePicUrl),
                                postPreview = imghelp.GetImage(story.ImageList[0].Uri) ?? new BitmapImage(new Uri("Images/instagram.png", UriKind.Relative)),
                                StoryId = story.Id,
                                ExpiringAt = story.ExpiringAt,
                                DeviceTimestamp = story.DeviceTimestamp
                            });
                        }
                        catch (Exception e) { logs.Add(e.ToString(), MessageType.Type.ERROR, this.GetType().Name); }
                    }
                }
            }

            if (userstoriesfeed.Count == 0)
            {
                var dee = GetUserIdsWithStories(result);
                var stories = await GetUsersStories(dee);

                foreach (var story in stories)
                {
                    story.DeviceTimestamp = DateTime.Now;
                    try
                    {
                        userstoriesfeed.Add(new StoryModel
                        {
                            user = story.User.UserName,
                            userPict = imghelp.GetImage(story.User.ProfilePicUrl),
                            postPreview = imghelp.GetImage(story.ImageList[0].Uri) ?? new BitmapImage(new Uri("Images/instagram.png", UriKind.Relative)),
                            StoryId = story.Id,
                            ExpiringAt = story.ExpiringAt,
                            DeviceTimestamp = story.DeviceTimestamp
                        });
                    }
                    catch (Exception e) { logs.Add(e.ToString(), MessageType.Type.ERROR, this.GetType().Name); }
                }
            }

            logs.Add($"Got {userstoriesfeed.Count} story reels.", MessageType.Type.DEBUGINFO, this.GetType().Name);
            return userstoriesfeed;
        }
        private List<long> GetUserIdsWithStories(IResult<InstaStoryFeed> feed)
        {
            List<long> userids = new List<long>();
            foreach (var ids in feed.Value.Items)
            {
                
                userids.Add(long.Parse(ids.Id));
            }

            return userids;
        }
        private async Task<List<InstaStoryItem>> GetUsersStories(List<long> ids)
        {
            List<InstaStoryItem> stories = new List<InstaStoryItem>();
            List<IResult<InstaReelFeed>> _story = new List<IResult<InstaReelFeed>>();

            int count = 0;

            foreach (var id in ids)
            {
                if(count > 50)
                        break;

                _story.Clear();
                var c = await Account.StoryProcessor.GetUserStoryFeedAsync(id);
                _story.Add(c);

                FeedStoriesPageViewModel.Instanse.LastActionTextHelper = $"Searching stories";
                du.ClearActions(this.GetType().Name);

                foreach (var story in _story)
                {
                    if (story?.Value.Seen == 0)
                    {
                        FeedStoriesPageViewModel.Instanse.LastActionTextHelper = $"Getting stories by {story.Value.User.UserName}";
                        foreach (var item in _story)
                        {
                            foreach (var storiesitems in item.Value.Items)
                            {
                                stories.Add(storiesitems);
                                count++;
                            }
                        }
                    }else
                    {
                        int rnd = new Random().Next(0, 3);
                        switch (rnd)
                        {
                            case 0:
                                FeedStoriesPageViewModel.Instanse.LastActionTextHelper = $"Seem to be we saw stories by {story.Value.User.UserName} already";
                                break;
                            case 1:
                                FeedStoriesPageViewModel.Instanse.LastActionTextHelper = $"Hmmmm... stories by {story.Value.User.UserName} we already saw";
                                break;
                            case 2:
                                FeedStoriesPageViewModel.Instanse.LastActionTextHelper = $"I think stories by {story.Value.User.UserName} we already watched";
                                break;
                        }

                    }
                }
            }
            FeedStoriesPageViewModel.Instanse.LastActionTextHelper = $"";
            return stories;
        }

        private void TimeInit()
        {
            StartTime = DateTime.Now;
            EndTime = helper.GetNextTime(StartTime);
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
            _storiesstats.TimeInWork = th.GetNormalTime(timepass);
            if (Delay >= 0)
            {
                if (Delay > 1000)
                    Delay = Delay / 1000;

                if (Delay != 0)
                 Delay--;

                _storiesstats = du.StatsUpdate(_storiesstats, this.GetType().Name, null,null,null, Delay,null,null);
            }
        }
    }
}
