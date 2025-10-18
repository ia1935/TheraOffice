using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Maui.TheraOffice.Models;
using Maui.TheraOffice.Services;
using System.Collections.ObjectModel;

namespace Maui.TheraOffice.ViewModels
{
    public partial class AppointmentDetailViewModel : ObservableObject
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IPatientService _patientService;
        private readonly IPhysicianService _physicianService;

        [ObservableProperty]
        Appointment appointment;

        [ObservableProperty]
        ObservableCollection<Patient> patients;

        [ObservableProperty]
        Patient selectedPatient;

        [ObservableProperty]
        ObservableCollection<Physician> physicians;

        [ObservableProperty]
        Physician selectedPhysician;

        public AppointmentDetailViewModel(IAppointmentService appointmentService, IPatientService patientService, IPhysicianService physicianService)
        {
            _appointmentService = appointmentService;
            _patientService = patientService;
            _physicianService = physicianService;
            Appointment = new Appointment { StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(1) }; // Default to 1 hour appointment
            Patients = new ObservableCollection<Patient>();
            Physicians = new ObservableCollection<Physician>();
        }

        public async Task LoadPatientsAndPhysiciansAsync()
        {
            var patientList = await _patientService.GetPatientsAsync();
            Patients.Clear();
            foreach (var patient in patientList)
            {
                Patients.Add(patient);
            }

            var physicianList = await _physicianService.GetPhysiciansAsync();
            Physicians.Clear();
            foreach (var physician in physicianList)
            {
                Physicians.Add(physician);
            }

            if (Appointment.PatientId != Guid.Empty)
            {
                SelectedPatient = Patients.FirstOrDefault(p => p.Id == Appointment.PatientId);
            }
            if (Appointment.PhysicianId != Guid.Empty)
            {
                SelectedPhysician = Physicians.FirstOrDefault(p => p.Id == Appointment.PhysicianId);
            }
        }

        public async void LoadAppointment(string appointmentId)
        {
            if (Guid.TryParse(appointmentId, out Guid id))
            {
                Appointment = await _appointmentService.GetAppointmentAsync(id);
            }
            else
            {
                Appointment = new Appointment { StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(1) };
            }
        }

        [RelayCommand]
        async Task SaveAppointmentAsync()
        {
            if (SelectedPatient == null || SelectedPhysician == null)
            {
                await Shell.Current.DisplayAlert("Error", "Please select a patient and a physician.", "OK");
                return;
            }

            Appointment.PatientId = SelectedPatient.Id;
            Appointment.PhysicianId = SelectedPhysician.Id;

            try
            {
                if (Appointment.Id == Guid.Empty)
                {
                    await _appointmentService.AddAppointmentAsync(Appointment);
                }
                else
                {
                    await _appointmentService.UpdateAppointmentAsync(Appointment);
                }
                await Shell.Current.GoToAsync(".."); // Navigate back
            }
            catch (InvalidOperationException ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        [RelayCommand]
        async Task CancelAsync()
        {
            await Shell.Current.GoToAsync(".."); // Navigate back
        }
    }
}