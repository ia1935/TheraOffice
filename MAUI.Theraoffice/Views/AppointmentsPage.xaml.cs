using MAUI.Theraoffice.Models;
using MAUI.Theraoffice.ViewModels;
using Microsoft.Maui.Controls;

namespace MAUI.Theraoffice.Views
{
    public partial class AppointmentsPage : ContentPage
    {
        private readonly AppointmentsViewModel _vm;

        public AppointmentsPage(AppointmentsViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            BindingContext = _vm;
            List.ItemsSource = _vm.Appointments;
        }

        async void OnAdd(object? sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync("appointmentEdit");
        }

        async void OnSelect(object? sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Appointment a)
            {
                await Shell.Current.GoToAsync($"appointmentEdit?appointmentId={a.Id}");
                ((CollectionView)sender).SelectedItem = null;
            }
        }
    }
}
