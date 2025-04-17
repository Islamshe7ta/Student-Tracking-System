using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StudentTrackingSystem.DAL.Models
{
    class Parent
    {
        
        public int ParentId { get; set; }

        [Required]
        [Display(Name = "Parent Name")]
        [MaxLength(35)]
        [MinLength(5)]
        public string ParentName { get; set; }

        [Display(Name = "Phone Number")]
        [Required]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Invalid phone number. It must be 11 digits and start with 010, 011, 012, or 015.")]
        public string   ParentPhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string ParentEmail { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string ParentAddress { get; set; }
    }
}
