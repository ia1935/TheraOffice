using Maui.TheraOffice.Models;

namespace Maui.TheraOffice.Services
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> GetAppointmentsAsync();
        Task<Appointment> GetAppointmentAsync(Guid id);
        Task AddAppointmentAsync(Appointment appointment);
        Task UpdateAppointmentAsync(Appointment appointment);
        Task DeleteAppointmentAsync(Guid id);
        Task<bool> IsPhysicianDoubleBookedAsync(Guid physicianId, DateTime startTime, DateTime endTime, Guid? appointmentId = null);
        Task<bool> IsAppointmentTimeValidAsync(DateTime startTime, DateTime endTime);
    }
}