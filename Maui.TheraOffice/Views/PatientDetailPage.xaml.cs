using Maui.TheraOffice.ViewModels;

namespace Maui.TheraOffice.Views;

[QueryProperty(nameof(PatientId), "id")]
public partial class PatientDetailPage : ContentPage
{
    private readonly PatientDetailViewModel _viewModel;
    public string PatientId
    {
        set => _viewModel.LoadPatient(value);
    }

	public PatientDetailPage(PatientDetailViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
		BindingContext = viewModel;
	}
}