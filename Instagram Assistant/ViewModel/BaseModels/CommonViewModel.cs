using Instagram_Assistant.Helpers;
using Instagram_Assistant.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Instagram_Assistant.ViewModel.BaseModels
{
    public class CommonViewModel : ViewModelBase
    {      
        //STATUS: OK
        protected LogsPageViewModel logs = LogsPageViewModel.Instance;
        protected MainVars mainVars = new MainVars();
        protected ConvertHelper convert = new ConvertHelper();

        public CommonViewModel()
        {
            ButtonContent = "Start";
            LastActionTextHelper = "No actions yet";
            Stats = new StatsModelBase
            {
                Count = convert.BigNumbersCutting(0),
                SessionCount = "0",
                Status = "OFF",
                NextSessionIn = "00:00:00",
                TimeInWork = "00:00:00",
                NextIn = 0
            };
        }

        private ObservableCollection<ActionModel> actions;
        public ObservableCollection<ActionModel> Actions
        {
            get { return actions; }
            set
            {
                actions = value;
                OnPropertyChanged();
            }
        }

        private StatsModelBase stats;
        public StatsModelBase Stats
        {
            get { return stats; }
            set
            {
                stats = value;
                OnPropertyChanged();
            }
        }

        private string buttonContent;
        public string ButtonContent
        {
            get { return buttonContent; }
            set { buttonContent = value; OnPropertyChanged(); }
        }

        private string lastActionTextHelper;
        public string LastActionTextHelper
        {
            get { return lastActionTextHelper; }
            set { lastActionTextHelper = value; OnPropertyChanged(); }
        }


        private ICommand startCommand;
        public ICommand StartCommand
        {
            get
            {
                return startCommand ?? (startCommand = new RelayCommand(p => Start()));
            }
        }

        public virtual Task Start() { return null; }
    }
}
