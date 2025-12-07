namespace MAUI.Theraoffice
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void OnPatients(object? sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync("patients");
        }

        async void OnPhysicians(object? sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync("physicians");
        }

        async void OnAppointments(object? sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync("appointments");
        }
    }

}
