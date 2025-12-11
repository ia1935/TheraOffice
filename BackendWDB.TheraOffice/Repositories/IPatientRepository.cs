using System.Collections.Generic;
using Backend.TheraOffice.Models;

namespace Backend.TheraOffice.Repositories
{
    public interface IPatientRepository
    {
        IEnumerable<Patient> GetAll();
        Patient? GetById(int id);
        Patient Create(Patient patient);
        bool Update(Patient patient);
        bool Delete(int id);
        IEnumerable<Patient> Search(string query);
    }
}
