using AbsentManagement.View;
using Microsoft.Maui.Controls;

namespace AbsentManagement
{
    public partial class App : Application
    {
        public App(SplashView splashView, IServiceProvider serviceProvider)
        {
            InitializeComponent();

            MainPage = splashView;

            Task.Run(async () =>
            {
                await Task.Delay(3000);
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    MainPage = new NavigationPage(serviceProvider.GetRequiredService<LoginView>());
                });
            });
        }
    }
}