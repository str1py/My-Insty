using Instagram_Assistant.SplashScreen;
using Instagram_Assistant.ViewModel;
using System.Windows;

namespace Instagram_Assistant
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected async override void OnStartup(StartupEventArgs e)
        {
            SplashScreenView splashScreen = new SplashScreenView();
            SplashScreenViewModel splashVM = SplashScreenViewModel.Instanse;
            splashScreen.Show();

            base.OnStartup(e);

            var mainWindow = await splashVM.Init();
            mainWindow.DataContext = MainWindowViewModel.instance;
            var login = await splashVM.IsLoggedIn();

            splashScreen.Hide();
            mainWindow.Show();
        }
    }
}
