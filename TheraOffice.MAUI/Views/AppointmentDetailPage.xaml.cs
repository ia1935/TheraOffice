using TheraOffice.MAUI.ViewModels;

namespace TheraOffice.MAUI.Views
{
    public partial class AppointmentDetailPage : ContentPage
    {
        public AppointmentDetailPage(AppointmentDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
