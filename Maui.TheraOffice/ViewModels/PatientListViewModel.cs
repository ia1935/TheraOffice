using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Maui.TheraOffice.Models;
using Maui.TheraOffice.Services;
using System.Collections.ObjectModel;

namespace Maui.TheraOffice.ViewModels
{
    public partial class PatientListViewModel : ObservableObject
    {
        private readonly IPatientService _patientService;

        [ObservableProperty]
        ObservableCollection<Patient> patients;

        public PatientListViewModel(IPatientService patientService)
        {
            _patientService = patientService;
            Patients = new ObservableCollection<Patient>();
        }

        [RelayCommand]
        async Task LoadPatientsAsync()
        {
            var patientList = await _patientService.GetPatientsAsync();
            Patients.Clear();
            foreach (var patient in patientList)
            { 
                Patients.Add(patient);
            }
        }

        [RelayCommand]
        async Task AddPatientAsync()
        {
            // Navigation to Add/Edit Patient Page
            await Shell.Current.GoToAsync("PatientDetailPage");
        }

        [RelayCommand]
        async Task EditPatientAsync(Patient patient)
        {
            // Navigation to Add/Edit Patient Page with patient ID
            await Shell.Current.GoToAsync($"PatientDetailPage?id={patient.Id}");
        }

        [RelayCommand]
        async Task DeletePatientAsync(Patient patient)
        {
            await _patientService.DeletePatientAsync(patient.Id);
            await LoadPatientsAsync();
        }
    }
}