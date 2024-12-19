namespace AbsentManagement
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new AppShell();

            MainPage = new View.SplashView();

            Task.Run(async () =>
            {
                await Task.Delay(3000);
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    MainPage = new NavigationPage(new View.LoginView());
                });
            });
        }
    }
}
