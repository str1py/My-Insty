using Instagram_Assistant.SplashScreen;
using Instagram_Assistant.ViewModel;
using System.Windows;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace Instagram_Assistant
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected async override void OnStartup(StartupEventArgs e)
        {
            AppCenter.Start("e6657a5a-5759-4fe6-8bf3-0b1ca7533041",
                   typeof(Analytics), typeof(Crashes));

            SplashScreenView splashScreen = new SplashScreenView();
            SplashScreenViewModel splashVM = SplashScreenViewModel.Instanse;
            splashScreen.Show();

            base.OnStartup(e);

            var mainWindow = new MainWindowView();
            mainWindow.DataContext = MainWindowViewModel.instance;
            var login = await splashVM.IsLoggedIn();

            splashScreen.Hide();
            mainWindow.Show();
        }
    }
}
    


