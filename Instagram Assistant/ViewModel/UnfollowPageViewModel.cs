using Instagram_Assistant.Helpers.Unfollow;
using Instagram_Assistant.Model;
using Instagram_Assistant.ViewModel.BaseModels;
using System.Threading.Tasks;


namespace Instagram_Assistant.ViewModel
{
    class UnfollowPageViewModel: CommonViewModel
    {
        private static UnfollowPageViewModel unfollowinstanse;
        public static UnfollowPageViewModel Instance
        {
            get
            {
                if (unfollowinstanse == null)
                {
                    unfollowinstanse = new UnfollowPageViewModel();
                }
                return unfollowinstanse;
            }
        }

        UnfollowHelper unfollowHelper;

        public UnfollowPageViewModel()
        {
            unfollowHelper = new UnfollowHelper(this);
            ButtonContent = "Start";
            LastActionTextHelper = "No actions yet";
            Stats = new StatsModelBase
            {
                Count = convert.BigNumbersCutting(Properties.Settings.Default.UnfollowCount),
                SessionCount = "0",
                Status = "OFF",
                NextSessionIn = "00:00:00",
                TimeInWork = "00:00:00",
                NextIn = 0
            };
        }

        public override async Task Start()
        {
            if (mainVars.IsUnfollowInProgress == false)
            {
                LastActionTextHelper = "";
                ButtonContent = "Stop";
                await unfollowHelper.BeginUnfollow();
            }
            else
            {
                unfollowHelper.Stop(this);
                ButtonContent = "Start";
            }
        }
    }
}
