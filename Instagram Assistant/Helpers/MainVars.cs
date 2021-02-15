using Instagram_Assistant.ViewModel;
using Instagram_Assistant.ViewModel.BaseModels;

namespace Instagram_Assistant.Helpers
{
    public class MainVars: ViewModelBase
    {
        //STATUS: OK
        private static bool isFeedLikeInProgress = false;
        private static bool isGeoLikeInProgress = false;
        private static bool isHashtagLikeInProgres = false;

        private static bool isFeedStoriesWatching = false;
        private static bool isGeoStoriesWatching = false;

        private static bool isUnfollowInProgress = false;
        private static bool isAudienceInProgress = false;
        private static bool isAudienceFilterInProgress = false;
        private static bool isHashtagAudienceInProgress = false;
        private static bool isGeoAudienceInProgress = false;

        private static bool isSPyInProgress = false;

        public bool IsFeedLikeInProgress
        {
            get { return isFeedLikeInProgress; }
            set
            {
                isFeedLikeInProgress = value;

                if (value == true)
                {
                    isGeoLikeInProgress = false;
                    isHashtagLikeInProgres = false;
                    isFeedStoriesWatching = false;
                    isGeoStoriesWatching = false;
                    isUnfollowInProgress = false;
                    isAudienceInProgress = false;
                }

                OnPropertyChanged();
            }
        }
        public bool IsGeoLikeInProgress
        {
            get { return isGeoLikeInProgress; }
            set
            {
                isGeoLikeInProgress = value;

                if (value == true)
                {
                    isFeedLikeInProgress = false;
                    isHashtagLikeInProgres = false;
                    isFeedStoriesWatching = false;
                    isGeoStoriesWatching = false;
                    isUnfollowInProgress = false;
                    isAudienceInProgress = false;
                }

                OnPropertyChanged();
            }
        }
        public bool IsHashtagLikeInProgres
        {
            get { return isHashtagLikeInProgres; }
            set
            {
                isHashtagLikeInProgres = value;

                if (value == true)
                {
                    isFeedLikeInProgress = false;
                    isGeoLikeInProgress = false;
                    isFeedStoriesWatching = false;
                    isGeoStoriesWatching = false;
                    isUnfollowInProgress = false;
                    isAudienceInProgress = false;
                }
                OnPropertyChanged();
            }
        }

        public bool IsFeedStoriesWatching
        {
            get { return isFeedStoriesWatching; }
            set
            {
                isFeedStoriesWatching = value;

                if (value == true)
                {
                    isFeedLikeInProgress = false;
                    isGeoLikeInProgress = false;
                    isHashtagLikeInProgres = false;
                    isGeoStoriesWatching = false;
                    isUnfollowInProgress = false;
                    isAudienceInProgress = false;
                }
                OnPropertyChanged();
            }
        }  
        public bool IsGeoStoriesWatching
        {
            get { return isGeoStoriesWatching; }
            set
            {
                isGeoStoriesWatching = value;
                if(value == true)
                {
                    isFeedLikeInProgress = false;
                    isGeoLikeInProgress = false;
                    isHashtagLikeInProgres = false;
                    isFeedStoriesWatching = false;
                    isUnfollowInProgress = false;
                    isAudienceInProgress = false;
                }
                OnPropertyChanged();
            }
        }


        public bool IsUnfollowInProgress
        {
            get { return isUnfollowInProgress; }
            set
            {
                isUnfollowInProgress = value;
                if (value == true)
                {
                    isFeedLikeInProgress = false;
                    isGeoLikeInProgress = false;
                    isHashtagLikeInProgres = false;
                    isFeedStoriesWatching = false;
                    isGeoStoriesWatching = false;
                    isAudienceInProgress = false;
                }

                OnPropertyChanged();
            }
        }
        public bool IsAudienceInProgress
        {
            get { return isAudienceInProgress; }
            set
            {
                isAudienceInProgress = value;
                if(value == true)
                {
                    isFeedLikeInProgress = false;
                    isGeoLikeInProgress = false;
                    isHashtagLikeInProgres = false;
                    isFeedStoriesWatching = false;
                    isGeoStoriesWatching = false;
                    isUnfollowInProgress = false;
                }
                OnPropertyChanged();
            }
        }
        public bool IsAudienceFilterInProgress
        {
            get { return isAudienceFilterInProgress; }
            set
            {
                isAudienceFilterInProgress = value;
                OnPropertyChanged();
            }
        }
        public bool IsHashtagAudienceInProgress
        {
            get { return isHashtagAudienceInProgress; }
            set { isHashtagAudienceInProgress = value; }
        }
        public bool IsGeoAudienceInProgress
        {
            get { return isGeoAudienceInProgress; }
            set { isGeoAudienceInProgress = value; }
        }



