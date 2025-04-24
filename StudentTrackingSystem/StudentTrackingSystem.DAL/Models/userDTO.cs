using System.ComponentModel.DataAnnotations;


namespace StudentTrackingSystem.DAL.Models
{
    public class userDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Password { get; set; } // كلمة المرور، هنقوم بتشفيرها بعد كده
    }
}
