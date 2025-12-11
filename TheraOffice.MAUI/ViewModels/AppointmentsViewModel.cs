using System.Collections.ObjectModel;
using System.Windows.Input;
using TheraOffice.MAUI.Models;
using TheraOffice.MAUI.Services;

namespace TheraOffice.MAUI.ViewModels
{
    public class AppointmentsViewModel : BaseViewModel
    {
        private readonly IDataService _dataService;
        private ObservableCollection<AppointmentDisplayItem> _appointments = new();

        public ObservableCollection<AppointmentDisplayItem> Appointments
        {
            get => _appointments;
            set => SetProperty(ref _appointments, value);
        }

        public ICommand LoadAppointmentsCommand { get; }
        public ICommand AddAppointmentCommand { get; }
        public ICommand SelectAppointmentCommand { get; }
        public ICommand DeleteAppointmentCommand { get; }

        public AppointmentsViewModel(IDataService dataService)
        {
            _dataService = dataService;
            Title = "Appointments";

            LoadAppointmentsCommand = new Command(async () => await LoadAppointmentsAsync());
            AddAppointmentCommand = new Command(async () => await AddAppointmentAsync());
            SelectAppointmentCommand = new Command<AppointmentDisplayItem>(async (apt) => await SelectAppointmentAsync(apt));
            DeleteAppointmentCommand = new Command<AppointmentDisplayItem>(async (apt) => await DeleteAppointmentAsync(apt));
        }

        public async Task LoadAppointmentsAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var appointments = await _dataService.GetAppointmentsAsync();
                var patients = await _dataService.GetPatientsAsync();
                var physicians = await _dataService.GetPhysiciansAsync();

                Appointments.Clear();
                foreach (var apt in appointments.OrderBy(a => a.AppointmentDateTime))
                {
                    var patient = patients.FirstOrDefault(p => p.Id == apt.PatientId);
                    var physician = physicians.FirstOrDefault(p => p.Id == apt.PhysicianId);

                    Appointments.Add(new AppointmentDisplayItem
                    {
                        Appointment = apt,
                        PatientName = patient?.FullName ?? "Unknown",
                        PhysicianName = physician?.FullName ?? "Unknown"
                    });
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Unable to load appointments: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task AddAppointmentAsync()
        {
            await Shell.Current.GoToAsync("AppointmentDetail");
        }

        private async Task SelectAppointmentAsync(AppointmentDisplayItem appointmentItem)
        {
            if (appointmentItem == null)
                return;

            await Shell.Current.GoToAsync($"AppointmentDetail?appointmentId={appointmentItem.Appointment.Id}");
        }

        private async Task DeleteAppointmentAsync(AppointmentDisplayItem appointmentItem)
        {
            if (appointmentItem == null)
                return;

            bool confirm = await Shell.Current.DisplayAlert("Delete Appointment", 
                $"Are you sure you want to delete this appointment?", "Yes", "No");

            if (confirm)
            {
                await _dataService.DeleteAppointmentAsync(appointmentItem.Appointment.Id);
                await LoadAppointmentsAsync();
            }
        }
    }

    public class AppointmentDisplayItem
    {
        public Appointment Appointment { get; set; } = null!;
        public string PatientName { get; set; } = string.Empty;
        public string PhysicianName { get; set; } = string.Empty;
        public string DisplayText => $"{Appointment.AppointmentDateTime:g} - {PatientName} with {PhysicianName}";
    }
}
