using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MAUI.Theraoffice.Models;
using MAUI.Theraoffice.Services;

namespace MAUI.Theraoffice.ViewModels
{
    public class AppointmentsViewModel : BaseViewModel
    {
        private readonly IClinicStore _store;
        public ObservableCollection<Appointment> Appointments { get; } = new();

        public AppointmentsViewModel(IClinicStore store)
        {
            _store = store;
            Task.Run(Load).Wait();
        }

        public async Task Load()
        {
            Appointments.Clear();
            foreach (var a in await _store.GetAppointmentsAsync()) Appointments.Add(a);
        }

        public Task DeleteAsync(Appointment a) => _store.DeleteAppointmentAsync(a.Id);
    }
}
