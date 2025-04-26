using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackingSystem.DAL.Models
{
    public class Subject
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        // Navigation Property
        public ICollection<Teatcher>? Teatchers { get; set; }
    }

}
