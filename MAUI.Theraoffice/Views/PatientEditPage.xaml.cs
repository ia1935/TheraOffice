using System;
using MAUI.Theraoffice.Models;
using MAUI.Theraoffice.Services;
using MAUI.Theraoffice.ViewModels;
using Microsoft.Maui.Controls;

namespace MAUI.Theraoffice.Views
{
    public partial class PatientEditPage : ContentPage
    {
        private readonly IClinicStore _store;
        private PatientEditViewModel _vm;

        public PatientEditPage(IClinicStore store)
        {
            InitializeComponent();
            _store = store;
            _vm = new PatientEditViewModel(_store);
            BindingContext = _vm;
        }

        protected override async void OnNavigatedTo(NavigatedToEventArgs args)
        {
            // Handle query param patientId via Shell routing if present
            if (Shell.Current.CurrentState != null)
            {
                var nav = Shell.Current.CurrentState.Location.OriginalString;
            }
            base.OnNavigatedTo(args);
        }

        async void OnSave(object? sender, EventArgs e)
        {
            await _vm.SaveAsync();
            await Shell.Current.GoToAsync("..");
        }

        async void OnCancel(object? sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
