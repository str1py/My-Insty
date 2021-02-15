using Instagram_Assistant.Helpers;
using Instagram_Assistant.Model;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Instagram_Assistant.ViewModel
{
    class AboutPageViewModel :ViewModelBase
    {
        private ServerHelper server = new ServerHelper();
        public AboutPageViewModel()
        {
            Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            License = "Developer";
        }

        private string programState = "Alpha";
        public string ProgramState
        {
            get { return programState; }
            set { programState = value; OnPropertyChanged(); }
        }
        
        private string version;
        public string Version
        {
            get { return version; }
            set { version = value; OnPropertyChanged(); }
        }
        private string license;
        public string License
        {
            get { return license; }
            set { license = value; OnPropertyChanged(); }
        }
        private string downLoadRelease;
        public string DownloadRelease
        {
            get { return downLoadRelease; }
            set { downLoadRelease = value; OnPropertyChanged(); }
        }
        private string updateVersion;
        public string UpdateVersion
        {
            get { return updateVersion; }
            set { updateVersion = value; OnPropertyChanged(); }
        }
        private string changeLog;
        public string ChangeLog
        {
            get { return changeLog; }
            set { changeLog = value; OnPropertyChanged(); }
        }


        private Visibility checkVisibility = Visibility.Hidden;
        public Visibility CheckVisibility
        {
            get { return checkVisibility; }
            set { checkVisibility = value; OnPropertyChanged(); }
        }

        private Visibility newVersionVisibility = Visibility.Hidden;
        public Visibility NewVersionVisibility
        {
            get { return newVersionVisibility; }
            set { newVersionVisibility = value; OnPropertyChanged(); }
        }

        private Visibility uptodateVersionVisibility = Visibility.Hidden;
        public Visibility UptodateVersionVisibility
        {
            get { return uptodateVersionVisibility; }
            set { uptodateVersionVisibility = value; OnPropertyChanged(); }
        }

        private Visibility checkProgressVisibility = Visibility.Visible;
        public Visibility CheckProgressVisibility
        {
            get { return checkProgressVisibility; }
            set { checkProgressVisibility = value; OnPropertyChanged(); }
        }

        private ICommand checkUpdateCommand;
        public ICommand CheckUpdateCommand
        {
            get { return checkUpdateCommand ?? (checkUpdateCommand = new RelayCommand(p => CheckUpdate())); }
        }

        private async void  CheckUpdate()
        {
            CheckVisibility = Visibility.Visible;
            await Task.Delay(3000);
            AutoUpdate.Update update = new AutoUpdate.Update();
            bool result = await update.CompareProgrammVersions();
            if (result)
            {
                CheckProgressVisibility = Visibility.Hidden;
                NewVersionVisibility = Visibility.Visible;
                UpdateVersion = AutoUpdate.Update.NewVersion;
                DownloadRelease = "20.20.20";
                ChangeLog = server.GetChangelogFromServer();
            }
            else
            {
                CheckProgressVisibility = Visibility.Hidden;
                UptodateVersionVisibility = Visibility.Visible;
                UpdateVersion = AutoUpdate.Update.NewVersion;
                DownloadRelease = "20.20.20";
            }
        }


        private ICommand cancleUpdateCommand;
        public ICommand CancleUpdateCommand
        {
            get { return cancleUpdateCommand ?? (cancleUpdateCommand = new RelayCommand(p => CancleCheckUpdate())); }
        }

        void CancleCheckUpdate()
        {
            CheckVisibility = Visibility.Hidden;
            CheckProgressVisibility = Visibility.Visible;
            UptodateVersionVisibility = Visibility.Hidden;
            NewVersionVisibility = Visibility.Hidden;
        }

      
        private ICommand updateNowCommand;
        public ICommand UpdateNowCommand
        {
            get { return updateNowCommand ?? (updateNowCommand = new RelayCommand(p => UpdateNow())); }
        }

        void UpdateNow()
        {
            AutoUpdate.Update update = new AutoUpdate.Update();
            update.BeginUpdate();
        }
    }
}
