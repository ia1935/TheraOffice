using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MAUI.Theraoffice.Models;

namespace MAUI.Theraoffice.Services
{
    public interface IClinicStore
    {
        // Patients
        Task<IEnumerable<Patient>> GetPatientsAsync();
        Task<Patient?> GetPatientAsync(Guid id);
        Task AddPatientAsync(Patient p);
        Task UpdatePatientAsync(Patient p);
        Task DeletePatientAsync(Guid id);

        // Physicians
        Task<IEnumerable<Physician>> GetPhysiciansAsync();
        Task<Physician?> GetPhysicianAsync(Guid id);
        Task AddPhysicianAsync(Physician p);
        Task UpdatePhysicianAsync(Physician p);
        Task DeletePhysicianAsync(Guid id);

        // Appointments
        Task<IEnumerable<Appointment>> GetAppointmentsAsync();
        Task<Appointment?> GetAppointmentAsync(Guid id);
        /// <summary>
        /// Try to add appointment. Returns null on success or error message on failure.
        /// </summary>
        Task<string?> TryAddAppointmentAsync(Appointment a);
        Task<string?> TryUpdateAppointmentAsync(Appointment a);
        Task DeleteAppointmentAsync(Guid id);
    }
}
