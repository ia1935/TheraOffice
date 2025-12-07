using MAUI.Theraoffice.Views;
using Microsoft.Maui.Controls;

namespace MAUI.Theraoffice
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Register routes
            Routing.RegisterRoute("patients", typeof(PatientsPage));
            Routing.RegisterRoute("patientEdit", typeof(PatientEditPage));
            Routing.RegisterRoute("physicians", typeof(PhysiciansPage));
            Routing.RegisterRoute("physicianEdit", typeof(PhysicianEditPage));
            Routing.RegisterRoute("appointments", typeof(AppointmentsPage));
            Routing.RegisterRoute("appointmentEdit", typeof(AppointmentEditPage));
        }
    }
}
