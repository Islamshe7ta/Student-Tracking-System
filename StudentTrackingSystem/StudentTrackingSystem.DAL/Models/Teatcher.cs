using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StudentTrackingSystem.DAL.Models
{
    public class Teatcher : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public string Subject { get; set; }  // المادة اللي بيشرحها مثلاً

        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        // Foreign Key
        public int SubjectId { get; set; }

        // Navigation Property
        public Subject subjects { get; set; }
    }
}
