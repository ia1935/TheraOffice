using MAUI.Theraoffice.Models;
using MAUI.Theraoffice.ViewModels;
using Microsoft.Maui.Controls;

namespace MAUI.Theraoffice.Views
{
    public partial class PhysiciansPage : ContentPage
    {
        private readonly PhysiciansViewModel _vm;

        public PhysiciansPage(PhysiciansViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            BindingContext = _vm;
            List.ItemsSource = _vm.Physicians;
        }

        async void OnAdd(object? sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync("physicianEdit");
        }

        async void OnSelect(object? sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Physician p)
            {
                await Shell.Current.GoToAsync($"physicianEdit?physicianId={p.Id}");
                ((CollectionView)sender).SelectedItem = null;
            }
        }
    }
}
