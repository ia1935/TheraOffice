using System;
using System.Collections.Generic;

namespace MAUI.Theraoffice.Models
{
    public class Physician
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public DateTime GraduationDate { get; set; } = DateTime.Now.AddYears(-10);
        public List<string> Specializations { get; set; } = new();

        public string DisplayName => $"{FirstName} {LastName}";
    }
}

