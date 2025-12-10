using System.Collections.Concurrent;
using Backend.TheraOffice.Models;

namespace Backend.TheraOffice.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ConcurrentDictionary<int, Patient> _patients = new();
        private int _nextId = 1;

        public PatientRepository()
        {
            // seed with a sample
            var p = new Patient
            {
                Id = _nextId++,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            };
            _patients[p.Id] = p;
        }

        public Patient Create(Patient patient)
        {
            var id = _nextId++;
            patient.Id = id;
            _patients[id] = patient;
            return patient;
        }

        public bool Delete(int id)
        {
            return _patients.TryRemove(id, out _);
        }

        public IEnumerable<Patient> GetAll()
        {
            return _patients.Values.OrderBy(p => p.Id);
        }

        public Patient? GetById(int id)
        {
            _patients.TryGetValue(id, out var p);
            return p;
        }

        public IEnumerable<Patient> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return GetAll();
            query = query.Trim().ToLowerInvariant();
            return _patients.Values.Where(p =>
                (!string.IsNullOrEmpty(p.FirstName) && p.FirstName.ToLowerInvariant().Contains(query)) ||
                (!string.IsNullOrEmpty(p.LastName) && p.LastName.ToLowerInvariant().Contains(query)) ||
                (!string.IsNullOrEmpty(p.Email) && p.Email.ToLowerInvariant().Contains(query))
            );
        }

        public bool Update(Patient patient)
        {
            if (!_patients.ContainsKey(patient.Id)) return false;
            _patients[patient.Id] = patient;
            return true;
        }
    }
}
