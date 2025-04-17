using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace StudentTrackingSystem.DAL.Models
{
    class Notfication
    {
        
        public int NotificationId { get; set; }

        [Required]
        [MinLength(10)]
        public string Message { get; set; }

        [Required]
        public DateTime DateSent { get; set; }
    }
}
