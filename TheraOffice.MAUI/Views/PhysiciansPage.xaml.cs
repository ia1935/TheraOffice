using TheraOffice.MAUI.ViewModels;

namespace TheraOffice.MAUI.Views
{
    public partial class PhysiciansPage : ContentPage
    {
        private readonly PhysiciansViewModel _viewModel;

        public PhysiciansPage(PhysiciansViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadPhysiciansAsync();
        }
    }
}
