using TheraOffice.MAUI.Models;

namespace TheraOffice.MAUI.Services
{
    public class DataService : IDataService
    {
        private readonly List<Patient> _patients = new();
        private readonly List<Physician> _physicians = new();
        private readonly List<Appointment> _appointments = new();
        private int _nextPatientId = 1;
        private int _nextPhysicianId = 1;
        private int _nextAppointmentId = 1;
        private int _nextNoteId = 1;

        public DataService()
        {
            SeedData();
        }

        private void SeedData()
        {
            var patient1 = new Patient
            {
                Id = _nextPatientId++,
                FirstName = "John",
                LastName = "Doe",
                Address = "123 Main St, City, State 12345",
                BirthDate = new DateTime(1980, 5, 15),
                Race = "Caucasian",
                Gender = "Male"
            };
            _patients.Add(patient1);

            var patient2 = new Patient
            {
                Id = _nextPatientId++,
                FirstName = "Jane",
                LastName = "Smith",
                Address = "456 Oak Ave, City, State 12345",
                BirthDate = new DateTime(1992, 8, 22),
                Race = "African American",
                Gender = "Female"
            };
            _patients.Add(patient2);

            var physician1 = new Physician
            {
                Id = _nextPhysicianId++,
                FirstName = "Sarah",
                LastName = "Johnson",
                LicenseNumber = "MD123456",
                GraduationDate = new DateTime(2005, 6, 1),
                Specializations = new List<string> { "Internal Medicine", "Cardiology" }
            };
            _physicians.Add(physician1);

            var physician2 = new Physician
            {
                Id = _nextPhysicianId++,
                FirstName = "Michael",
                LastName = "Chen",
                LicenseNumber = "MD789012",
                GraduationDate = new DateTime(2010, 5, 15),
                Specializations = new List<string> { "Family Medicine" }
            };
            _physicians.Add(physician2);
        }

        public Task<List<Patient>> GetPatientsAsync()
        {
            return Task.FromResult(_patients.ToList());
        }

        public Task<Patient?> GetPatientAsync(int id)
        {
            return Task.FromResult(_patients.FirstOrDefault(p => p.Id == id));
        }

        public Task<Patient> AddPatientAsync(Patient patient)
        {
            patient.Id = _nextPatientId++;
            _patients.Add(patient);
            return Task.FromResult(patient);
        }

        public Task<Patient> UpdatePatientAsync(Patient patient)
        {
            var existing = _patients.FirstOrDefault(p => p.Id == patient.Id);
            if (existing != null)
            {
                _patients.Remove(existing);
                _patients.Add(patient);
            }
            return Task.FromResult(patient);
        }

