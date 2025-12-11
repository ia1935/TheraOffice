using System.Collections.ObjectModel;
using System.Windows.Input;
using TheraOffice.MAUI.Models;
using TheraOffice.MAUI.Services;

namespace TheraOffice.MAUI.ViewModels
{
    [QueryProperty(nameof(AppointmentId), "appointmentId")]
    public class AppointmentDetailViewModel : BaseViewModel
    {
        private readonly IDataService _dataService;
        private int _appointmentId;
        private Patient? _selectedPatient;
        private Physician? _selectedPhysician;
        private DateTime _appointmentDate = DateTime.Today;
        private TimeSpan _appointmentTime = new TimeSpan(9, 0, 0);
        private int _durationMinutes = 30;
        private int _selectedDurationIndex = 1;
        private string _reason = string.Empty;
        private string _status = "Scheduled";
        private ObservableCollection<Patient> _patients = new();
        private ObservableCollection<Physician> _physicians = new();
        private List<int> _durationOptions = new() { 15, 30, 45, 60, 90, 120 };

        public int AppointmentId
        {
            get => _appointmentId;
            set
            {
                _appointmentId = value;
                LoadAppointment(value);
            }
        }

        public Patient? SelectedPatient
        {
            get => _selectedPatient;
            set => SetProperty(ref _selectedPatient, value);
        }

        public Physician? SelectedPhysician
        {
            get => _selectedPhysician;
            set => SetProperty(ref _selectedPhysician, value);
        }

        public DateTime AppointmentDate
        {
            get => _appointmentDate;
            set => SetProperty(ref _appointmentDate, value);
        }

        public TimeSpan AppointmentTime
        {
            get => _appointmentTime;
            set => SetProperty(ref _appointmentTime, value);
        }

        public int DurationMinutes
        {
            get => _durationMinutes;
            set => SetProperty(ref _durationMinutes, value);
        }

        public int SelectedDurationIndex
        {
            get => _selectedDurationIndex;
            set
            {
                if (SetProperty(ref _selectedDurationIndex, value) && value >= 0 && value < _durationOptions.Count)
                {
                    DurationMinutes = _durationOptions[value];
                }
            }
        }

        public List<int> DurationOptions
        {
            get => _durationOptions;
            set => SetProperty(ref _durationOptions, value);
        }

        public string Reason
        {
            get => _reason;
            set => SetProperty(ref _reason, value);
        }

        public string Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        public ObservableCollection<Patient> Patients
        {
            get => _patients;
            set => SetProperty(ref _patients, value);
        }

        public ObservableCollection<Physician> Physicians
        {
            get => _physicians;
            set => SetProperty(ref _physicians, value);
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public AppointmentDetailViewModel(IDataService dataService)
        {
            _dataService = dataService;
            Title = "Appointment Details";

            SaveCommand = new Command(async () => await SaveAppointmentAsync());
            CancelCommand = new Command(async () => await CancelAsync());

            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            try
            {
                var patients = await _dataService.GetPatientsAsync();
                var physicians = await _dataService.GetPhysiciansAsync();

                Patients.Clear();
                foreach (var patient in patients)
                {
                    Patients.Add(patient);
                }

                Physicians.Clear();
                foreach (var physician in physicians)
                {
                    Physicians.Add(physician);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Unable to load data: {ex.Message}", "OK");
            }
        }

        private async void LoadAppointment(int id)
        {
            if (id == 0)
            {
                Title = "New Appointment";
                return;
            }

            try
            {
                IsBusy = true;
                var appointment = await _dataService.GetAppointmentAsync(id);
                if (appointment != null)
                {
                    Title = "Edit Appointment";
                    SelectedPatient = Patients.FirstOrDefault(p => p.Id == appointment.PatientId);
                    SelectedPhysician = Physicians.FirstOrDefault(p => p.Id == appointment.PhysicianId);
                    AppointmentDate = appointment.AppointmentDateTime.Date;
                    AppointmentTime = appointment.AppointmentDateTime.TimeOfDay;
                    DurationMinutes = appointment.DurationMinutes;
                    SelectedDurationIndex = _durationOptions.IndexOf(appointment.DurationMinutes);
                    if (SelectedDurationIndex < 0) SelectedDurationIndex = 1;
                    Reason = appointment.Reason;
                    Status = appointment.Status;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Unable to load appointment: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task SaveAppointmentAsync()
        {
            if (SelectedPatient == null || SelectedPhysician == null)
            {
                await Shell.Current.DisplayAlert("Validation Error", "Please select both a patient and a physician.", "OK");
                return;
            }

            try
            {
                IsBusy = true;

                var appointmentDateTime = AppointmentDate.Date + AppointmentTime;

                var appointment = new Appointment
                {
                    Id = AppointmentId,
                    PatientId = SelectedPatient.Id,
                    PhysicianId = SelectedPhysician.Id,
                    AppointmentDateTime = appointmentDateTime,
                    DurationMinutes = DurationMinutes,
                    Reason = Reason,
                    Status = Status
                };

                if (AppointmentId == 0)
                {
                    await _dataService.AddAppointmentAsync(appointment);
                }
                else
                {
                    await _dataService.UpdateAppointmentAsync(appointment);
                }

                await Shell.Current.GoToAsync("..");
            }
            catch (InvalidOperationException ex)
            {
                await Shell.Current.DisplayAlert("Scheduling Error", ex.Message, "OK");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Unable to save appointment: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task CancelAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
