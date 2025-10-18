using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Maui.TheraOffice.Models;
using Maui.TheraOffice.Services;
using System.Collections.ObjectModel;

namespace Maui.TheraOffice.ViewModels
{
    public partial class PhysicianListViewModel : ObservableObject
    {
        private readonly IPhysicianService _physicianService;

        [ObservableProperty]
        ObservableCollection<Physician> physicians;

        public PhysicianListViewModel(IPhysicianService physicianService)
        {
            _physicianService = physicianService;
            Physicians = new ObservableCollection<Physician>();
        }

        [RelayCommand]
        async Task LoadPhysiciansAsync()
        {
            var physicianList = await _physicianService.GetPhysiciansAsync();
            Physicians.Clear();
            foreach (var physician in physicianList)
            { 
                Physicians.Add(physician);
            }
        }

        [RelayCommand]
        async Task AddPhysicianAsync()
        {
            // Navigation to Add/Edit Physician Page
            await Shell.Current.GoToAsync("PhysicianDetailPage");
        }

        [RelayCommand]
        async Task EditPhysicianAsync(Physician physician)
        {
            // Navigation to Add/Edit Physician Page with physician ID
            await Shell.Current.GoToAsync($"PhysicianDetailPage?id={physician.Id}");
        }

        [RelayCommand]
        async Task DeletePhysicianAsync(Physician physician)
        {
            await _physicianService.DeletePhysicianAsync(physician.Id);
            await LoadPhysiciansAsync();
        }
    }
}