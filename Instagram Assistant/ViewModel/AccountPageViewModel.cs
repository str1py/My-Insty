using Instagram_Assistant.Helpers;
using Instagram_Assistant.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Instagram_Assistant.ViewModel
{
    class AccountPageViewModel : ViewModelBase
    {
        private static AccountPageViewModel accinstance;
        public static AccountPageViewModel Instance
        {
            get
            {
                if (accinstance == null)
                {
                    accinstance = new AccountPageViewModel();
                }
                return accinstance;
            }
        }

        private ObservableCollection<AccountInfo> _userInfo;
        public ObservableCollection<AccountInfo> UserInfo
        {
            get { return _userInfo; }
            set
            {
                _userInfo = value;
                OnPropertyChanged();
            }
        }

        public void GetInfo()
        {
            UserInfo = AccountInfoHelper.info;
        }


        private AccountInfo _accountListSelectedItem;
        public AccountInfo AccountListSelectedItem
        {
            get { return _accountListSelectedItem; }
            set
            {
                _accountListSelectedItem = value;
                OnPropertyChanged();
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        private ICommand _logOutCommand;
        public ICommand LogOutCommand
        {
            get { return _logOutCommand ?? (_logOutCommand = new RelayCommand(p => LogOut(p.ToString()))); }
        }

        private ICommand _logInCommand;
        public ICommand LoginCommand
        {
            get { return _logInCommand ?? (_logInCommand = new RelayCommand(p => LogIn(p.ToString()))); }
        }

        private ICommand _acceptChallangeCommand;
        public ICommand AcceptChallangeCommand
        {
            get { return _acceptChallangeCommand ?? (_acceptChallangeCommand = new RelayCommand(p => AcceptChallange(p.ToString()))); }
        }

        private ICommand _accountAddCommand;
        public ICommand AccountAddCommand
        {
            get { return _accountAddCommand ?? (_accountAddCommand = new RelayCommand(p => AccountAdd())); }
        }

        private ICommand _comboBoxSelectedCommand;
        public ICommand ComboBoxSelectedCommand
        {
            get { return _comboBoxSelectedCommand ?? (_comboBoxSelectedCommand = new RelayCommand(p => ChangeRole(p.ToString()))); }
        }
        
        private void ChangeRole(string username)
        {
            AccountInfoHelper acc = new AccountInfoHelper();
            acc.UpdateInfo(username);
        }
        private void AccountAdd()
        {
            LoginPageViewModel loginPage = LoginPageViewModel.Instanse;
            MainWindowViewModel.instance.SelectedViewModel = loginPage;
            MainWindowViewModel.instance.MainSelectedViewModel = null;
            loginPage.LoginVisibility = Visibility.Visible;
            loginPage.ChallengesVisibility = Visibility.Hidden;
            loginPage.CodeCheckVisibility = Visibility.Hidden;
            loginPage.LoginGridIsEnable = true;
            loginPage.ChallengesGridIsEnabel = true;
            loginPage.CodeCheckGridIsEnabel = true;
            loginPage.CancleLoginVisibility = Visibility.Visible;
        }


        private void LogOut(string username)
        {
            LogInHelper logInHelper = new LogInHelper();
            int logedinuserscount = LogInHelper.LoggedInUsers.Count;
            bool logOutStatus = logInHelper.LogOut(username);
            if (logOutStatus == true && logedinuserscount == 1)
            {
                LoginPageViewModel loginPage = LoginPageViewModel.Instanse;
                MainWindowViewModel.instance.SelectedViewModel = loginPage;
                MainWindowViewModel.instance.MainSelectedViewModel = null;
                loginPage.LoginVisibility = Visibility.Visible;
                loginPage.ChallengesVisibility = Visibility.Hidden;
                loginPage.CodeCheckVisibility = Visibility.Hidden;
                loginPage.LoginGridIsEnable = true;
                loginPage.ChallengesGridIsEnabel = true;
                loginPage.CodeCheckGridIsEnabel = true;
            }
            GetInfo();
            // MessageBox.Show(username);
        }

        private void LogIn(string username)
        {
            LogInHelper logInHelper = new LogInHelper();
            logInHelper.Login(username);
            LoginPageViewModel loginPage = LoginPageViewModel.Instanse;
            loginPage.ChallengesVisibility = Visibility.Hidden;
            loginPage.CodeCheckVisibility = Visibility.Hidden;
            loginPage.LoginGridIsEnable = true;
            loginPage.ChallengesGridIsEnabel = true;
            loginPage.CodeCheckGridIsEnabel = true;
        }



        private void  AcceptChallange(string username)
        {
           // LogInHelper logInHelper = new LogInHelper(); 
        }

    }
}
