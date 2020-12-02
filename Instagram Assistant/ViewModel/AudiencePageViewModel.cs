using Instagram_Assistant.Helpers;
using Instagram_Assistant.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Instagram_Assistant.ViewModel
{
    class AudiencePageViewModel : ViewModelBase, INotifyCollectionChanged
    {
        private static AudiencePageViewModel audienceInstance;
        public static AudiencePageViewModel Instanse
        {
            get
            {
                if (audienceInstance == null)
                    audienceInstance = new AudiencePageViewModel();
                return audienceInstance;
            }
        }

        private AudienceHelper auhelper = new AudienceHelper();
        private MainVars mainVars = new MainVars();


        private ICommand _startAudienceCommand;
        public ICommand StartAudienceCommand
        {
            get { return _startAudienceCommand ?? (_startAudienceCommand = new RelayCommand(p => StartAudience())); }
        }


        private AudienceProcessModel audienceProcess;
        public AudienceProcessModel AudienceProcess
        { 
            get { return audienceProcess; }
            set
            {
                audienceProcess = value;
                OnPropertyChanged();
            }
        }


        public AudiencePageViewModel()
        {
            ButtonContent = "Start";
            AudienceProcess = new AudienceProcessModel("No actions yet", 0);
            LastActionTextHelper = "No actions yet";
            AccountInUse = "n/a";
            CompetitorInUse = "n/a";
            AudienceCount = 0;
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

        private string _accountInUse;
        public string AccountInUse
        {
            get { return _accountInUse; }
            set { _accountInUse = value; OnPropertyChanged(); }
        }

        private string _competitorInUse;
        public string CompetitorInUse
        {
            get { return _competitorInUse; }
            set { _competitorInUse = value; OnPropertyChanged(); }
        }

        private int _audienceCount;
        public int AudienceCount
        {
            get { return _audienceCount; }
            set { _audienceCount = value; OnPropertyChanged(); }
        }

        private string _competitorList;
        public string CompetitorList
        {
            get { return _competitorList; }
            set { _competitorList = value; 
                OnPropertyChanged();
                CompList = CompetitorsToList(_competitorList); }
        }

        private string[] _compList;
        public string[] CompList
        {
            get { return _compList; }
            set { _compList = value; OnPropertyChanged(); }
        }

        private string[] CompetitorsToList(string srt)
        {
            char[] delimiterChars = { '\r', '\t',' ' };
            srt = srt.Replace('\n', ' ');

            string[] massive = srt.Split(delimiterChars);

            foreach (var line in massive)
                 line.Trim();

            massive = massive.Where(x => x != "").ToArray();

            return massive;
        }

        public async Task StartAudience()
        {
            if (mainVars.IsAudienceInProgress == false)
            {
                if (CompList != null)
                {
                    LastActionTextHelper = "";
                    ButtonContent = "Stop";
                    await auhelper.BeginCollectingAudience();
                }else
                {
                    MessageBox.Show("Seems to be there are no competitors");
                }
            }
            else
            {
                auhelper.StopCollectingAudience();
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
