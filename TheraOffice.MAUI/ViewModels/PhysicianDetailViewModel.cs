using System.Windows.Input;
using TheraOffice.MAUI.Models;
using TheraOffice.MAUI.Services;

namespace TheraOffice.MAUI.ViewModels
{
    [QueryProperty(nameof(PhysicianId), "physicianId")]
    public class PhysicianDetailViewModel : BaseViewModel
    {
        private readonly IDataService _dataService;
        private int _physicianId;
        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private string _licenseNumber = string.Empty;
        private DateTime _graduationDate = DateTime.Now.AddYears(-10);
        private string _specializationsText = string.Empty;

        public int PhysicianId
        {
            get => _physicianId;
            set
            {
                _physicianId = value;
                LoadPhysician(value);
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

        public string LicenseNumber
        {
            get => _licenseNumber;
            set => SetProperty(ref _licenseNumber, value);
        }

        public DateTime GraduationDate
        {
            get => _graduationDate;
            set => SetProperty(ref _graduationDate, value);
        }

        public string SpecializationsText
        {
            get => _specializationsText;
            set => SetProperty(ref _specializationsText, value);
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public PhysicianDetailViewModel(IDataService dataService)
        {
            _dataService = dataService;
            Title = "Physician Details";

            SaveCommand = new Command(async () => await SavePhysicianAsync());
            CancelCommand = new Command(async () => await CancelAsync());
        }

        private async void LoadPhysician(int id)
        {
            if (id == 0)
            {
                Title = "New Physician";
                return;
            }

            try
            {
                IsBusy = true;
                var physician = await _dataService.GetPhysicianAsync(id);
                if (physician != null)
                {
                    Title = "Edit Physician";
                    FirstName = physician.FirstName;
                    LastName = physician.LastName;
                    LicenseNumber = physician.LicenseNumber;
                    GraduationDate = physician.GraduationDate;
                    SpecializationsText = string.Join(", ", physician.Specializations);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Unable to load physician: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task SavePhysicianAsync()
        {
            if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName))
            {
                await Shell.Current.DisplayAlert("Validation Error", "First name and last name are required.", "OK");
                return;
            }

            try
            {
                IsBusy = true;

                var specializations = SpecializationsText
                    .Split(',')
                    .Select(s => s.Trim())
                    .Where(s => !string.IsNullOrWhiteSpace(s))
                    .ToList();

                var physician = new Physician
                {
                    Id = PhysicianId,
                    FirstName = FirstName,
                    LastName = LastName,
                    LicenseNumber = LicenseNumber,
                    GraduationDate = GraduationDate,
                    Specializations = specializations
                };

                if (PhysicianId == 0)
                {
                    await _dataService.AddPhysicianAsync(physician);
                }
                else
                {
                    await _dataService.UpdatePhysicianAsync(physician);
                }

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Unable to save physician: {ex.Message}", "OK");
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
