using Instagram_Assistant.Helpers;
using Instagram_Assistant.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Instagram_Assistant.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        public static MainWindowViewModel instance;
        public  MainWindowViewModel()
        {
            instance = this;
            SelectedViewModel = LoginPageViewModel.Instanse;
        }


        private WindowState _curWindowState;
        public WindowState CurWindowState
        {
            get { return _curWindowState; }
            set
            {
                _curWindowState = value;
                OnPropertyChanged();
            }
        }

        //MAINVIEW
        private object _mainSelectedViewModel;
        public object MainSelectedViewModel
        {
            get { return _mainSelectedViewModel; }
            set { _mainSelectedViewModel = value; OnPropertyChanged(); }

        }

        //LOGIN VIEW
        private object selectedViewModel;
        public object SelectedViewModel
        {
            get { return selectedViewModel; }
            set { selectedViewModel = value; OnPropertyChanged(); }
        }

        private int _menuItem;
        public int MenuItem
        {
            get { return _menuItem; }
            set 
            { 
                _menuItem = value;
                OnPropertyChanged();
                MainSelectedViewModel = GetView(_menuItem);
            }
        }


        private ICommand _closeAppCommand;
        public ICommand CloseAppCommand
        {
            get { return _closeAppCommand ?? (_closeAppCommand = new RelayCommand(p => WindowClose())); }
        }

        private ICommand _collapseCommand;
        public ICommand CollapseCommand
        {
            get { return _collapseCommand ?? (_collapseCommand = new RelayCommand(p => WindowCollapse())); }
        }

        private void WindowCollapse()
        {
            CurWindowState = WindowState.Minimized;
        }

        private void WindowClose()
        {
            App.Current.Shutdown(0);
        }


        public static object GetView(int index)
        {
            switch (index)
            {
                case 0:
                    return AccountPageViewModel.Instanse;
                case 1:
                    return DashboardPageViewModel.Instanse;
                case 2:
                    return new SettingsPageViewModel();
                case 3:
                    return LogsPageViewModel.Instanse;
                case 4:
                    return new AboutPageViewModel();
            }
            return null;
        }

    }
}
