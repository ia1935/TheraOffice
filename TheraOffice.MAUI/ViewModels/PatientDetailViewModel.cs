using System.Windows.Input;
using TheraOffice.MAUI.Models;
using TheraOffice.MAUI.Services;

namespace TheraOffice.MAUI.ViewModels
{
    [QueryProperty(nameof(PatientId), "patientId")]
    public class PatientDetailViewModel : BaseViewModel
    {
        private readonly IDataService _dataService;
        private int _patientId;
        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private string _address = string.Empty;
        private DateTime _birthDate = DateTime.Now.AddYears(-30);
        private string _race = string.Empty;
        private string _gender = string.Empty;

        public int PatientId
        {
            get => _patientId;
            set
            {
                _patientId = value;
                LoadPatient(value);
            }
        }

        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }

        public DateTime BirthDate
        {
            get => _birthDate;
            set => SetProperty(ref _birthDate, value);
        }

        public string Race
        {
            get => _race;
            set => SetProperty(ref _race, value);
        }

        public string Gender
        {
            get => _gender;
            set => SetProperty(ref _gender, value);
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public PatientDetailViewModel(IDataService dataService)
        {
            _dataService = dataService;
            Title = "Patient Details";

            SaveCommand = new Command(async () => await SavePatientAsync());
            CancelCommand = new Command(async () => await CancelAsync());
        }

        private async void LoadPatient(int id)
        {
            if (id == 0)
            {
                Title = "New Patient";
                return;
            }

            try
            {
                IsBusy = true;
                var patient = await _dataService.GetPatientAsync(id);
                if (patient != null)
                {
                    Title = "Edit Patient";
                    FirstName = patient.FirstName;
                    LastName = patient.LastName;
                    Address = patient.Address;
                    BirthDate = patient.BirthDate;
                    Race = patient.Race;
                    Gender = patient.Gender;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Unable to load patient: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task SavePatientAsync()
        {
            if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName))
            {
                await Shell.Current.DisplayAlert("Validation Error", "First name and last name are required.", "OK");
                return;
            }

            try
            {
                IsBusy = true;

                var patient = new Patient
                {
                    Id = PatientId,
                    FirstName = FirstName,
                    LastName = LastName,
                    Address = Address,
                    BirthDate = BirthDate,
                    Race = Race,
                    Gender = Gender
                };

                if (PatientId == 0)
                {
                    await _dataService.AddPatientAsync(patient);
                }
                else
                {
                    await _dataService.UpdatePatientAsync(patient);
                }

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Unable to save patient: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task CancelAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
