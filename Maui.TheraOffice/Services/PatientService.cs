using Maui.TheraOffice.Models;

namespace Maui.TheraOffice.Services
{
    public class PatientService : IPatientService
    {
        private readonly List<Patient> _patients = new();

        public async Task<IEnumerable<Patient>> GetPatientsAsync()
        {
            return await Task.FromResult(_patients);
        }

        public async Task<Patient> GetPatientAsync(Guid id)
        {
            return await Task.FromResult(_patients.FirstOrDefault(p => p.Id == id));
        }

        public async Task AddPatientAsync(Patient patient)
        {
            _patients.Add(patient);
            await Task.CompletedTask;
        }

        public async Task UpdatePatientAsync(Patient patient)
        {
            var existingPatient = _patients.FirstOrDefault(p => p.Id == patient.Id);
            if (existingPatient != null)
            {
                existingPatient.Name = patient.Name;
                existingPatient.Address = patient.Address;
                existingPatient.Birthdate = patient.Birthdate;
                existingPatient.Race = patient.Race;
                existingPatient.Gender = patient.Gender;
                existingPatient.Diagnoses = patient.Diagnoses;
                existingPatient.Prescriptions = patient.Prescriptions;
            }
            await Task.CompletedTask;
        }

        public async Task DeletePatientAsync(Guid id)
        {
            var patientToRemove = _patients.FirstOrDefault(p => p.Id == id);
            if (patientToRemove != null)
            {
                _patients.Remove(patientToRemove);
            }
            await Task.CompletedTask;
        }
    }
}