using Instagram_Assistant.Enums;
using Instagram_Assistant.Helpers.Audience;
using Instagram_Assistant.Model;
using Instagram_Assistant.ViewModel.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Instagram_Assistant.ViewModel
{
    class GeoAudiencePageViewModel : AudienceViewModelBase
    {
        private static GeoAudiencePageViewModel geoAudienceInstance;
        public static GeoAudiencePageViewModel Instance
        {
            get
            {
                if (geoAudienceInstance == null)
                    geoAudienceInstance = new GeoAudiencePageViewModel();
                return geoAudienceInstance;
            }
        }

        private GeoAudienceHelper geoAudience;

        public GeoAudiencePageViewModel()
        {
            geoAudience = new GeoAudienceHelper(this);
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
            if (mainVars.IsGeoAudienceInProgress == false)
            {
                if (CollectFromList != null)
                {
                    ComboBoxEnable = false;
                    LastActionTextHelper = "";
                    ButtonContent = "Stop";
                    await geoAudience.BeginCollectingAudience(ComboBoxSelectedIndex);
                }
                else
                    MessageBox.Show("Seems to be there are no hashtags");

            }
            else
            {
                ComboBoxEnable = true;
                geoAudience.StopCollectingAudience();
                ButtonContent = "Start";
            }
        }
    }
}
