using AbsentManagement.View;

namespace AbsentManagement;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // Set SplashView as the initial page
        MainPage = new SplashView();

        // Simulate splash screen delay and switch to AppShell
        Task.Run(async () =>
        {
            await Task.Delay(3000);
            MainThread.BeginInvokeOnMainThread(() =>
            {
                MainPage = new AppShell();
            });
        });
    }
}
