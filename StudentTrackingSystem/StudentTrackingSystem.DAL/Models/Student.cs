using StudentTrackingSystem.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace StudentTrackingSystem.Models
{
    public class Student : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNo { get; set; }
        public int Grade { get; set; }
        public string Address { get; set; }
         

    }
}
