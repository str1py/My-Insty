using Instagram_Assistant.Helpers;
using Instagram_Assistant.Model;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Instagram_Assistant.ViewModel
{
    class FilterAudiencePageViewModel:ViewModelBase, INotifyCollectionChanged
    {
        private static FilterAudiencePageViewModel audienceFilterInstance;
        public static FilterAudiencePageViewModel Instanse
        {
            get
            {
                if (audienceFilterInstance == null)
                    audienceFilterInstance = new FilterAudiencePageViewModel();
                return audienceFilterInstance;
            }
        }


        private AudienceFilterHelper auhelper = new AudienceFilterHelper();
        private MainVars mainVars = new MainVars();


        public FilterAudiencePageViewModel()
        {
            AudienceActions = new ObservableCollection<AudienceActionModel>();

            ButtonContent = "Start";
            LastActionTextHelper = "No actions yet";
        }

        private ICommand _startFilterAudienceCommand;
        public ICommand StartFilterAudienceCommand
        {
            get { return _startFilterAudienceCommand ?? (_startFilterAudienceCommand = new RelayCommand(p => StartFilterAudience())); }
        }

        private ObservableCollection<AudienceActionModel> audienceActions;
        public ObservableCollection<AudienceActionModel> AudienceActions
        {
            get
            {
                if (audienceActions?.Count-1 > 100)
                {
                    for (int i = audienceActions.Count; i > 100; i--)
                     audienceActions.RemoveAt(i);
                }    
                return audienceActions; 
            }
            set
            {
                audienceActions = value;
                OnPropertyChanged();

                if (audienceActions?.Count > 100)
                    audienceActions.RemoveAt(0);
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

        public async Task StartFilterAudience()
        {
            if (mainVars.IsAudienceInProgress == false)
            {
                if (Properties.Settings.Default.SaveFilteredAudiencePath != "")
                {
                    LastActionTextHelper = "";
                    ButtonContent = "Stop";
                    await auhelper.FilterAudience();
                }
                else
                {
                    MessageBox.Show("You dont select audience file");
                }
            }
            else
            {
                auhelper.StopFilterAudience();
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
