using Microsoft.Extensions.Logging;
using TheraOffice.MAUI.Services;
using TheraOffice.MAUI.ViewModels;
using TheraOffice.MAUI.Views;

namespace TheraOffice.MAUI
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

            builder.Services.AddSingleton<IDataService, DataService>();

            builder.Services.AddTransient<PatientsViewModel>();
            builder.Services.AddTransient<PatientDetailViewModel>();
            builder.Services.AddTransient<PhysiciansViewModel>();
            builder.Services.AddTransient<PhysicianDetailViewModel>();
            builder.Services.AddTransient<AppointmentsViewModel>();
            builder.Services.AddTransient<AppointmentDetailViewModel>();

            builder.Services.AddTransient<PatientsPage>();
            builder.Services.AddTransient<PatientDetailPage>();
            builder.Services.AddTransient<PhysiciansPage>();
            builder.Services.AddTransient<PhysicianDetailPage>();
            builder.Services.AddTransient<AppointmentsPage>();
            builder.Services.AddTransient<AppointmentDetailPage>();

            return builder.Build();
        }
    }
}
