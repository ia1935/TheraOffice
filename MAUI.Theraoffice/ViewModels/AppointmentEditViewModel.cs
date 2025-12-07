using System;
using System.Threading.Tasks;
using MAUI.Theraoffice.Models;
using MAUI.Theraoffice.Services;

namespace MAUI.Theraoffice.ViewModels
{
    public class AppointmentEditViewModel : BaseViewModel
    {
        private readonly IClinicStore _store;
        public Appointment Appointment { get; set; }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                Set(ref _startDate, value);
                UpdateAppointmentStart();
            }
        }

        private TimeSpan _startTime;
        public TimeSpan StartTime
        {
            get => _startTime;
            set
            {
                Set(ref _startTime, value);
                UpdateAppointmentStart();
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                Set(ref _endDate, value);
                UpdateAppointmentEnd();
            }
        }

        private TimeSpan _endTime;
        public TimeSpan EndTime
        {
            get => _endTime;
            set
            {
                Set(ref _endTime, value);
                UpdateAppointmentEnd();
            }
        }

        public AppointmentEditViewModel(IClinicStore store, Appointment? a = null)
        {
            _store = store;
            Appointment = a ?? new Appointment
            {
                Start = DateTime.Today.AddHours(9),
                End = DateTime.Today.AddHours(9).AddMinutes(30)
            };

            // initialize date/time backing fields
            _startDate = Appointment.Start.Date;
            _startTime = Appointment.Start.TimeOfDay;
            _endDate = Appointment.End.Date;
            _endTime = Appointment.End.TimeOfDay;
        }

        private void UpdateAppointmentStart()
        {
            Appointment.Start = StartDate.Date + StartTime;
        }

        private void UpdateAppointmentEnd()
        {
            Appointment.End = EndDate.Date + EndTime;
        }

        public Task<string?> SaveAsync()
        {
            if (Appointment.Id == Guid.Empty) Appointment.Id = Guid.NewGuid();
            // Ensure Appointment.Start/End are up to date
            UpdateAppointmentStart();
            UpdateAppointmentEnd();

            return _store.GetAppointmentAsync(Appointment.Id).ContinueWith<Task<string?>>(t =>
            {
                if (t.Result is null) return _store.TryAddAppointmentAsync(Appointment);
                return _store.TryUpdateAppointmentAsync(Appointment);
            }).Unwrap();
        }
    }
}
