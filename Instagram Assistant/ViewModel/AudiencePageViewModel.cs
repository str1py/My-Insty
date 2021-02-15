using Instagram_Assistant.Enums;
using Instagram_Assistant.Helpers;
using Instagram_Assistant.Model;
using Instagram_Assistant.ViewModel.BaseModels;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Instagram_Assistant.ViewModel
{
    class AudiencePageViewModel : AudienceViewModelBase
    {
        private static AudiencePageViewModel audienceInstance;
        public static AudiencePageViewModel Instance
        {
            get
            {
                if (audienceInstance == null)
                    audienceInstance = new AudiencePageViewModel();
                return audienceInstance;
            }
        }

        private AudienceHelper auhelper;
      
        public AudiencePageViewModel()
        {
            auhelper = new AudienceHelper(this);
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
            if (mainVars.IsAudienceInProgress == false)
            {
                if (CollectFromList != null)
                {
                    ComboBoxEnable = false;
                    LastActionTextHelper = "";
                    ButtonContent = "Stop";
                    await auhelper.BeginCollectingAudience(ComboBoxSelectedIndex);
                }else
                {
                    MessageBox.Show("Seems to be there are no competitors");
                }
            }
            else
            {
                ComboBoxEnable = true;
                auhelper.StopCollectingAudience();
                ButtonContent = "Start";
            }
        }

    }
}
