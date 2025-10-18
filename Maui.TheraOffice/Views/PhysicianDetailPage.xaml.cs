using Maui.TheraOffice.ViewModels;

namespace Maui.TheraOffice.Views;

[QueryProperty(nameof(PhysicianId), "id")]
public partial class PhysicianDetailPage : ContentPage
{
    private readonly PhysicianDetailViewModel _viewModel;
    public string PhysicianId
    {
        set => _viewModel.LoadPhysician(value);
    }

	public PhysicianDetailPage(PhysicianDetailViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
		BindingContext = viewModel;
	}
}