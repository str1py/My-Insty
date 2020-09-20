﻿using Instagram_Assistant.Helpers;
using Instagram_Assistant.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram_Assistant.SplashScreen
{
    class SplashScreenViewModel :ViewModelBase
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
        private LoginPageViewModel loginVM = new LoginPageViewModel();


        public SplashScreenViewModel()
        {
        }

        public async Task<MainWindowView> Init()
        {
            await Task.Delay(3000);
            SplashScreenText = "Creating some awsome things...";
            await Task.Delay(2000);
            return new MainWindowView();
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

        private string splashScreenText = "Initializing...";
        public string SplashScreenText
        {
            get { return splashScreenText; }
            set { splashScreenText = value; OnPropertyChanged(); }
        }
    }
}