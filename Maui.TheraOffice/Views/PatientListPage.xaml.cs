using Maui.TheraOffice.ViewModels;

namespace Maui.TheraOffice.Views;

public partial class PatientListPage : ContentPage
{
    private readonly PatientListViewModel _viewModel;

	public PatientListPage(PatientListViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
		BindingContext = viewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadPatientsAsync();
    }
}