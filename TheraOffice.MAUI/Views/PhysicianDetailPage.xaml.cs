using TheraOffice.MAUI.ViewModels;

namespace TheraOffice.MAUI.Views
{
    public partial class PhysicianDetailPage : ContentPage
    {
        public PhysicianDetailPage(PhysicianDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
