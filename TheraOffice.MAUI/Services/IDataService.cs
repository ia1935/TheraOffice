using TheraOffice.MAUI.Models;

namespace TheraOffice.MAUI.Services
{
    public interface IDataService
    {
        Task<List<Patient>> GetPatientsAsync();
        Task<Patient?> GetPatientAsync(int id);
        Task<Patient> AddPatientAsync(Patient patient);
        Task<Patient> UpdatePatientAsync(Patient patient);
        Task<bool> DeletePatientAsync(int id);

        Task<List<Physician>> GetPhysiciansAsync();
        Task<Physician?> GetPhysicianAsync(int id);
        Task<Physician> AddPhysicianAsync(Physician physician);
        Task<Physician> UpdatePhysicianAsync(Physician physician);
        Task<bool> DeletePhysicianAsync(int id);

        Task<List<Appointment>> GetAppointmentsAsync();
        Task<Appointment?> GetAppointmentAsync(int id);
        Task<Appointment> AddAppointmentAsync(Appointment appointment);
        Task<Appointment> UpdateAppointmentAsync(Appointment appointment);
        Task<bool> DeleteAppointmentAsync(int id);
        Task<bool> IsPhysicianAvailableAsync(int physicianId, DateTime startTime, int durationMinutes, int? excludeAppointmentId = null);

        Task<MedicalNote> AddMedicalNoteAsync(int patientId, MedicalNote note);
        Task<MedicalNote> UpdateMedicalNoteAsync(MedicalNote note);
        Task<bool> DeleteMedicalNoteAsync(int patientId, int noteId);
    }
}
