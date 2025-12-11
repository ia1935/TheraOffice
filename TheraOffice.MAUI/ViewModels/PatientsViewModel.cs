using System.Collections.ObjectModel;
using System.Windows.Input;
using TheraOffice.MAUI.Models;
using TheraOffice.MAUI.Services;

namespace TheraOffice.MAUI.ViewModels
{
    public class PatientsViewModel : BaseViewModel
    {
        private readonly IDataService _dataService;
        private ObservableCollection<Patient> _patients = new();

        public ObservableCollection<Patient> Patients
        {
            get => _patients;
            set => SetProperty(ref _patients, value);
        }

        public ICommand LoadPatientsCommand { get; }
        public ICommand AddPatientCommand { get; }
        public ICommand SelectPatientCommand { get; }
        public ICommand DeletePatientCommand { get; }

        public PatientsViewModel(IDataService dataService)
        {
            _dataService = dataService;
            Title = "Patients";

            LoadPatientsCommand = new Command(async () => await LoadPatientsAsync());
            AddPatientCommand = new Command(async () => await AddPatientAsync());
            SelectPatientCommand = new Command<Patient>(async (patient) => await SelectPatientAsync(patient));
            DeletePatientCommand = new Command<Patient>(async (patient) => await DeletePatientAsync(patient));
        }

        public async Task LoadPatientsAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var patients = await _dataService.GetPatientsAsync();
                Patients.Clear();
                foreach (var patient in patients)
                {
                    Patients.Add(patient);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Unable to load patients: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task AddPatientAsync()
        {
            await Shell.Current.GoToAsync("PatientDetail");
        }

        private async Task SelectPatientAsync(Patient patient)
        {
            if (patient == null)
                return;

            await Shell.Current.GoToAsync($"PatientDetail?patientId={patient.Id}");
        }

        private async Task DeletePatientAsync(Patient patient)
        {
            if (patient == null)
                return;

            bool confirm = await Shell.Current.DisplayAlert("Delete Patient", 
                $"Are you sure you want to delete {patient.FullName}?", "Yes", "No");

            if (confirm)
            {
                await _dataService.DeletePatientAsync(patient.Id);
                await LoadPatientsAsync();
            }
        }
    }
}
