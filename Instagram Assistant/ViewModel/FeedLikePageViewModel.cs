using Instagram_Assistant.Helpers;
using Instagram_Assistant.Model;
using Instagram_Assistant.ViewModel.BaseModels;
using System.Threading.Tasks;

namespace Instagram_Assistant.ViewModel
{
    public class FeedLikePageViewModel : CommonViewModel
    {
        private static FeedLikePageViewModel feedinstance;
        public static FeedLikePageViewModel Instance
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

        private FeedLikeHelper feedlike;

        public FeedLikePageViewModel()
        {
            feedlike = new FeedLikeHelper(this);
            Stats.Count = Properties.Settings.Default.FeedLikesTotalCount.ToString();
        }

        public override async Task Start()
        {
            if (mainVars.IsFeedLikeInProgress == false)
            {
                LastActionTextHelper = "";
                ButtonContent = "Stop";
                await feedlike.BeginLike();
            }
            else
            {
                feedlike.Stop(this);
                ButtonContent = "Start";
            }
        }

    }
}
