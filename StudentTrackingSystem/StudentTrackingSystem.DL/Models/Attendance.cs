using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StudentTrackingSystem.DAL.Models
{
    class Attendance
    {
        
        public int AttendanceId { get; set; }
        
        [Required]
        public DateTime Date { get; set; }
    }
}
