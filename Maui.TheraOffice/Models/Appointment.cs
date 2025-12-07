using System;

namespace MAUI.Theraoffice.Models
{
    public class Appointment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid PatientId { get; set; }
        public Guid PhysicianId { get; set; }
        public DateTime Start { get; set; } = DateTime.Today.AddHours(9);
        public DateTime End { get; set; } = DateTime.Today.AddHours(10);
        public string Notes { get; set; } = string.Empty;
    }
}
