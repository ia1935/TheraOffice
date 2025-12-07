using MAUI.Theraoffice.Services;
using MAUI.Theraoffice.ViewModels;
using MAUI.Theraoffice.Views;
using Microsoft.Extensions.Logging;

namespace MAUI.Theraoffice
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

#if DEBUG
            builder.Logging.AddDebug();
#endif

            // Register application services
            builder.Services.AddSingleton<IClinicStore, ClinicStore>();

            // ViewModels
            builder.Services.AddTransient<PatientsViewModel>();
            builder.Services.AddTransient<PatientEditViewModel>();
            builder.Services.AddTransient<PhysiciansViewModel>();
            builder.Services.AddTransient<PhysicianEditViewModel>();
            builder.Services.AddTransient<AppointmentsViewModel>();
            builder.Services.AddTransient<AppointmentEditViewModel>();

            // Pages
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<PatientsPage>();
            builder.Services.AddTransient<PatientEditPage>();
            builder.Services.AddTransient<PhysiciansPage>();
            builder.Services.AddTransient<PhysicianEditPage>();
            builder.Services.AddTransient<AppointmentsPage>();
            builder.Services.AddTransient<AppointmentEditPage>();

            return builder.Build();
        }
    }
}
