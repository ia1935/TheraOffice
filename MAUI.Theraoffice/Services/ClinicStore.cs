using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MAUI.Theraoffice.Models;

namespace MAUI.Theraoffice.Services
{
    public class ClinicStore : IClinicStore
    {
        private readonly List<Patient> _patients = new();
        private readonly List<Physician> _physicians = new();
        private readonly List<Appointment> _appointments = new();

        public ClinicStore()
        {
            // seed sample data
            var doc = new Physician
            {
                FirstName = "Alice",
                LastName = "Smith",
                LicenseNumber = "MD12345",
                GraduationDate = DateTime.Now.AddYears(-12),
                Specializations = new List<string> { "Family Medicine" }
            };
            _physicians.Add(doc);

            var patient = new Patient
            {
                FirstName = "John",
                LastName = "Doe",
                Address = "123 Main St",
                BirthDate = DateTime.Now.AddYears(-40),
                Race = "Unknown",
                Gender = "Male"
            };
            patient.Notes.Add(new MedicalNote { Diagnosis = "Hypertension", Prescription = "Lisinopril" });
            _patients.Add(patient);

            _appointments.Add(new Appointment
            {
                PatientId = patient.Id,
                PhysicianId = doc.Id,
                Start = DateTime.Today.AddHours(9),
                End = DateTime.Today.AddHours(9).AddMinutes(30),
                Notes = "Follow-up"
            });
        }

        // Patients
        public Task AddPatientAsync(Patient p)
        {
            _patients.Add(p);
            return Task.CompletedTask;
        }

        public Task DeletePatientAsync(Guid id)
        {
            _patients.RemoveAll(x => x.Id == id);
            _appointments.RemoveAll(a => a.PatientId == id);
            return Task.CompletedTask;
        }

        public Task<Patient?> GetPatientAsync(Guid id) => Task.FromResult(_patients.FirstOrDefault(p => p.Id == id));

        public Task<IEnumerable<Patient>> GetPatientsAsync() => Task.FromResult(_patients.AsEnumerable());

        public Task UpdatePatientAsync(Patient p)
        {
            var idx = _patients.FindIndex(x => x.Id == p.Id);
            if (idx >= 0) _patients[idx] = p;
            return Task.CompletedTask;
        }

        // Physicians
        public Task AddPhysicianAsync(Physician p)
        {
            _physicians.Add(p);
            return Task.CompletedTask;
        }

        public Task DeletePhysicianAsync(Guid id)
        {
            _physicians.RemoveAll(x => x.Id == id);
            _appointments.RemoveAll(a => a.PhysicianId == id);
            return Task.CompletedTask;
        }

        public Task<Physician?> GetPhysicianAsync(Guid id) => Task.FromResult(_physicians.FirstOrDefault(p => p.Id == id));

        public Task<IEnumerable<Physician>> GetPhysiciansAsync() => Task.FromResult(_physicians.AsEnumerable());

        public Task UpdatePhysicianAsync(Physician p)
        {
            var idx = _physicians.FindIndex(x => x.Id == p.Id);
            if (idx >= 0) _physicians[idx] = p;
            return Task.CompletedTask;
        }

        // Appointments
        public Task<IEnumerable<Appointment>> GetAppointmentsAsync() => Task.FromResult(_appointments.AsEnumerable());

        public Task<Appointment?> GetAppointmentAsync(Guid id) => Task.FromResult(_appointments.FirstOrDefault(a => a.Id == id));

        public Task<string?> TryAddAppointmentAsync(Appointment a)
        {
            var validation = ValidateAppointment(a, ignoreAppointmentId: null);
            if (validation != null) return Task.FromResult<string?>(validation);

            _appointments.Add(a);
            return Task.FromResult<string?>(null);
        }

        public Task<string?> TryUpdateAppointmentAsync(Appointment a)
        {
            var validation = ValidateAppointment(a, ignoreAppointmentId: a.Id);
            if (validation != null) return Task.FromResult<string?>(validation);

            var idx = _appointments.FindIndex(x => x.Id == a.Id);
            if (idx >= 0) _appointments[idx] = a;
            return Task.FromResult<string?>(null);
        }

        public Task DeleteAppointmentAsync(Guid id)
        {
            _appointments.RemoveAll(x => x.Id == id);
            return Task.CompletedTask;
        }

        private string? ValidateAppointment(Appointment a, Guid? ignoreAppointmentId)
        {
            var localStart = a.Start.ToLocalTime();
            var localEnd = a.End.ToLocalTime();

            if (localStart.Date != localEnd.Date)
                return "Appointment must start and end on the same day.";

            var startHour = localStart.TimeOfDay;
            var endHour = localEnd.TimeOfDay;
            var earliest = TimeSpan.FromHours(8);
            var latest = TimeSpan.FromHours(17);

            if (startHour < earliest || endHour > latest || endHour <= startHour)
                return "Appointments must be between 8:00 and 17:00 and End must be after Start.";

            if (localStart.DayOfWeek == DayOfWeek.Saturday || localStart.DayOfWeek == DayOfWeek.Sunday)
                return "Appointments may only be scheduled Monday through Friday.";

            var overlapping = _appointments.Where(x => x.PhysicianId == a.PhysicianId);
            if (ignoreAppointmentId.HasValue)
                overlapping = overlapping.Where(x => x.Id != ignoreAppointmentId.Value);

            var hasConflict = overlapping.Any(x => a.Start < x.End && a.End > x.Start);
            if (hasConflict)
                return "Physician is already booked during this time.";

            return null;
        }
    }
}
