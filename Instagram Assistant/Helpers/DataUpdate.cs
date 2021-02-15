using Instagram_Assistant.Enums;
using Instagram_Assistant.Model;
using Instagram_Assistant.ViewModel;
using Instagram_Assistant.ViewModel.BaseModels;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Instagram_Assistant.Helpers
{
    class DataUpdate
    {
        //STATUS: OK
        private LogsPageViewModel logs = LogsPageViewModel.Instance;
        private ConvertHelper convert = new ConvertHelper();

        public void UpdateActions(ObservableCollection<ActionModel> actions, CommonViewModel _class, BitmapImage _accountImage, string _accountName, string _action, string _status = null, BitmapImage _postPreview = null)
        {
            try
            {
                var act = new ActionModel()
                {
                    AccountImage = _accountImage,
                    AccountName = _accountName,
                    PostImage = _postPreview,
                    Action = _action,
                    Status = _status,
                    Time = DateTime.Now
                };

                int maxfeedcount;
                if (_class is UnfollowPageViewModel || _class is SpyPageViewModel)
                    maxfeedcount = 50;
                else maxfeedcount = 5;

                if (actions.Count > maxfeedcount)
                    actions.RemoveAt(0);

                if (_class.Actions is null)
                {
                    _class.Actions = new ObservableCollection<ActionModel>();
                    _class.Actions.Add(act);
                }
                else
                    _class.Actions?.Add(act);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public StatsModelBase StatsReset()
        {
            var stats = new StatsModelBase
            {
                NextIn = 0,
                NextSessionIn = "00:00:00",
                Status = "OFF",
                TimeInWork = "00:00:00",
                SessionCount = "0",
                Count = Properties.Settings.Default.FeedLikesTotalCount.ToString()
            };
            return stats;
        }

        public StatsModelBase StatsUpdate(StatsModelBase _stats, CommonViewModel _class, string _status, int? _count, int? _sessioncount, int? _nextint, string _timeinwork, string _nextsessionin)
        {
            var stats = new StatsModelBase()
            {
                Status = _status ?? _stats.Status,
                SessionCount = convert.BigNumbersCutting(_sessioncount) ?? convert.BigNumbersCutting(_stats.SessionCount),
                NextSessionIn = _nextsessionin ?? _stats.NextSessionIn,
                NextIn = _nextint ?? _stats.NextIn,
                Count = convert.BigNumbersCutting(_count) ?? convert.BigNumbersCutting(_stats.Count),
                TimeInWork = _timeinwork ?? _stats.TimeInWork
            };

            _class.Stats = stats;

            return stats;
        }
       
        public void ClearActions(CommonViewModel _class)
        {
            _class.Actions?.Clear();
        }

        //AUDIENCE
        public void UpdateProcess(string message, AudienceViewModelBase model,double? folowerscount, double? followerspassed, MessageType.Type type, string _class)
        {
            if (folowerscount == null || followerspassed == null)
            {
                model.AudienceProcess = new AudienceProcessModel(message, 0);
            }
            else
            {
                double? pr = (followerspassed / folowerscount) * 100;
                model.AudienceProcess = new AudienceProcessModel($"{message} ({followerspassed}/{folowerscount})", pr);
            }
            if(type != MessageType.Type.HIDDEN & type !=MessageType.Type.DEBUGINFO)
                logs.Add($"{message}", type, _class);
        }

        public AudienceStatsModel AudienceStatsUpdate(AudienceStatsModel _stats, AudienceViewModelBase model, string _status, int? _count,  string _timeinwork, string _mainacc, string _competitor)
        {
            var stats = new AudienceStatsModel()
            {
                Status = _status ?? _stats.Status,
                Count = convert.BigNumbersCutting(_count) ?? convert.BigNumbersCutting(_stats.Count),
                TimeInWork = _timeinwork ?? _stats.TimeInWork,
                Competitor = _competitor ?? _stats.Competitor,
                TechAccount = _mainacc ?? _stats.TechAccount
            };
            model.Stats = stats;
            return stats;
        }

        //FILTER AUDIENCE
        public void UpdateFilterAudienceActions(string accountName, string fullname, long accid, string phone, string email, int type, string category, string city, bool iscity, int mediacount,
                    int followerscount, string bio, bool hl, string image, bool stopword, bool goWords, string act)
        {
            AudienceActionModel action = new AudienceActionModel(accountName, fullname, accid, phone, email, type, category, city, iscity, mediacount, followerscount, bio, hl, image, stopword, goWords, act);
            FilterAudiencePageViewModel.Instance.AudienceActions.Add(action);
        }


    }
}
