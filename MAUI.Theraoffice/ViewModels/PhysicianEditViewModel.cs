using System;
using System.Linq;
using System.Threading.Tasks;
using MAUI.Theraoffice.Models;
using MAUI.Theraoffice.Services;

namespace MAUI.Theraoffice.ViewModels
{
    public class PhysicianEditViewModel : BaseViewModel
    {
        private readonly IClinicStore _store;
        public Physician Physician { get; set; }

        public PhysicianEditViewModel(IClinicStore store, Physician? p = null)
        {
            _store = store;
            Physician = p ?? new Physician();
        }

        public Task SaveAsync()
        {
            if (Physician.Id == Guid.Empty) Physician.Id = Guid.NewGuid();
            return _store.GetPhysicianAsync(Physician.Id).ContinueWith(t =>
            {
                if (t.Result is null) _store.AddPhysicianAsync(Physician);
                else _store.UpdatePhysicianAsync(Physician);
            });
        }
    }
}
