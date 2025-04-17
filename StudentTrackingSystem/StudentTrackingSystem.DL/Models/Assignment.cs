using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StudentTrackingSystem.DAL.Models
{
    class Assignment
    {
        
        public int AssignmentId { get; set; }
        public string AssignmentName { get; set; }
        public DateTime DueDate { get; set; }
    }
}
