using MAUI.Theraoffice.Models;
using MAUI.Theraoffice.ViewModels;
using Microsoft.Maui.Controls;

namespace MAUI.Theraoffice.Views
{
    public partial class PatientsPage : ContentPage
    {
        private readonly PatientsViewModel _vm;
        public PatientsPage(PatientsViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            BindingContext = _vm;
            PatientsList.ItemsSource = _vm.Patients;
        }

        async void OnAdd(object? sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync("patientEdit");
        }

        async void OnSelect(object? sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Patient p)
            {
                await Shell.Current.GoToAsync($"patientEdit?patientId={p.Id}");
                ((CollectionView)sender).SelectedItem = null;
            }
        }
    }
}
