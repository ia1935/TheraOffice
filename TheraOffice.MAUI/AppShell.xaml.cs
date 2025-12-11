using TheraOffice.MAUI.Views;

namespace TheraOffice.MAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("PatientDetail", typeof(PatientDetailPage));
            Routing.RegisterRoute("PhysicianDetail", typeof(PhysicianDetailPage));
            Routing.RegisterRoute("AppointmentDetail", typeof(AppointmentDetailPage));
        }
    }
}
