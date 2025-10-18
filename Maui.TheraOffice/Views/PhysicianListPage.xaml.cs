using Maui.TheraOffice.ViewModels;

namespace Maui.TheraOffice.Views;

public partial class PhysicianListPage : ContentPage
{
    private readonly PhysicianListViewModel _viewModel;

	public PhysicianListPage(PhysicianListViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
		BindingContext = viewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadPhysiciansAsync();
    }
}