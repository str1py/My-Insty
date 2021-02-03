using Instagram_Assistant.Helpers;
using Instagram_Assistant.Model;
using Instagram_Assistant.ViewModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Instagram_Assistant.SplashScreen
{
    class SplashScreenViewModel : ViewModelBase
    {
        private static SplashScreenViewModel splashinstance;
        public static SplashScreenViewModel Instanse
        {
            get
            {
                if (splashinstance == null)
                {
                    splashinstance = new SplashScreenViewModel();
                }
                return splashinstance;
            }
        }
        private LogInHelper loginhelp = new LogInHelper();
        private LoginPageViewModel loginVM = LoginPageViewModel.Instanse;


        private float _loadValue;
        public float LoadValue
        {
            get { return _loadValue; }
            set { _loadValue = value; OnPropertyChanged(); }
        }
        private string splashScreenText = "Starting....";
        public string SplashScreenText
        {
            get { return splashScreenText; }
            set { splashScreenText = value; OnPropertyChanged(); }
        }

        private ICommand _windowLoaded;
        public ICommand WindowLoaded
        {
            get { return _windowLoaded ?? (_windowLoaded = new RelayCommand(p => Start())); }
        }

        private async void Start()
        {
            await Task.Delay(3000);
            AutoUpdate.Update update = new AutoUpdate.Update();

            //SEARCH MISSING FILES
            SplashScreenText = "Checking files...";
            LoadValue += 20;



            if (!await update.IsFilesExists())
            {
                SplashScreenText = "Download missing files...";
                await Task.Delay(3000);
            }
            await Task.Delay(1000);

            //SEARCH BROKEN FILES
            SplashScreenText = "Checking files hashes...";
            LoadValue += 20;
            bool hash = await update.CompareFilesHash();
            await Task.Delay(1000);
            if (!hash)
            {
                await Task.Delay(1000);
                SplashScreenText = "Repairing files...";
                //await update.RepairFiles();
            }

            //SAVE HASHES IN FILE
            SplashScreenText = "Saving local files info...";
            LoadValue += 20;
            update.GetFilesHashesLocal();
            await Task.Delay(1000);


            //TRY UPDATE UPDATER
            await update.UpdateUpdater();


            SplashScreenText = "Login into exist accounts... Please wait...";
            LoadValue += 20;
        }
    


        public async Task<bool> IsLoggedIn()
        {
            if (loginhelp.LogedInCheck() == true)
            {
                await loginVM.SuccessLogIn();
                return true;
            }
            else
                return false;
        }



    }
}
