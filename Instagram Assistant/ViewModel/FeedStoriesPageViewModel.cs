using Instagram_Assistant.Helpers;
using Instagram_Assistant.Helpers.Story;
using Instagram_Assistant.Model;
using Instagram_Assistant.Model.Stories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Instagram_Assistant.ViewModel
{
    class FeedStoriesPageViewModel:ViewModelBase, INotifyCollectionChanged
    {
        private static FeedStoriesPageViewModel storyFeedInstance;
        public static FeedStoriesPageViewModel Instanse
        {
            get
            {
                if (storyFeedInstance == null)
                    storyFeedInstance = new FeedStoriesPageViewModel();
                return storyFeedInstance;
            }
        }

        private StoriesFeedHelper story = new StoriesFeedHelper();
        private MainVars mainVars = new MainVars();
        CommonHelper helper = new CommonHelper();
        public FeedStoriesPageViewModel()
        {
            ButtonContent = "Start";
            LastActionTextHelper = "No actions yet";
            StoryFeedStats = new StatsModelBase
            {
                Count = helper.BigNumbersCutting(Properties.Settings.Default.FeedStoriesTotalCount),
                SessionCount = "0",
                Status = "OFF",
                NextSessionIn = "00:00:00",
                TimeInWork = "00:00:00",
                NextIn = 0
            };
        }

        private StatsModelBase storyfeedstats;
        public StatsModelBase StoryFeedStats
        {
            get { return storyfeedstats; }
            set { storyfeedstats = value; OnPropertyChanged(); }
        }

        private ObservableCollection<ActionModel> storyFeedActions;
        public ObservableCollection<ActionModel> StoryFeedActions
        {
            get { return storyFeedActions; }
            set { storyFeedActions = value; OnPropertyChanged(); }
        }

        private ICommand _startStoriesWatchCommand;
        public ICommand StartStoriesWatchCommand
        {
            get { return _startStoriesWatchCommand ?? (_startStoriesWatchCommand = new RelayCommand(p => StartWatch())); }
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

        public async Task StartWatch()
        {
            if (mainVars.IsFeedStoriesWatching == false)
            {
                LastActionTextHelper = "";
                ButtonContent = "Stop";
                await story.BeginWatchStories();
            }
            else
            {
                story.StopStoryWatch();
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
