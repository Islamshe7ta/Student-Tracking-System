
    using System.ComponentModel.DataAnnotations;

    namespace StudentTrackingSystem.PL.DTOs
    {
        public class StudentDTO
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

            [Required]
            [Display(Name = "Email Address")]
            [DataType(DataType.EmailAddress)]
            public string EmailAddress { get; set; }

            [Required]
            [Display(Name = "Phone Number")]
            [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Invalid phone number.")]
            public string PhoneNo { get; set; }

            [Required]
            [Display(Name = "Address")]
            public string Address { get; set; }

            [Required]
            [Display(Name = "Grade")]
            public int Grade { get; set; }
        }
    }


