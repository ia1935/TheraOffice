using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Maui.TheraOffice.Models;
using Maui.TheraOffice.Services;
using System.Collections.ObjectModel;

namespace Maui.TheraOffice.ViewModels
{
    public partial class AppointmentListViewModel : ObservableObject
    {
        private readonly IAppointmentService _appointmentService;

        [ObservableProperty]
        ObservableCollection<Appointment> appointments;

        public AppointmentListViewModel(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
            Appointments = new ObservableCollection<Appointment>();
        }

        [RelayCommand]
        async Task LoadAppointmentsAsync()
        {
            var appointmentList = await _appointmentService.GetAppointmentsAsync();
            Appointments.Clear();
            foreach (var appointment in appointmentList)
            { 
                Appointments.Add(appointment);
            }
        }

        [RelayCommand]
        async Task AddAppointmentAsync()
        {
            // Navigation to Add/Edit Appointment Page
            await Shell.Current.GoToAsync("AppointmentDetailPage");
        }

        [RelayCommand]
        async Task EditAppointmentAsync(Appointment appointment)
        {
            // Navigation to Add/Edit Appointment Page with appointment ID
            await Shell.Current.GoToAsync($"AppointmentDetailPage?id={appointment.Id}");
        }

        [RelayCommand]
        async Task DeleteAppointmentAsync(Appointment appointment)
        {
            await _appointmentService.DeleteAppointmentAsync(appointment.Id);
            await LoadAppointmentsAsync();
        }
    }
}