using StudentTrackingSystem.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentTrackingSystem.DAL.Models;
using StudentTrackingSystem.Models; // تأكد إن ده موجود علشان يلاقي Student

namespace StudentTrackingSystem.DAL.Data.Contexts
{
    //public class AppDbContext : DbContext
    //{
    //    public DbSet<Grade> Grades { get; set; }

    //    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    //    {
    //    }

    //    protected override void OnModelCreating(ModelBuilder modelBuilder)
    //    {
    //        base.OnModelCreating(modelBuilder);
    //        // Additional model configuration if needed  
    //    }
    //}
    public class AppDbContext : /*DbContext*/ IdentityDbContext<AppUser> // لو بتستخدم Identity
    {
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Subject> Subjects { get; set; } // ✅ ضيف ده
        public DbSet<Teatcher> Teatchers { get; set; } // لو بتستخدمه
        public DbSet<Student> Students { get; set; } // لو بتستخدمه
        

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // أي إعدادات إضافي

            modelBuilder.Entity<Subject>()
                .HasOne(s => s.Grade)
                .WithMany()
                .HasForeignKey(s => s.GradeId)
                .OnDelete(DeleteBehavior.Cascade);  // يحدد الحذف التلقائي


        }

    }
}


