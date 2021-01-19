using Instagram_Assistant.Helpers;
using Instagram_Assistant.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Instagram_Assistant.ViewModel.BaseModels
{
    public abstract class LikeViewModelBase: ViewModelBase
    {
        protected LogsPageViewModel logs = LogsPageViewModel.Instanse;
        protected MainVars mainVars = new MainVars();
        protected CommonHelper helper = new CommonHelper();

        private ObservableCollection<ActionModel> _likeActions;
        public ObservableCollection<ActionModel> LikeActions
        {
            get { return _likeActions; }
            set
            {
                _likeActions = value;
                OnPropertyChanged();
            }
        }

        private StatsModelBase _likestats;
        public StatsModelBase LikeStats
        {
            get { return _likestats; }
            set
            {
                _likestats = value;
                OnPropertyChanged();
            }
        }

        private string _buttonContent;
        public string ButtonContent
        {
            get { return _buttonContent; }
            set { _buttonContent = value; OnPropertyChanged(); }
        }

        private string _lastActionTextHelper;
        public string LastActionTextHelper
        {
            get { return _lastActionTextHelper; }
            set { _lastActionTextHelper = value; OnPropertyChanged(); }
        }


        public virtual Task StartLike() { return null; }

    }
}
