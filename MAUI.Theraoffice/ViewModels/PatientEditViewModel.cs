using System;
using System.Threading.Tasks;
using MAUI.Theraoffice.Models;
using MAUI.Theraoffice.Services;

namespace MAUI.Theraoffice.ViewModels
{
    public class PatientEditViewModel : BaseViewModel
    {
        private readonly IClinicStore _store;
        public Patient Patient { get; set; }

        public PatientEditViewModel(IClinicStore store, Patient? p = null)
        {
            _store = store;
            Patient = p ?? new Patient();
        }

        public Task SaveAsync()
        {
            if (Patient.Id == Guid.Empty) Patient.Id = Guid.NewGuid();
            // Determine if exists
            return _store.GetPatientAsync(Patient.Id).ContinueWith(t =>
            {
                if (t.Result is null) _store.AddPatientAsync(Patient);
                else _store.UpdatePatientAsync(Patient);
            });
        }
    }
}
