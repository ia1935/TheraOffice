using System.Collections.Concurrent;
using Backend.TheraOffice.Models;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;
using System;

namespace Backend.TheraOffice.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ConcurrentDictionary<int, Patient> _patients = new();
        private int _nextId = 1;
        private readonly string _dataFile;
        private readonly object _fileLock = new();
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { WriteIndented = true };

        public PatientRepository()
        {
            var dataDir = Path.Combine(AppContext.BaseDirectory, "data");
            if (!Directory.Exists(dataDir)) Directory.CreateDirectory(dataDir);
            _dataFile = Path.Combine(dataDir, "patients.json");

            LoadFromFile();

            // seed if empty
            if (!_patients.Any())
            {
                var p = new Patient
                {
                    Id = _nextId++,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com"
                };
                _patients[p.Id] = p;
                SaveToFile();
            }
        }

        private void LoadFromFile()
        {
            try
            {
                if (!File.Exists(_dataFile)) return;
                var json = File.ReadAllText(_dataFile);
                var list = JsonSerializer.Deserialize<List<Patient>>(json);
                if (list == null) return;
                foreach (var p in list)
                {
                    _patients[p.Id] = p;
                }
                if (_patients.Any())
                {
                    _nextId = _patients.Keys.Max() + 1;
                }
            }
            catch
            {
                // ignore and start fresh
            }
        }

        private void SaveToFile()
        {
            try
            {
                lock (_fileLock)
                {
                    var list = _patients.Values.OrderBy(p => p.Id).ToList();
                    var json = JsonSerializer.Serialize(list, _jsonOptions);
                    File.WriteAllText(_dataFile, json);
                }
            }
            catch
            {
                // swallow IO errors for now
            }
        }

        public Patient Create(Patient patient)
        {
            var id = _nextId++;
            patient.Id = id;
            _patients[id] = patient;
            SaveToFile();
            return patient;
        }

        public bool Delete(int id)
        {
            var removed = _patients.TryRemove(id, out _);
            if (removed) SaveToFile();
            return removed;
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
            SaveToFile();
            return true;
        }
    }
}
