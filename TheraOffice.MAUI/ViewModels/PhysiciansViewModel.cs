using System.Collections.ObjectModel;
using System.Windows.Input;
using TheraOffice.MAUI.Models;
using TheraOffice.MAUI.Services;

namespace TheraOffice.MAUI.ViewModels
{
    public class PhysiciansViewModel : BaseViewModel
    {
        private readonly IDataService _dataService;
        private ObservableCollection<Physician> _physicians = new();

        public ObservableCollection<Physician> Physicians
        {
            get => _physicians;
            set => SetProperty(ref _physicians, value);
        }

        public ICommand LoadPhysiciansCommand { get; }
        public ICommand AddPhysicianCommand { get; }
        public ICommand SelectPhysicianCommand { get; }
        public ICommand DeletePhysicianCommand { get; }

        public PhysiciansViewModel(IDataService dataService)
        {
            _dataService = dataService;
            Title = "Physicians";

            LoadPhysiciansCommand = new Command(async () => await LoadPhysiciansAsync());
            AddPhysicianCommand = new Command(async () => await AddPhysicianAsync());
            SelectPhysicianCommand = new Command<Physician>(async (physician) => await SelectPhysicianAsync(physician));
            DeletePhysicianCommand = new Command<Physician>(async (physician) => await DeletePhysicianAsync(physician));
        }

        public async Task LoadPhysiciansAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var physicians = await _dataService.GetPhysiciansAsync();
                Physicians.Clear();
                foreach (var physician in physicians)
                {
                    Physicians.Add(physician);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Unable to load physicians: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task AddPhysicianAsync()
        {
            await Shell.Current.GoToAsync("PhysicianDetail");
        }

        private async Task SelectPhysicianAsync(Physician physician)
        {
            if (physician == null)
                return;

            await Shell.Current.GoToAsync($"PhysicianDetail?physicianId={physician.Id}");
        }

        private async Task DeletePhysicianAsync(Physician physician)
        {
            if (physician == null)
                return;

            bool confirm = await Shell.Current.DisplayAlert("Delete Physician", 
                $"Are you sure you want to delete {physician.FullName}?", "Yes", "No");

            if (confirm)
            {
                await _dataService.DeletePhysicianAsync(physician.Id);
                await LoadPhysiciansAsync();
            }
        }
    }
}
