using Instagram_Assistant.Enums;
using Instagram_Assistant.Helpers.Audience;
using Instagram_Assistant.Model;
using Instagram_Assistant.ViewModel.BaseModels;
using System.Threading.Tasks;
using System.Windows;

namespace Instagram_Assistant.ViewModel
{
    class HashtagAudiencePageViewModel : AudienceViewModelBase
    {
        private static HashtagAudiencePageViewModel hashtagAudienceInstance;
        public static HashtagAudiencePageViewModel Instance
        {
            get
            {
                if (hashtagAudienceInstance == null)
                    hashtagAudienceInstance = new HashtagAudiencePageViewModel();
                return hashtagAudienceInstance;
            }
        }

        private HashtagAudienceHelper htAudience;

        public HashtagAudiencePageViewModel()
        { 
            htAudience = new HashtagAudienceHelper(this);
            ButtonContent = "Start";
            AudienceProcess = new AudienceProcessModel("No actions yet", 0);
            LastActionTextHelper = "No actions yet";

            Stats = (new AudienceStatsModel
            {
                TechAccount = "n/a",
                Competitor = "n/a",
                Count = "",
                Status = AccountStatus.Type.OFF.ToString(),
                TimeInWork = "00:00:00"
            });
            ComboBoxSelectedIndex = 0;
        }

        public override async Task StartAudience()
        {
            if (mainVars.IsHashtagAudienceInProgress == false)
            {
                if (CollectFromList != null)
                {
                    ComboBoxEnable = false;
                    LastActionTextHelper = "";
                    ButtonContent = "Stop";
                    await htAudience.BeginCollectingAudience(ComboBoxSelectedIndex);
                }
                else 
                    MessageBox.Show("Seems to be there are no hashtags");
                
            }
            else
            {
                ComboBoxEnable = true;
                htAudience.StopCollectingAudience();
                ButtonContent = "Start";
            }
        }

    }
}
