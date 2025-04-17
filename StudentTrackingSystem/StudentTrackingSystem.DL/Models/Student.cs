using System.ComponentModel.DataAnnotations;

namespace StudentTrackingSystem.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        [Required]
        [Display(Name = "Student Name")]
        [MaxLength(35)]
        [MinLength(5)]
        public string FullName { get; set; }


        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        [Required]
        public string EmailAddress { get; set; }

        [Display(Name = "Phone Number")]
        [Required]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Invalid phone number. It must be 11 digits and start with 010, 011, 012, or 015.")]
        public string PhoneNo { get; set; }

        [Display(Name = "Address")]
        [Required]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Grade")]
        public int Grade { get; set; }

    }
}
