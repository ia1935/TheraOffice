namespace TheraOffice.MAUI.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string Race { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public List<MedicalNote> MedicalNotes { get; set; } = new();

        public string FullName => $"{FirstName} {LastName}";
        public int Age => DateTime.Now.Year - BirthDate.Year - (DateTime.Now.DayOfYear < BirthDate.DayOfYear ? 1 : 0);
    }
}
