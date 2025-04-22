using Microsoft.EntityFrameworkCore;
using StudentTrackingSystem.DAL.Models;
using StudentTrackingSystem.Models; // تأكد إن ده موجود علشان يلاقي Student

namespace StudentTrackingSystem.DAL.Data.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }  // ✅ ده اللي ناقص
        public DbSet<Teatcher> Teatchers { get; set; }  // ✅ ده اللي ناقص

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
