using StudentTrackingSystem.BLL.Interfaces;
using StudentTrackingSystem.DAL.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using StudentTrackingSystem.BLL.Repositories;
using StudentTrackingSystem.DAL.Models;
using Microsoft.AspNetCore.Identity;
using StudentTrackingSystem.DAL.Models;
using StudentTrackingSystem.Pl.Services;


namespace StudentTrackingSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IStudentRepository, StudentRepository>();

            builder.Services.AddScoped<ITeatcherRepository, TeatcherRepository>();
            builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
            builder.Services.AddScoped<IParentRepository, ParentRepository>();
            builder.Services.AddScoped<IGradeRepository, GradeRepository>();
            builder.Services.AddScoped<IEmailService, EmailService>();

            // Ensure the 'UseSqlServer' method is available by adding the correct using directive
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

                    builder.Services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/SignIn"; // ← هنا بتقوله فين صفحة تسجيل الدخول
                options.AccessDeniedPath = "/Account/AccessDenied"; // ← اختياري، لو عامل صفحة رفض صلاحية
            });


            builder.Services.AddScoped<EmailService>();

            //builder.Services.AddAutoMapper(typeof(StudentMapper));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

                app.MapControllerRoute(
         name: "default",
         pattern: "{controller=Account}/{action=SignIn}/{id?}");
            app.Run();
        }
    }

}
