using Instagram_Assistant.Enums;
using Instagram_Assistant.Helpers;
using Instagram_Assistant.Model;
using Instagram_Assistant.Model.Spy;
using Instagram_Assistant.ViewModel.BaseModels;
using System.Threading.Tasks;

namespace Instagram_Assistant.ViewModel
{
    class SpyPageViewModel: CommonViewModel
    {
        private static SpyPageViewModel spystance;
        public static SpyPageViewModel Instance
        {
            get
            {
                if (spystance == null)
                    spystance = new SpyPageViewModel();
                return spystance;
            }
        }
        SpyHelper spyHelper;

        public SpyPageViewModel()
        {
            spyHelper = new SpyHelper(this);
            stats = new SpyStatsModel()
            {
                Status = AccountStatus.Type.OFF.ToString(),
                NextSessionIn = "00:00:00",
                StoriesCount = "0",
                PostsCount = "0",
                TimeInWork = "00:00:00",
                MainAccount = "n/a",
                TechAccount = "n/a"
            };
            ComboBoxSelectedIndex = 0;
        }

        private int comboBoxSelectedIndex;
        public int ComboBoxSelectedIndex
        {
            get { return comboBoxSelectedIndex; }
            set
            {
                comboBoxSelectedIndex = value;
                OnPropertyChanged();
            }
        }

        private SpyStatsModel stats;
        public new SpyStatsModel Stats
        {
            get { return stats; }
            set
            {
                stats = value;
                OnPropertyChanged();
            }
        }


        public override async Task Start()
        {
            if (mainVars.IsSpyInProgress == false)
            {
                LastActionTextHelper = "";
                ButtonContent = "Stop";
                await spyHelper.StartSpy(ComboBoxSelectedIndex);
            }
            else
            {
                spyHelper.StopSpy();
                ButtonContent = "Start";
            }
        }
    }
}
