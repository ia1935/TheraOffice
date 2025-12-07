using System;
using MAUI.Theraoffice.Models;
using MAUI.Theraoffice.Services;
using MAUI.Theraoffice.ViewModels;
using Microsoft.Maui.Controls;

namespace MAUI.Theraoffice.Views
{
    public partial class PhysicianEditPage : ContentPage
    {
        private readonly IClinicStore _store;
        private PhysicianEditViewModel _vm;

        public PhysicianEditPage(IClinicStore store)
        {
            InitializeComponent();
            _store = store;
            _vm = new PhysicianEditViewModel(_store);
            BindingContext = _vm;
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
