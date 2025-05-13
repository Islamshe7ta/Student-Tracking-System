
using System.ComponentModel.DataAnnotations;


namespace StudentTrackingSystem.PL.DTOs
{
    public class TeatcherDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string FullName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        //public string Subject { get; set; }

        public string Gender { get; set; }
        public int SubjectId { get; set; }

        public string? ImagePath { get; set; }

        public IFormFile? TeacherImage { get; set; }

        // Navigation Property
        // public Subject subjects { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }
}


