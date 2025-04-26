using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackingSystem.DAL.Models
{
    
        public class Parent : BaseEntity
        {
            public int Id { get; set; }

            [Required]
            [MaxLength(35)]
            public string FullName { get; set; }

            [Required]
            [EmailAddress]
            public string EmailAddress { get; set; }

            [Required]
            [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Invalid phone number.")]
            public string PhoneNo { get; set; }
       
            // علاقة مع الطالب
            public ICollection<Student> Students { get; set; }
        }

    }


