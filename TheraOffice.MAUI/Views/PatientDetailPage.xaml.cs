using TheraOffice.MAUI.ViewModels;

namespace TheraOffice.MAUI.Views
{
    public partial class PatientDetailPage : ContentPage
    {
        public PatientDetailPage(PatientDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
