using Maui.TheraOffice.ViewModels;

namespace Maui.TheraOffice.Views;

public partial class AppointmentListPage : ContentPage
{
    private readonly AppointmentListViewModel _viewModel;

	public AppointmentListPage(AppointmentListViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
		BindingContext = viewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadAppointmentsAsync();
    }
}