        public bool IsSpyInProgress
        {
            get { return isSPyInProgress; }
            set
            {
                isSPyInProgress = value;
                OnPropertyChanged();
            }
        }


        public void ChangeProgressToTrue(CommonViewModel model)
        {
            if (model is FeedLikePageViewModel)
                IsFeedLikeInProgress = true;
            else if (model is GeoLikePageViewModel)
                IsGeoLikeInProgress = true;
            else if (model is HashtagLikePageViewModel)
                IsHashtagLikeInProgres = true;
            else if (model is FeedStoriesPageViewModel)
                IsFeedStoriesWatching = true;
            else if (model is GeoStoriesWatchViewModel)
                IsGeoStoriesWatching = true;
            else if (model is UnfollowPageViewModel)
                IsUnfollowInProgress = true;
            else if (model is SpyPageViewModel)
                IsSpyInProgress = true;
        }

        public void ChangeProgressToFalse(CommonViewModel model)
        {
            if (model is FeedLikePageViewModel)
                IsFeedLikeInProgress = false;
            else if (model is GeoLikePageViewModel)
                IsGeoLikeInProgress = false;
            else if (model is HashtagLikePageViewModel)
                IsHashtagLikeInProgres = false;
            else if (model is FeedStoriesPageViewModel)
                IsFeedStoriesWatching = false;
            else if (model is GeoStoriesWatchViewModel)
                IsGeoStoriesWatching = false;
            else if (model is UnfollowPageViewModel)
                IsUnfollowInProgress = false;
            else if (model is SpyPageViewModel)
                IsSpyInProgress = false;

        }

        public int GetTotalCountFromProperties(CommonViewModel model)
        {
            var prop = Properties.Settings.Default;
            if (model is FeedLikePageViewModel)
                return prop.FeedLikesTotalCount;
            else if (model is GeoLikePageViewModel)
                return prop.GeoTotalLikeCount;
            else if (model is HashtagLikePageViewModel)
                return prop.HashatagTotalLikeCount;
            else if (model is FeedStoriesPageViewModel)
                return prop.FeedStoriesTotalCount;
            else if (model is GeoStoriesWatchViewModel)
                return prop.GeoTotalStoriesCount;
            else if (model is UnfollowPageViewModel)
                return prop.UnfollowCount;
            else return 0;
        }

        public void IncrementActionsCount(CommonViewModel model)
        {
            var prop = Properties.Settings.Default;
            if (model is FeedLikePageViewModel)
                prop.FeedLikesTotalCount++;
            else if (model is GeoLikePageViewModel)
                prop.GeoTotalLikeCount++;
            else if (model is HashtagLikePageViewModel)
                prop.HashatagTotalLikeCount++;
            else if (model is FeedStoriesPageViewModel)
                prop.FeedStoriesTotalCount++;
            else if (model is GeoStoriesWatchViewModel)
                prop.GeoTotalStoriesCount++;
            else if (model is UnfollowPageViewModel)
                prop.UnfollowCount++;

            prop.Save();
        }

        public bool GetProgressStatus(CommonViewModel model)
        {
            var prop = Properties.Settings.Default;
            if (model is FeedLikePageViewModel)
                return IsFeedLikeInProgress;
            else if (model is GeoLikePageViewModel)
                return IsGeoLikeInProgress;
            else if (model is HashtagLikePageViewModel)
                return IsHashtagLikeInProgres;
            else if (model is FeedStoriesPageViewModel)
                return IsFeedStoriesWatching;
            else if (model is GeoStoriesWatchViewModel)
                return IsGeoStoriesWatching;
            else if (model is UnfollowPageViewModel)
                return IsUnfollowInProgress;
            else if (model is SpyPageViewModel)
                return IsSpyInProgress;
            else return false;
        }

        public int GetMaxCount(CommonViewModel model)
        {
            var prop = Properties.Settings.Default;
            if (model is FeedLikePageViewModel)
                return prop.MaxLikePerHour;
            else if (model is GeoLikePageViewModel)
                return prop.MaxLikePerHour;
            else if (model is HashtagLikePageViewModel)
                return prop.MaxLikePerHour;
            else if (model is FeedStoriesPageViewModel)
                return prop.MaxStoriesPerHour;
            else if (model is GeoStoriesWatchViewModel)
                return prop.MaxStoriesPerHour;
            else if (model is UnfollowPageViewModel)
                return prop.MaxUnfollowPerHour;
            else return 0;
        }

    }
}
