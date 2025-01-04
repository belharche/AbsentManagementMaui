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
           
            builder.Services.AddTransient<SplashView>();
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddTransient<RegisterView>();
            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<LoginView>();
            builder.Services.AddTransient<HomeView>();
            builder.Services.AddTransient<HomeViewModel>();
            builder.Services.AddTransient<AddStudentViewModel>();
            builder.Services.AddTransient<AddStudentView>();

            builder.Services.AddTransient<AbsenceView>();
            builder.Services.AddTransient<AbsenceViewModel>();
            builder.Services.AddTransient<SearchView>();
            builder.Services.AddTransient<SearchViewModel>();

            builder.Services.AddTransient<NewLessonView>();
            builder.Services.AddTransient<NewLessonViewModel>();

            var app = builder.Build();

            return app;
        }

    }
}