        public Task<bool> DeletePatientAsync(int id)
        {
            var patient = _patients.FirstOrDefault(p => p.Id == id);
            if (patient != null)
            {
                _patients.Remove(patient);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<List<Physician>> GetPhysiciansAsync()
        {
            return Task.FromResult(_physicians.ToList());
        }

        public Task<Physician?> GetPhysicianAsync(int id)
        {
            return Task.FromResult(_physicians.FirstOrDefault(p => p.Id == id));
        }

        public Task<Physician> AddPhysicianAsync(Physician physician)
        {
            physician.Id = _nextPhysicianId++;
            _physicians.Add(physician);
            return Task.FromResult(physician);
        }

        public Task<Physician> UpdatePhysicianAsync(Physician physician)
        {
            var existing = _physicians.FirstOrDefault(p => p.Id == physician.Id);
            if (existing != null)
            {
                _physicians.Remove(existing);
                _physicians.Add(physician);
            }
            return Task.FromResult(physician);
        }

        public Task<bool> DeletePhysicianAsync(int id)
        {
            var physician = _physicians.FirstOrDefault(p => p.Id == id);
            if (physician != null)
            {
                _physicians.Remove(physician);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<List<Appointment>> GetAppointmentsAsync()
        {
            return Task.FromResult(_appointments.ToList());
        }

        public Task<Appointment?> GetAppointmentAsync(int id)
        {
            return Task.FromResult(_appointments.FirstOrDefault(a => a.Id == id));
        }

        public async Task<Appointment> AddAppointmentAsync(Appointment appointment)
        {
            if (!await IsValidAppointmentTimeAsync(appointment.AppointmentDateTime))
            {
                throw new InvalidOperationException("Appointments can only be scheduled Monday-Friday, 8 AM to 5 PM.");
            }

            if (!await IsPhysicianAvailableAsync(appointment.PhysicianId, appointment.AppointmentDateTime, appointment.DurationMinutes))
            {
                throw new InvalidOperationException("Physician is not available at the selected time.");
            }

            appointment.Id = _nextAppointmentId++;
            _appointments.Add(appointment);
            return appointment;
        }

        public async Task<Appointment> UpdateAppointmentAsync(Appointment appointment)
        {
            if (!await IsValidAppointmentTimeAsync(appointment.AppointmentDateTime))
            {
                throw new InvalidOperationException("Appointments can only be scheduled Monday-Friday, 8 AM to 5 PM.");
            }

            if (!await IsPhysicianAvailableAsync(appointment.PhysicianId, appointment.AppointmentDateTime, appointment.DurationMinutes, appointment.Id))
            {
                throw new InvalidOperationException("Physician is not available at the selected time.");
            }

            var existing = _appointments.FirstOrDefault(a => a.Id == appointment.Id);
            if (existing != null)
            {
                _appointments.Remove(existing);
                _appointments.Add(appointment);
            }
            return appointment;
        }

        public Task<bool> DeleteAppointmentAsync(int id)
        {
            var appointment = _appointments.FirstOrDefault(a => a.Id == id);
            if (appointment != null)
            {
                _appointments.Remove(appointment);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<bool> IsPhysicianAvailableAsync(int physicianId, DateTime startTime, int durationMinutes, int? excludeAppointmentId = null)
        {
            var endTime = startTime.AddMinutes(durationMinutes);
            var conflicts = _appointments
                .Where(a => a.PhysicianId == physicianId && 
                           (!excludeAppointmentId.HasValue || a.Id != excludeAppointmentId.Value))
                .Any(a => (startTime >= a.AppointmentDateTime && startTime < a.EndDateTime) ||
                         (endTime > a.AppointmentDateTime && endTime <= a.EndDateTime) ||
                         (startTime <= a.AppointmentDateTime && endTime >= a.EndDateTime));

            return Task.FromResult(!conflicts);
        }

        private Task<bool> IsValidAppointmentTimeAsync(DateTime dateTime)
        {
            if (dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday)
                return Task.FromResult(false);

            var time = dateTime.TimeOfDay;
            var startTime = new TimeSpan(8, 0, 0);
            var endTime = new TimeSpan(17, 0, 0);

            return Task.FromResult(time >= startTime && time < endTime);
        }

        public Task<MedicalNote> AddMedicalNoteAsync(int patientId, MedicalNote note)
        {
            var patient = _patients.FirstOrDefault(p => p.Id == patientId);
            if (patient == null)
                throw new InvalidOperationException("Patient not found.");

            note.Id = _nextNoteId++;
            note.PatientId = patientId;
            patient.MedicalNotes.Add(note);
            return Task.FromResult(note);
        }

        public Task<MedicalNote> UpdateMedicalNoteAsync(MedicalNote note)
        {
            var patient = _patients.FirstOrDefault(p => p.Id == note.PatientId);
            if (patient == null)
                throw new InvalidOperationException("Patient not found.");

            var existing = patient.MedicalNotes.FirstOrDefault(n => n.Id == note.Id);
            if (existing != null)
            {
                patient.MedicalNotes.Remove(existing);
                patient.MedicalNotes.Add(note);
            }
            return Task.FromResult(note);
        }

        public Task<bool> DeleteMedicalNoteAsync(int patientId, int noteId)
        {
            var patient = _patients.FirstOrDefault(p => p.Id == patientId);
            if (patient == null)
                return Task.FromResult(false);

            var note = patient.MedicalNotes.FirstOrDefault(n => n.Id == noteId);
            if (note != null)
            {
                patient.MedicalNotes.Remove(note);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}
