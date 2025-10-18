namespace Maui.TheraOffice.Models
{
    public class Physician
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string LicenseNumber { get; set; }
        public DateTime GraduationDate { get; set; }
        public List<string> Specializations { get; set; } = new List<string>();
    }
}