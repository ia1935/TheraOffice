using Maui.TheraOffice.ViewModels;

namespace Maui.TheraOffice.Views;

[QueryProperty(nameof(AppointmentId), "id")]
public partial class AppointmentDetailPage : ContentPage
{
    private readonly AppointmentDetailViewModel _viewModel;
    public string AppointmentId
    {
        set => _viewModel.LoadAppointment(value);
    }

	public AppointmentDetailPage(AppointmentDetailViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
		BindingContext = viewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadPatientsAndPhysiciansAsync();
    }
}