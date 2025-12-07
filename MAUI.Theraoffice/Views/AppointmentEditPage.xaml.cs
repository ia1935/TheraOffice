using System;
using MAUI.Theraoffice.Models;
using MAUI.Theraoffice.Services;
using MAUI.Theraoffice.ViewModels;
using Microsoft.Maui.Controls;

namespace MAUI.Theraoffice.Views
{
    public partial class AppointmentEditPage : ContentPage
    {
        private readonly IClinicStore _store;
        private AppointmentEditViewModel _vm;

        public AppointmentEditPage(IClinicStore store)
        {
            InitializeComponent();
            _store = store;
            _vm = new AppointmentEditViewModel(_store);
            BindingContext = _vm;
        }

        async void OnSave(object? sender, EventArgs e)
        {
            var err = await _vm.SaveAsync();
            if (err != null)
            {
                ErrorLabel.Text = err;
                return;
            }
            await Shell.Current.GoToAsync("..");
        }

        async void OnCancel(object? sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
