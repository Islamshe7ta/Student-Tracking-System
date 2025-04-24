using System.ComponentModel.DataAnnotations;


namespace StudentTrackingSystem.DAL.Models
{
    public class AdminUserDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        // الخصائص الأخرى التي تحتاجها
    }
}
