using Instagram_Assistant.Helpers;
using Instagram_Assistant.Helpers.Unfollow;
using Instagram_Assistant.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Instagram_Assistant.ViewModel
{
    class UnfollowPageViewModel: ViewModelBase, INotifyCollectionChanged
    {
        private static UnfollowPageViewModel unfollowinstanse;
        public static UnfollowPageViewModel Instanse
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

        UnfollowHelper unfollowHelper = new UnfollowHelper();
        MainVars mainVars = new MainVars();
        CommonHelper helper = new CommonHelper();

        private StatsModelBase _unfollowStats;
        public StatsModelBase UnfollowStats
        {
            get { return _unfollowStats; }
            set
            {
                _unfollowStats = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ActionModel> _unfollowActions;
        public ObservableCollection<ActionModel> UnfollowActions
        {
            get { return _unfollowActions; }
            set
            {
                _unfollowActions = value;
                OnPropertyChanged();
            }
        }

        public UnfollowPageViewModel()
        {
            ButtonContent = "Start";
            LastActionTextHelper = "No actions yet";
            UnfollowStats = new StatsModelBase
            {
                Count = helper.BigNumbersCutting(Properties.Settings.Default.UnfollowCount),
                SessionCount = "",
                Status = "OFF",
                NextSessionIn = "00:00:00",
                TimeInWork = "00:00:00",
                NextIn = 0
            };
        }

        private ICommand _startUnfollowCommand;
        public ICommand StartUnfollowCommand
        {
            get { return _startUnfollowCommand ?? (_startUnfollowCommand = new RelayCommand(p => StartUnfollow())); }
        }


        private string _lastActionTextHelper;
        public string LastActionTextHelper
        {
            get { return _lastActionTextHelper; }
            set { _lastActionTextHelper = value; OnPropertyChanged(); }
        }

        private string _buttonContent;
        public string ButtonContent
        {
            get { return _buttonContent; }
            set { _buttonContent = value; OnPropertyChanged(); }
        }

        public async Task StartUnfollow()
        {
            if (mainVars.IsUnfollowInProgress == false)
            {
                LastActionTextHelper = "";
                ButtonContent = "Stop";
                await unfollowHelper.BeginUnfollow();
            }
            else
            {
                unfollowHelper.StopUnfollow();
                ButtonContent = "Start";
            }
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged = delegate { };
        public void OnCollectionChanged(NotifyCollectionChangedAction action)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action));
        }
    }
}
