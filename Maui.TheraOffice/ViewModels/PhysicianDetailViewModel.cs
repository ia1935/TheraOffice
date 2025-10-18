using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Maui.TheraOffice.Models;
using Maui.TheraOffice.Services;

namespace Maui.TheraOffice.ViewModels
{
    public partial class PhysicianDetailViewModel : ObservableObject
    {
        private readonly IPhysicianService _physicianService;

        [ObservableProperty]
        Physician physician;

        [ObservableProperty]
        string specializationsText;

        public PhysicianDetailViewModel(IPhysicianService physicianService)
        {
            _physicianService = physicianService;
            Physician = new Physician { GraduationDate = DateTime.Now }; // Initialize with a default date
        }

        public async void LoadPhysician(string physicianId)
        {
            if (Guid.TryParse(physicianId, out Guid id))
            {
                Physician = await _physicianService.GetPhysicianAsync(id);
                SpecializationsText = string.Join(", ", Physician.Specializations);
            }
            else
            {
                Physician = new Physician { GraduationDate = DateTime.Now };
            }
        }

        [RelayCommand]
        async Task SavePhysicianAsync()
        {
            Physician.Specializations = SpecializationsText?.Split(',').Select(s => s.Trim()).ToList() ?? new List<string>();

            if (Physician.Id == Guid.Empty)
            {
                await _physicianService.AddPhysicianAsync(Physician);
            }
            else
            {
                await _physicianService.UpdatePhysicianAsync(Physician);
            }
            await Shell.Current.GoToAsync(".."); // Navigate back
        }

        [RelayCommand]
        async Task CancelAsync()
        {
            await Shell.Current.GoToAsync(".."); // Navigate back
        }
    }
}