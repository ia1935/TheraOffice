using Maui.TheraOffice.Views;
using Maui.TheraOffice.ViewModels;

namespace Maui.TheraOffice;

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

		builder.Services.AddSingleton<Maui.TheraOffice.Services.IPatientService, Maui.TheraOffice.Services.PatientService>();
		builder.Services.AddSingleton<Maui.TheraOffice.Services.IPhysicianService, Maui.TheraOffice.Services.PhysicianService>();
		builder.Services.AddSingleton<Maui.TheraOffice.Services.IAppointmentService, Maui.TheraOffice.Services.AppointmentService>();

		builder.Services.AddSingleton<PatientListPage>();
		builder.Services.AddSingleton<PatientListViewModel>();

		builder.Services.AddSingleton<PhysicianListPage>();
		builder.Services.AddSingleton<PhysicianListViewModel>();

		builder.Services.AddSingleton<AppointmentListPage>();
		builder.Services.AddSingleton<AppointmentListViewModel>();

		builder.Services.AddTransient<PatientDetailPage>();
		builder.Services.AddTransient<PatientDetailViewModel>();

		builder.Services.AddTransient<PhysicianDetailPage>();
		builder.Services.AddTransient<PhysicianDetailViewModel>();

		builder.Services.AddTransient<AppointmentDetailPage>();
		builder.Services.AddTransient<AppointmentDetailViewModel>();

		return builder.Build();
	}
}
