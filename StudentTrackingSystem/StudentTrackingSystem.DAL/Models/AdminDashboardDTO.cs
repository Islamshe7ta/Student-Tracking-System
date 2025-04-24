using System.ComponentModel.DataAnnotations;
using StudentTrackingSystem.Models;


namespace StudentTrackingSystem.DAL.Models
{
    public class AdminDashboardDTO
    {
        public int StudentCount { get; set; }
        public int TeacherCount { get; set; }
        public List<Student> RecentStudents { get; set; }
        public List<Teatcher> RecentTeachers { get; set; }
    }
}