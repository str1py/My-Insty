using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Instagram_Assistant;
using Instagram_Assistant.Model;
using Instagram_Assistant.ViewModel;
using Instagram_Assistant.SplashScreen;
using Instagram_Assistant.AutoUpdate;
using System.Windows;
using System.IO;

namespace Update
{
    public class UpdatePageViewModel : ViewModelBase
    {
        private static UpdatePageViewModel updateinstance;
        public static UpdatePageViewModel Instanse
        {
            get
            {
                if (updateinstance == null)
                {
                    updateinstance = new UpdatePageViewModel();
                }
                return updateinstance;
            }
        }

        Instagram_Assistant.AutoUpdate.Update update = new Instagram_Assistant.AutoUpdate.Update();

        private string splashScreenText;
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
            SplashScreenText = "Starting...";
            await FilesCheck();
            await Update();
            StartProgramm();
        }


        public async Task<bool> FilesCheck()
        {
            await Task.Delay(2000);
            SplashScreenText = "Checking files...";
            await update.IsFilesExists();
            await Task.Delay(2000);

            SplashScreenText = "Loking for new files...";
            await update.CheckForNewFiles();
            await Task.Delay(2000);

            SplashScreenText = "Compare files hashes...";
            update.GetFilesHashesLocal();
            return false;
        }

        private async Task<bool> Update()
        {
            await Task.Delay(2000);
            SplashScreenText = "Checking hashes...";
            update.GetFilesHashesLocal();
            await Task.Delay(2000);
            SplashScreenText = "Updating...";
            await Task.Delay(2000);
            await update.UpdateAssistant();
            return false;
        }



        public void StartProgramm()
        {
            try
            {
                string path = Directory.GetCurrentDirectory();
                System.Diagnostics.Process.Start(path + "\\Instagram Assistant.exe");
                Environment.Exit(0);
            }
            catch ( Exception e)
            {
                
            }
        }
    
    }
}
