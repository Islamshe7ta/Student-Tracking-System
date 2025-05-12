using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace StudentTrackingSystem.DAL.Models
{
    public class Subject
    {

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        // Navigation Property to Grade
        public int GradeId { get; set; }
        public Grade? Grade { get; set; } // لازم يكون فيه علاقة بين المادة والصف

        // Navigation Property
        public ICollection<Teatcher>? Teatchers { get; set; }
    }
}
