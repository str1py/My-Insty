using Instagram_Assistant.Model;
using Instagram_Assistant.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace Instagram_Assistant.Helpers
{
    class DataUpdate
    {
        //STATUS: OK
        private LogsPageViewModel logs = LogsPageViewModel.Instanse;

        public void UpdateActions(ObservableCollection<ActionModel> actions,string _class,int _maxfeedcount, BitmapImage _accountImage, string _accountName, BitmapImage _postPreview, string _action)
        {
            actions.Add(new ActionModel
            {
                AccountImage = _accountImage,
                AccountName = _accountName,
                PostImage = _postPreview,
                Action = _action,
                Time = DateTime.Now
            });
            if (actions.Count > _maxfeedcount)
                actions.RemoveAt(0);

            if(_class == "FeedLikeHelper")
                FeedLikePageViewModel.Instanse.FeedLikeActions = actions;
            else if(_class == "StoriesFeedHelper")
                FeedStoriesPageViewModel.Instanse.StoryFeedActions = actions;
            else if (_class == "UnfollowHelper")
                UnfollowPageViewModel.Instanse.UnfollowActions = actions;

        }
        public void UpdateActions(ObservableCollection<ActionModel> actions, string _class, int _maxfeedcount, BitmapImage _accountImage, string _accountName, string _status, string _action)
        {
            actions.Add(new ActionModel
            {
                AccountImage = _accountImage,
                AccountName = _accountName,
                Action = _action,
                Status = _status,
                Time = DateTime.Now
            });
            if (actions.Count > _maxfeedcount)
                actions.RemoveAt(0);

            if (_class == "FeedLikeHelper")
                FeedLikePageViewModel.Instanse.FeedLikeActions = actions;
            else if (_class == "StoriesFeedHelper")
                FeedStoriesPageViewModel.Instanse.StoryFeedActions = actions;
            else if (_class == "UnfollowHelper")
                UnfollowPageViewModel.Instanse.UnfollowActions = actions;

        }

        //AUDIENCE
        public void UpdateProcess(string message, double? folowerscount, double? followerspassed, MessageType.Type type)
        {
            if (folowerscount == null || followerspassed == null)
            {
                AudiencePageViewModel.Instanse.AudienceProcess = new AudienceProcessModel(message, 0);
            }
            else
            {
                double? pr = (followerspassed / folowerscount) * 100;
                AudiencePageViewModel.Instanse.AudienceProcess = new AudienceProcessModel($"{message} ({followerspassed}/{folowerscount})", pr);
            }
            if (type != MessageType.Type.HIDDEN)
                logs.Add($"{message}", type, this.GetType().Name);
        }


        //FILTER AUDIENCE
        public void UpdateFilterAudienceActions(string accountName, string fullname, long accid, string phone, string email, int type, string category, string city, bool iscity, int mediacount,
int followerscount, string bio, bool hl, string image, bool stopword, bool goWords, string act)
        {
            AudienceActionModel action = new AudienceActionModel(accountName, fullname, accid, phone, email, type, category, city, iscity, mediacount, followerscount, bio, hl, image, stopword, goWords, act);
            FilterAudiencePageViewModel.Instanse.AudienceActions.Add(action);
        }

        public void ClearActions(string _class)
        {
            if (_class == "FeedLikeHelper" )
                FeedLikePageViewModel.Instanse.FeedLikeActions?.Clear();
            else if (_class == "StoriesFeedHelper")
                FeedStoriesPageViewModel.Instanse.StoryFeedActions?.Clear();
            else if (_class == "UnfollowHelper")
                UnfollowPageViewModel.Instanse.UnfollowActions?.Clear();
        }

        public StatsModelBase StatsUpdate(StatsModelBase _stats, string _class, string _status, int? _count, int? _sessioncount, int? _nextint, string _timeinwork, string _nextsessionin)
        {
            var helper = new CommonHelper();

            var stats = new StatsModelBase()
            {
                Status = _status ?? _stats.Status,
                SessionCount = helper.BigNumbersCutting(_sessioncount) ?? helper.BigNumbersCutting(_stats.SessionCount),
                NextSessionIn = _nextsessionin ?? _stats.NextSessionIn,
                NextIn = _nextint ?? _stats.NextIn,
                Count = helper.BigNumbersCutting(_count) ?? helper.BigNumbersCutting(_stats.Count),
                TimeInWork = _timeinwork ?? _stats.TimeInWork
            };

            if (_class == "FeedLikeHelper")
                FeedLikePageViewModel.Instanse.FeedStats = stats;
            else if (_class == "StoriesFeedHelper")
                FeedStoriesPageViewModel.Instanse.StoryFeedStats = stats;
            else if (_class == "UnfollowHelper")
                UnfollowPageViewModel.Instanse.UnfollowStats = stats;

            return stats;
        }
    }
}
