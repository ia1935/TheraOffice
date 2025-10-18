using Maui.TheraOffice.Models;

namespace Maui.TheraOffice.Services
{
    public interface IPhysicianService
    {
        Task<IEnumerable<Physician>> GetPhysiciansAsync();
        Task<Physician> GetPhysicianAsync(Guid id);
        Task AddPhysicianAsync(Physician physician);
        Task UpdatePhysicianAsync(Physician physician);
        Task DeletePhysicianAsync(Guid id);
    }
}