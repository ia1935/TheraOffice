using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Maui.TheraOffice.Models;
using Maui.TheraOffice.Services;

namespace Maui.TheraOffice.ViewModels
{
    public partial class PatientDetailViewModel : ObservableObject
    {
        private readonly IPatientService _patientService;

        [ObservableProperty]
        Patient patient;

        [ObservableProperty]
        string diagnosesText;

        [ObservableProperty]
        string prescriptionsText;

        public PatientDetailViewModel(IPatientService patientService)
        {
            _patientService = patientService;
            Patient = new Patient { Birthdate = DateTime.Now }; // Initialize with a default date
        }

        public async void LoadPatient(string patientId)
        {
            if (Guid.TryParse(patientId, out Guid id))
            {
                Patient = await _patientService.GetPatientAsync(id);
                DiagnosesText = string.Join(", ", Patient.Diagnoses);
                PrescriptionsText = string.Join(", ", Patient.Prescriptions);
            }
            else
            {
                Patient = new Patient { Birthdate = DateTime.Now };
            }
        }

        [RelayCommand]
        async Task SavePatientAsync()
        {
            Patient.Diagnoses = DiagnosesText?.Split(',').Select(d => d.Trim()).ToList() ?? new List<string>();
            Patient.Prescriptions = PrescriptionsText?.Split(',').Select(p => p.Trim()).ToList() ?? new List<string>();

            if (Patient.Id == Guid.Empty)
            {
                await _patientService.AddPatientAsync(Patient);
            }
            else
            {
                await _patientService.UpdatePatientAsync(Patient);
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