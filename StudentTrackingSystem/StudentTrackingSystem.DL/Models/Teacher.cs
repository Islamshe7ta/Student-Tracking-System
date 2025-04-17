using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StudentTrackingSystem.DAL.Models
{
    class Teacher
    {
        public int TeacherId { get; set; }
        public string TeacherPassword { get; set; }

        [Required]
        [Display(Name = "Teacher Name")]
        [MaxLength(35)]
        [MinLength(5)]
        public string TeacherName { get; set; }

        [Required(ErrorMessage = "Teacher email is required.")]
        [Display(Name = "Teacher Email")]
        [MaxLength(35, ErrorMessage = "Teacher email cannot be more than 35 characters.")]
        [MinLength(5, ErrorMessage = "Teacher email must be at least 5 characters.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string TeacherEmail { get; set; }


        [Required(ErrorMessage = "Teacher phone number is required.")]
        [Display(Name = "Teacher Phone")]
        [MaxLength(11, ErrorMessage = "Teacher phone number must be exactly 11 digits.")]
        [MinLength(11, ErrorMessage = "Teacher phone number must be exactly 11 digits.")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Invalid phone number. It must start with 010, 011, 012, or 015 and contain 11 digits.")]
        public string TeacherPhone { get; set; }


        [Required(ErrorMessage = "Age is required.")]
        [Display(Name = "Teacher Age")]
        [Range(18, 100, ErrorMessage = "Age must be between 18 and 100.")]
        public int Age { get; set; }

        public string Gender { get; set; }

        [Display(Name = "Address")]
        [Required]
        public string AddressTeacher { get; set; }

        [RegularExpression(@".*\.(jpg|jpeg|png)$", ErrorMessage = "Only image files with .jpg, .jpeg, .png, or .gif extensions are allowed.")]
        public string TeacherImage { get; set; }

        [Required]
        public string TeachingSubject { get; set; }




    }
}
