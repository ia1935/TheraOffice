namespace TheraOffice.MAUI.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PhysicianId { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public int DurationMinutes { get; set; } = 30;
        public string Reason { get; set; } = string.Empty;
        public string Status { get; set; } = "Scheduled";

        public DateTime EndDateTime => AppointmentDateTime.AddMinutes(DurationMinutes);
    }
}
