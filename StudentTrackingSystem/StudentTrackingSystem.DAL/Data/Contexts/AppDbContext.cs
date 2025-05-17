using StudentTrackingSystem.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentTrackingSystem.DAL.Data.Models;
using StudentTrackingSystem.Models;
using Microsoft.AspNetCore.Identity; // تأكد إن ده موجود علشان يلاقي Student

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
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<StudentAssignment> StudentAssignments { get; set; }
      

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

            // Configure Assignment relationships
            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.Grade)
                .WithMany()
                .HasForeignKey(a => a.GradeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.Subject)
                .WithMany()
                .HasForeignKey(a => a.SubjectId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.Teacher)
                .WithMany()
                .HasForeignKey(a => a.TeacherId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Teatcher>()
       .HasOne(t => t.Subject)
       .WithMany(s => s.Teatchers)
       .HasForeignKey(t => t.SubjectId)
       .OnDelete(DeleteBehavior.Restrict);
        }

        public void DataSeed()
        {
            SeedUsers();
            SeedRoles();
            SaveChanges();
        }

        private void SeedRoles()
        {
            var adminRole = Roles.Find(Consts.Constants.AdminRoleId);
            var TeatcherRoleId = Roles.Find(Consts.Constants.TeatcherRoleId);
            var StudentRoleId = Roles.Find(Consts.Constants.StudentRoleId);
            if (adminRole == null)
            {
                Roles.Add(new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString("D"),
                    Id = Consts.Constants.AdminRoleId,
                });
            }

            if (TeatcherRoleId == null)
            {
                Roles.Add(new IdentityRole
                {
                    Name = "Teachr",
                    NormalizedName = "Teachr".ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString("D"),
                    Id = Consts.Constants.TeatcherRoleId,
                });
            }
            
            if (StudentRoleId == null)
            {
                Roles.Add(new IdentityRole
                {
                    Name = "Studnt",
                    NormalizedName = "Studnt".ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString("D"),
                    Id = Consts.Constants.StudentRoleId,
                });
            }
        }


        private void SeedUsers()
        {
            var userId = Guid.NewGuid().ToString("D"); 
            var user = Users.FirstOrDefault(u => u.Email == "admin@admin.eg");

            if (user != null)
                return;

            user = new AppUser
            {
                Id = userId,
                Email = "admin@admin.eg",
                UserName = "admin@admin.eg",
                NormalizedUserName = "admin@admin.eg".ToUpper(),
                NormalizedEmail = "admin@admin.eg".ToUpper(),
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                PhoneNumber = "01000000000",
                TwoFactorEnabled = false,
                PasswordHash = null,
                FirstName= "Ali",
                LastName= "Mohamed",
                SecurityStamp = Guid.NewGuid().ToString("D"),
                ConcurrencyStamp = Guid.NewGuid().ToString("D"),
                IsAgree = true,
            };

            var hasher = new PasswordHasher<IdentityUser>();
            user.PasswordHash = hasher.HashPassword(user, "P@ssw0rd");

            Users.Add(user);


            this.UserRoles.Add(new IdentityUserRole<string>
            {
                RoleId = Consts.Constants.AdminRoleId,
                UserId = user.Id
            });
        }
    }
}


