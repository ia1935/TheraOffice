using System;
using System.Collections.Generic;

namespace MAUI.Theraoffice.Models
{
    public class Patient
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; } = DateTime.Now.AddYears(-30);
        public string Race { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public List<MedicalNote> Notes { get; set; } = new();

        public string DisplayName => $"{FirstName} {LastName}";
    }
}
