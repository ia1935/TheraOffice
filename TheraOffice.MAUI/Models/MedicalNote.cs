namespace TheraOffice.MAUI.Models
{
    public class MedicalNote
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; } = string.Empty;
        public List<Diagnosis> Diagnoses { get; set; } = new();
        public List<Prescription> Prescriptions { get; set; } = new();
    }
}
