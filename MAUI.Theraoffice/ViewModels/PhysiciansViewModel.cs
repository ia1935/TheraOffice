using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MAUI.Theraoffice.Models;
using MAUI.Theraoffice.Services;

namespace MAUI.Theraoffice.ViewModels
{
    public class PhysiciansViewModel : BaseViewModel
    {
        private readonly IClinicStore _store;
        public ObservableCollection<Physician> Physicians { get; } = new();

        public PhysiciansViewModel(IClinicStore store)
        {
            _store = store;
            Task.Run(Load).Wait();
        }

        public async Task Load()
        {
            Physicians.Clear();
            foreach (var p in await _store.GetPhysiciansAsync()) Physicians.Add(p);
        }

        public Task DeleteAsync(Physician p) => _store.DeletePhysicianAsync(p.Id);
    }
}
