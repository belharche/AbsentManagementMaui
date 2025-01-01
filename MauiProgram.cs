using AbsentManagement.Database;
using AbsentManagement.View;
using AbsentManagement.ViewModel;
using Microsoft.Extensions.Logging;

namespace AbsentManagement
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<DbService>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<LoginView>();
            builder.Services.AddTransient<SplashView>();
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddTransient<RegisterView>();

            var app = builder.Build();

            return app;
        }

    }
}
