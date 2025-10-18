using Maui.TheraOffice.Models;

namespace Maui.TheraOffice.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly List<Appointment> _appointments = new();

        public async Task<IEnumerable<Appointment>> GetAppointmentsAsync()
        {
            return await Task.FromResult(_appointments);
        }

        public async Task<Appointment> GetAppointmentAsync(Guid id)
        {
            return await Task.FromResult(_appointments.FirstOrDefault(a => a.Id == id));
        }

        public async Task AddAppointmentAsync(Appointment appointment)
        {
            if (!await IsAppointmentTimeValidAsync(appointment.StartTime, appointment.EndTime))
            {
                throw new InvalidOperationException("Appointment time is outside of allowed hours (8 AM - 5 PM, Monday - Friday).");
            }

            if (await IsPhysicianDoubleBookedAsync(appointment.PhysicianId, appointment.StartTime, appointment.EndTime))
            {
                throw new InvalidOperationException("Physician is already booked for this time slot.");
            }

            _appointments.Add(appointment);
            await Task.CompletedTask;
        }

        public async Task UpdateAppointmentAsync(Appointment appointment)
        {
            var existingAppointment = _appointments.FirstOrDefault(a => a.Id == appointment.Id);
            if (existingAppointment != null)
            {
                if (!await IsAppointmentTimeValidAsync(appointment.StartTime, appointment.EndTime))
                {
                    throw new InvalidOperationException("Appointment time is outside of allowed hours (8 AM - 5 PM, Monday - Friday).");
                }

                if (await IsPhysicianDoubleBookedAsync(appointment.PhysicianId, appointment.StartTime, appointment.EndTime, appointment.Id))
                { 
                    throw new InvalidOperationException("Physician is already booked for this time slot.");
                }

                existingAppointment.PatientId = appointment.PatientId;
                existingAppointment.PhysicianId = appointment.PhysicianId;
                existingAppointment.StartTime = appointment.StartTime;
                existingAppointment.EndTime = appointment.EndTime;
                existingAppointment.Notes = appointment.Notes;
            }
            await Task.CompletedTask;
        }

        public async Task DeleteAppointmentAsync(Guid id)
        {
            var appointmentToRemove = _appointments.FirstOrDefault(a => a.Id == id);
            if (appointmentToRemove != null)
            {
                _appointments.Remove(appointmentToRemove);
            }
            await Task.CompletedTask;
        }

        public async Task<bool> IsPhysicianDoubleBookedAsync(Guid physicianId, DateTime startTime, DateTime endTime, Guid? appointmentId = null)
        {
            return await Task.FromResult(_appointments.Any(a =>
                a.PhysicianId == physicianId &&
                a.Id != appointmentId && // Exclude the current appointment if it's an update
                ((startTime < a.EndTime && endTime > a.StartTime))));
        }

        public async Task<bool> IsAppointmentTimeValidAsync(DateTime startTime, DateTime endTime)
        {
            // Check if it's Monday to Friday
            if (startTime.DayOfWeek == DayOfWeek.Saturday || startTime.DayOfWeek == DayOfWeek.Sunday)
            {
                return await Task.FromResult(false);
            }

            // Check if within 8 AM to 5 PM
            if (startTime.Hour < 8 || endTime.Hour > 17 || (endTime.Hour == 17 && endTime.Minute > 0))
            { 
                return await Task.FromResult(false);
            }

            return await Task.FromResult(true);
        }
    }
}