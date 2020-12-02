using Instagram_Assistant.Helpers;
using Instagram_Assistant.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Dynamic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Instagram_Assistant.ViewModel
{
    public class DashboardPageViewModel : ViewModelBase, INotifyCollectionChanged
    {
        private static DashboardPageViewModel dashinstance;
        public static DashboardPageViewModel Instanse
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
            SelectedLikeView = FeedLikePageViewModel.Instanse;
            SelectedStoriesView = FeedStoriesPageViewModel.Instanse;
            UnfollowView = UnfollowPageViewModel.Instanse;
            AudienceView = AudiencePageViewModel.Instanse;
        }


        private object _selectedLikeView = 0;
        public object SelectedLikeView
        {
            get { return _selectedLikeView; }
            set { _selectedLikeView = value; OnPropertyChanged(); }
        }

        private int _item = 0;
        public int Item
        {
            get { return _item; }
            set
            {
                _item = value;

                SelectedLikeView = GetView(Item);
                OnPropertyChanged();
            }
        }

        public static object GetView(int index)
        {
            switch (index)
            {
                case 0:
                    return FeedLikePageViewModel.Instanse;
                case 1:
                    return FeedStoriesPageViewModel.Instanse;
            }
            return null;
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

                SelectedStoriesView = GetStoriesView(_storiesItem);
                OnPropertyChanged();
            }
        }

        public static object GetStoriesView(int index)
        {
            switch (index)
            {
                case 0:
                    return FeedStoriesPageViewModel.Instanse;
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
                switch (TabSelectedIndex)
                {
                    case 0:
                        GetView(0);
                        return;
                    case 2:
                        GetStoriesView(0);
                        return;
                    case 3:
                        UnfollowView = UnfollowPageViewModel.Instanse;
                        return;
                    case 5:
                        AudienceView = AudiencePageViewModel.Instanse;
                        return;


                }
            }
        }



        private object _unfollowView;
        public object UnfollowView
        {
            get { return _unfollowView; }
            set { _unfollowView = value; OnPropertyChanged(); }
        }

        //AUDIENCE

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
                        AudienceView = AudiencePageViewModel.Instanse;
                        return;
                    case 1:
                        AudienceView = FilterAudiencePageViewModel.Instanse;
                        return;
                }
            }
        }

        private object _audienceView;
        public object AudienceView
        {
            get { return _audienceView; }
            set { _audienceView = value; OnPropertyChanged(); }
        }


        public event NotifyCollectionChangedEventHandler CollectionChanged = delegate { };
        public void OnCollectionChanged(NotifyCollectionChangedAction action)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action));
        }
    }
}
