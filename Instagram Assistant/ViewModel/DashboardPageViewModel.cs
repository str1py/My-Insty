using System.Collections.Specialized;

namespace Instagram_Assistant.ViewModel
{
    public class DashboardPageViewModel : ViewModelBase, INotifyCollectionChanged
    {
        private static DashboardPageViewModel dashinstance;
        public static DashboardPageViewModel Instance
        {
            get
            {
                if (dashinstance == null)
                {
                    dashinstance = new DashboardPageViewModel();
                }
                return dashinstance;
            }
        }

  
        public DashboardPageViewModel()
        {
            SelectedView = FeedLikePageViewModel.Instance;
            SelectedLikeView = FeedLikePageViewModel.Instance;
            SelectedStoriesView = FeedStoriesPageViewModel.Instance;
            UnfollowView = UnfollowPageViewModel.Instance;
            AudienceView = AudiencePageViewModel.Instance;
        }

       
        //MAIN
        public static object GetView(int index)
        {
            switch (index)
            {
                case 0:
                    return FeedLikePageViewModel.Instance;
                case 1:
                    return FeedStoriesPageViewModel.Instance;
            }
            return null;
        }

        private int _tabSelectedIndex = 0;
        public int TabSelectedIndex
        {
            get { return _tabSelectedIndex; }
            set
            {
                _tabSelectedIndex = value;
                OnPropertyChanged();

                switch(_tabSelectedIndex)
                {
                    case 0:
                        SelectedLikeView = FeedLikePageViewModel.Instance;
                        return;
                    case 2:
                        SelectedStoriesView = FeedStoriesPageViewModel.Instance;
                        return;
                    case 3:
                        UnfollowView = UnfollowPageViewModel.Instance;
                        return;
                    case 5:
                        AudienceView = AudiencePageViewModel.Instance;
                        return;
                }
            }
        }

        private object _selectedView = 0;
        public object SelectedView
        {
            get { return _selectedView; }
            set { _selectedView = value; OnPropertyChanged(); }
        }


        //LIKE
        private object _selectedLikeView;
        public object SelectedLikeView
        {
            get { return _selectedLikeView; }
            set { _selectedLikeView = value; OnPropertyChanged(); }
        }

        private int _likeItem = 0;
        public int LikeItem
        {
            get { return _likeItem; }
            set
            {
                _likeItem = value;

                switch (_likeItem)
                {
                    case 0:
                        SelectedLikeView = FeedLikePageViewModel.Instance;
                        break;
                    case 1:
                        SelectedLikeView = GeoLikePageViewModel.Instance;
                        break;
                    case 2:
                        SelectedLikeView = HashtagLikePageViewModel.Instance;
                        break;
                    default:
                        SelectedLikeView = FeedLikePageViewModel.Instance;
                        break;
                }
                OnPropertyChanged();
            }
        }


        //STORIES 
        private object _selectedStoriesView = 0;
        public object SelectedStoriesView
        {
            get { return _selectedStoriesView; }
            set { _selectedStoriesView = value; OnPropertyChanged(); }
        }

        private int _storiesItem = 0;
        public int StoriesItem
        {
            get { return _storiesItem; }
            set
            {
                _storiesItem = value;
                switch (_storiesItem)
                {
                    case 0:
                        SelectedStoriesView = FeedStoriesPageViewModel.Instance;
                        break;
                    case 1:
                        SelectedStoriesView = GeoStoriesWatchViewModel.Instance;
                        break;
                    default:
                        SelectedStoriesView = FeedStoriesPageViewModel.Instance;
                        break;
                }
                OnPropertyChanged();
            }
        }

        //UNFOLLOW
        private object _unfollowView;
        public object UnfollowView
        {
            get { return _unfollowView; }
            set { _unfollowView = value; OnPropertyChanged(); }
        }


        //AUDIENCE
        private object _audienceView;
        public object AudienceView
        {
            get { return _audienceView; }
            set { _audienceView = value; OnPropertyChanged(); }
        }

        private int _audienceItem;
        public int AudienceItem
        {
            get { return _audienceItem; }
            set
            {
                _audienceItem = value;
                OnPropertyChanged();
                switch (AudienceItem)
                {
                    case 0:
                        AudienceView = AudiencePageViewModel.Instance;
                        break;
                    case 1:
                        //Hashtag
                        AudienceView = HashtagAudiencePageViewModel.Instance;
                        break;
                    case 2:
                        //Geo
                        AudienceView = GeoAudiencePageViewModel.Instance;
                        break;
                    case 3:
                        AudienceView = FilterAudiencePageViewModel.Instance;
                        break;
                    case 4:
                        AudienceView = SpyPageViewModel.Instance;
                        break;
                }
            }
        }



        public event NotifyCollectionChangedEventHandler CollectionChanged = delegate { };
        public void OnCollectionChanged(NotifyCollectionChangedAction action)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action));
        }
    }
}
