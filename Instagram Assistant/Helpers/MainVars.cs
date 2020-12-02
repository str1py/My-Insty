using Instagram_Assistant.ViewModel;


namespace Instagram_Assistant.Helpers
{
    internal class MainVars: ViewModelBase
    {
        //STATUS: OK
        private static bool isFeedLikeInProgress = false;
        private static bool isFeedStoriesWatching = false;
        private static bool isUnfollowInProgress = false;
        private static bool isAudienceInProgress = false;
        private static bool isAudienceFilterInProgress = false;

        public bool IsUnfollowInProgress
        {
            get { return isUnfollowInProgress; }
            set
            {
                isUnfollowInProgress = value;

                if (isUnfollowInProgress == true)
                {
                    IsFeedStoriesWatching = false;
                    IsFeedLikeInProgress = false;
                }

                OnPropertyChanged();
            }
        }

        public bool IsFeedLikeInProgress
        {
            get { return isFeedLikeInProgress; }
            set 
            { 
                isFeedLikeInProgress = value;

                if (isFeedLikeInProgress == true)
                {
                    IsFeedStoriesWatching = false;
                    IsUnfollowInProgress = false;
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

                if (isFeedStoriesWatching == true)
                {
                    IsFeedLikeInProgress = false;
                    IsUnfollowInProgress = false;
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
        
    }
}
