using Instagram_Assistant.Helpers;
using Instagram_Assistant.Model;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Instagram_Assistant.ViewModel
{
    public class LoginPageViewModel : ViewModelBase
    {
        private static LoginPageViewModel logininstance;
        public static LoginPageViewModel Instanse
        {
            get
            {
                if (logininstance == null)
                {
                    logininstance = new LoginPageViewModel();
                }
                return logininstance;
            }
        }
        private LogInHelper loginhelp = new LogInHelper();
        private TrimHelper trimhelp = new TrimHelper();
        private LogsPageViewModel logs = LogsPageViewModel.Instance;

        public LoginPageViewModel()
        {
            LoginVisibility = Visibility.Hidden;
            ChallengesVisibility = Visibility.Hidden;
            CodeCheckVisibility = Visibility.Hidden;
            if (loginhelp.isSessionsLoaded != true)
            {
                LoginVisibility = Visibility.Visible;
                ChallengesVisibility = Visibility.Hidden;
                CodeCheckVisibility = Visibility.Hidden;
                LoginGridIsEnable = true;
                ChallengesGridIsEnabel = true;
                CodeCheckGridIsEnabel = true;
                CancleLoginVisibility = Visibility.Hidden;
            }
        }

        private string login;
        public string Login
        {
            get { return login; }
            set { login = value; OnPropertyChanged(); }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged(); }
        }

        private string _code;
        public string Code
        {
            get { return _code; }
            set { _code = value; OnPropertyChanged(); }
        }

        private string _phoneForCode;
        public string PhoneForCode
        {
            get { return _phoneForCode; }
            set { _phoneForCode = value; OnPropertyChanged(); }
        }

        private string _emailForCode;
        public string EmailForCode
        {
            get { return _emailForCode; }
            set { _emailForCode = value; OnPropertyChanged(); }
        }

        private Visibility _challengesVisibility;
        public Visibility ChallengesVisibility
        {
            get { return _challengesVisibility; }
            set { _challengesVisibility = value; OnPropertyChanged(); }
        }
        private Visibility _loginVisibility;
        public Visibility LoginVisibility
        {
            get { return _loginVisibility; }
            set { _loginVisibility = value; OnPropertyChanged(); }
        }
        private Visibility _codeCheckVisibility;
        public Visibility CodeCheckVisibility
        {
            get { return _codeCheckVisibility; }
            set { _codeCheckVisibility = value; OnPropertyChanged(); }
        }

        private bool _isPhoneCode;
        public bool IsPhoneCode
        {
            get { return _isPhoneCode; }
            set { _isPhoneCode = value; OnPropertyChanged(); }
        }
        private bool _isEmailCode;
        public bool IsEmailCode
        {
            get { return _isEmailCode; }
            set { _isEmailCode = value; OnPropertyChanged(); }
        }

        private ICommand _beginLogin;
        public ICommand BeginLogin
        {
            get { return _beginLogin ?? (_beginLogin = new RelayCommand(async p => await InstagramLogIn())); }
        }

        private ICommand _sendCodeCommand;
        public ICommand SendCodeCommand
        {
            get { return _sendCodeCommand ?? (_sendCodeCommand = new RelayCommand(async p => await Verify())); } 
        }
        private ICommand _codeCheckCommand;
        public ICommand CodeCheckCommand
        {
            get { return _codeCheckCommand ?? (_codeCheckCommand = new RelayCommand(async p => await CheckCode())); }
        }

        private bool _loginGridIsEnable;
        public bool LoginGridIsEnable
        {
            get { return _loginGridIsEnable; }
            set { _loginGridIsEnable = value; OnPropertyChanged(); }
        }

        private bool _challengesGridIsEnabel;
        public bool ChallengesGridIsEnabel
        {
            get { return _challengesGridIsEnabel; }
            set { _challengesGridIsEnabel = value; OnPropertyChanged(); }
        }

        private bool _codeCheckGridIsEnabel;
        public bool CodeCheckGridIsEnabel
        {
            get { return _codeCheckGridIsEnabel; }
            set { _codeCheckGridIsEnabel = value; OnPropertyChanged(); }
        }


        public void ChangePageToCode()
        {
            PhoneForCode = trimhelp.PersonalPhoneToStars(loginhelp.phone);
            EmailForCode = trimhelp.PersonalEmailToStars(loginhelp.email);
            LoginVisibility = Visibility.Hidden;
            ChallengesVisibility = Visibility.Visible;
        }

        public async Task InstagramLogIn()
        {
            LoginGridIsEnable = false;
            string result = await loginhelp.Login(Login, Password);
            logs.Add($"Login Result {result}",MessageType.Type.DEBUGINFO, this.GetType().Name);

            if (result == "ChallengeRequired")
                ChangePageToCode();
            else if (result == "BadPassword")
            {
                LoginGridIsEnable = true;
                MessageBox.Show("Error!" + result);
                Login = "";
                Password = "";
            }
            else if (result == "Success")
            {
                await SuccessLogIn();
            }
            else
            {
                MessageBox.Show("Error!" + result);
                LoginGridIsEnable = true;
                Login = "";
                Password = "";
            }
        }

        public async Task Verify()
        {
            ChallengesGridIsEnabel = false;
            var result = await loginhelp.SendCode(IsPhoneCode);
            if (result == true)
            {
                ChallengesVisibility = Visibility.Hidden;
                CodeCheckVisibility = Visibility.Visible;
            }
            else
                MessageBox.Show("ERROR");
                
        }

        public async Task CheckCode()
        {
            CodeCheckGridIsEnabel = false;
            var result = await loginhelp.CodeCheck(Code);
            if (result == true)            
                await SuccessLogIn();
            
        }

        public async Task SuccessLogIn()
        {

            AccountInfoHelper accountInfo = new AccountInfoHelper();
            if (loginhelp._instaApi == null)
            {
                await accountInfo.GetInfo();
                AccountPageViewModel.Instance.GetInfo();
            }
            else
            {
                await accountInfo.UpdateInfo(loginhelp._instaApi);
                AccountPageViewModel.Instance.GetInfo();
            }
            MainWindowViewModel.instance.SelectedViewModel = null;
            MainWindowViewModel.instance.MainSelectedViewModel = DashboardPageViewModel.Instance;
            MainWindowViewModel.instance.MenuItem = 0;
        }


        private Visibility _cancleLoginVisibility;
        public Visibility CancleLoginVisibility
        {
            get { return _cancleLoginVisibility; }
            set { _cancleLoginVisibility = value; OnPropertyChanged(); }
        }
        private ICommand _cancleLoginCommand;
        public ICommand CancleLoginCommand
        {
            get { return _cancleLoginCommand ?? (_cancleLoginCommand = new RelayCommand(p => CancleLogin())); }
        }

        private void CancleLogin()
        {
            MainWindowViewModel.instance.SelectedViewModel = null;
            MainWindowViewModel.instance.MainSelectedViewModel = DashboardPageViewModel.Instance;
            MainWindowViewModel.instance.MenuItem = 1;
            CancleLoginVisibility = Visibility.Hidden;
        }
    }
}
