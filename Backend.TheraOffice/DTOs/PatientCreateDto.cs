using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.TheraOffice.DTOs
{
    public class PatientCreateDto
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [EmailAddress]
        public string? Email { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }
}
