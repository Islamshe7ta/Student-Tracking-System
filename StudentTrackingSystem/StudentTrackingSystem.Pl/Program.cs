using StudentTrackingSystem.BLL.Interfaces;
using StudentTrackingSystem.DAL.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using StudentTrackingSystem.BLL.Repositories;


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

            // Ensure the 'UseSqlServer' method is available by adding the correct using directive
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }

}
