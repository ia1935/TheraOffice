using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MAUI.Theraoffice.Models;
using MAUI.Theraoffice.Services;

namespace MAUI.Theraoffice.ViewModels
{
    public class PatientsViewModel : BaseViewModel
    {
        private readonly IClinicStore _store;
        public ObservableCollection<Patient> Patients { get; } = new();

        public PatientsViewModel(IClinicStore store)
        {
            _store = store;
            Task.Run(Load).Wait();
        }

        public async Task Load()
        {
            Patients.Clear();
            foreach (var p in await _store.GetPatientsAsync()) Patients.Add(p);
        }

        public Task DeleteAsync(Patient p) => _store.DeletePatientAsync(p.Id);
    }
}
