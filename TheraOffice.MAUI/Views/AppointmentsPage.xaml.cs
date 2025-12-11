using TheraOffice.MAUI.ViewModels;

namespace TheraOffice.MAUI.Views
{
    public partial class AppointmentsPage : ContentPage
    {
        private readonly AppointmentsViewModel _viewModel;

        public AppointmentsPage(AppointmentsViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadAppointmentsAsync();
        }
    }
}
