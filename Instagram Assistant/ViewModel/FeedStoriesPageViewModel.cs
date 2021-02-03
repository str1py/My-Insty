using Instagram_Assistant.Helpers;
using Instagram_Assistant.Helpers.Story;
using Instagram_Assistant.Model;
using Instagram_Assistant.ViewModel.BaseModels;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Instagram_Assistant.ViewModel
{
    class FeedStoriesPageViewModel: CommonViewModel
    {
        private static FeedStoriesPageViewModel storyFeedInstance;
        public static FeedStoriesPageViewModel Instance
        {
            get
            {
                if (storyFeedInstance == null)
                    storyFeedInstance = new FeedStoriesPageViewModel();
                return storyFeedInstance;
            }
        }

        StoriesFeedHelper story;

        public FeedStoriesPageViewModel()
        {
            story = new StoriesFeedHelper(this);
        }

        public override async Task Start()
        {
            if (mainVars.IsFeedStoriesWatching == false)
            {
                LastActionTextHelper = "";
                ButtonContent = "Stop";
                await story.BeginWatch();
            }
            else
            {
                story.Stop(this);
                ButtonContent = "Start";
            }
        }

    }
}
