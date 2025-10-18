using Maui.TheraOffice.Models;

namespace Maui.TheraOffice.Services
{
    public class PhysicianService : IPhysicianService
    {
        private readonly List<Physician> _physicians = new();

        public async Task<IEnumerable<Physician>> GetPhysiciansAsync()
        {
            return await Task.FromResult(_physicians);
        }

        public async Task<Physician> GetPhysicianAsync(Guid id)
        {
            return await Task.FromResult(_physicians.FirstOrDefault(p => p.Id == id));
        }

        public async Task AddPhysicianAsync(Physician physician)
        {
            _physicians.Add(physician);
            await Task.CompletedTask;
        }

        public async Task UpdatePhysicianAsync(Physician physician)
        {
            var existingPhysician = _physicians.FirstOrDefault(p => p.Id == physician.Id);
            if (existingPhysician != null)
            {
                existingPhysician.Name = physician.Name;
                existingPhysician.LicenseNumber = physician.LicenseNumber;
                existingPhysician.GraduationDate = physician.GraduationDate;
                existingPhysician.Specializations = physician.Specializations;
            }
            await Task.CompletedTask;
        }

        public async Task DeletePhysicianAsync(Guid id)
        {
            var physicianToRemove = _physicians.FirstOrDefault(p => p.Id == id);
            if (physicianToRemove != null)
            {
                _physicians.Remove(physicianToRemove);
            }
            await Task.CompletedTask;
        }
    }
}