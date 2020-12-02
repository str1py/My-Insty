using Instagram_Assistant.Helpers;
using Instagram_Assistant.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Instagram_Assistant.ViewModel
{
    public class FeedLikePageViewModel : ViewModelBase, INotifyCollectionChanged
    {
        private static FeedLikePageViewModel feedinstance;
        public static FeedLikePageViewModel Instanse
        {
            get
            {
                if (feedinstance == null)
                {
                    feedinstance = new FeedLikePageViewModel();
                }
                return feedinstance;
            }
        }

        FeedLikeHelper feedlike = new FeedLikeHelper();
        MainVars mainVars = new MainVars();
        CommonHelper helper = new CommonHelper();

        private StatsModelBase feedstats;
        public StatsModelBase FeedStats
        {
            get { return feedstats; }
            set { 
                feedstats = value; 
                OnPropertyChanged(); 
            }
        }

        private ObservableCollection<ActionModel> feedLikeActions;
        public ObservableCollection<ActionModel> FeedLikeActions
        {
            get { return feedLikeActions; }
            set
            {
                feedLikeActions = value;
                OnPropertyChanged();
            }
        }


        public FeedLikePageViewModel()
        {
            ButtonContent = "Start";
            LastActionTextHelper = "No actions yet";
            FeedStats = new StatsModelBase
            {
                Count = helper.BigNumbersCutting(Properties.Settings.Default.FeedLikesTotalCount),
                SessionCount = "0",
                Status = "OFF",
                NextSessionIn = "00:00:00",
                TimeInWork = "00:00:00",
                NextIn = 0
            };       
        }

        private ICommand _startFeedLikeCommand;
        public ICommand StartFeedLikeCommand
        {
            get { return _startFeedLikeCommand ?? (_startFeedLikeCommand = new RelayCommand(p => StartLike())); }
        }


        private string _buttonContent;
        public string ButtonContent
        {
            get { return _buttonContent; }
            set { _buttonContent = value; OnPropertyChanged(); }
        }

        private string _lastActionTextHelper;
        public string LastActionTextHelper
        {
            get { return _lastActionTextHelper; }
            set { _lastActionTextHelper = value; OnPropertyChanged(); }
        }


        public async Task StartLike()
        {
            if (mainVars.IsFeedLikeInProgress == false)
            {
                LastActionTextHelper = "";
                   ButtonContent = "Stop";
                await feedlike.BeginFeedLike();
            }
            else
            {
                feedlike.StopFeedLike();
                ButtonContent = "Start";
            }
        }


        public event NotifyCollectionChangedEventHandler CollectionChanged = delegate { };
        public void OnCollectionChanged(NotifyCollectionChangedAction action)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action));
        }
    }
}
