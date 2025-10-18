namespace Maui.TheraOffice.Models
{
    public class Patient
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime Birthdate { get; set; }
        public string Race { get; set; }
        public string Gender { get; set; }
        public List<string> Diagnoses { get; set; } = new List<string>();
        public List<string> Prescriptions { get; set; } = new List<string>();
    }
}