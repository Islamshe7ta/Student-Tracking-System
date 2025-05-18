using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using StudentTrackingSystem.BLL.Interfaces;
using StudentTrackingSystem.DAL.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace StudentTrackingSystem.Pl.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStudentRepository _studentRepo;
        private readonly ITeatcherRepository _teacherRepo;
        private readonly IGenaricRepository<Attendance> _attendanceRepo;
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(
            UserManager<AppUser> userManager,
            IStudentRepository studentRepo,
            ITeatcherRepository teacherRepo,
            IGenaricRepository<Attendance> attendanceRepo,
            ILogger<ProfileController> logger)
        {
            _userManager = userManager;
            _studentRepo = studentRepo;
            _teacherRepo = teacherRepo;
            _attendanceRepo = attendanceRepo;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                {
                    TempData["Error"] = "لم يتم العثور على بيانات المستخدم";
                    return RedirectToAction("SignIn", "Account");
                }

                // Get user roles
                var roles = await _userManager.GetRolesAsync(currentUser);
                ViewBag.UserRoles = string.Join(", ", roles);

                if (roles.Contains("Student"))
                {
                    var student = (await _studentRepo.GetAllAsync())
                        .FirstOrDefault(s => s.EmailAddress == currentUser.Email);
                    
                    if (student != null)
                    {
                        // Get attendance statistics
                        var attendances = await _attendanceRepo.GetAllAsync();
                        var studentAttendances = attendances.Where(a => a.StudentId == student.Id).ToList();
                        
                        ViewBag.TotalDays = studentAttendances.Count;
                        ViewBag.PresentDays = studentAttendances.Count(a => a.IsPresent);
                        ViewBag.AbsentDays = studentAttendances.Count(a => !a.IsPresent);
                        ViewBag.AttendanceRate = ViewBag.TotalDays > 0 
                            ? Math.Round((double)ViewBag.PresentDays / ViewBag.TotalDays * 100, 1) 
                            : 0;

                        return View("StudentDashboard", student);
                    }
                }
                else if (roles.Contains("Teatcher"))
                {
                    var teacher = (await _teacherRepo.GetAllAsync())
                        .FirstOrDefault(t => t.Email == currentUser.Email);
                    
                    if (teacher != null)
                    {
                        // Get teaching statistics
                        var attendances = await _attendanceRepo.GetAllAsync();
                        var teacherClasses = attendances
                            .GroupBy(a => a.Date)
                            .OrderByDescending(g => g.Key)
                            .Take(10)
                            .Select(g => new
                            {
                                Date = g.Key,
                                Grade = g.First().Student.Grade,
                                PresentCount = g.Count(a => a.IsPresent),
                                AbsentCount = g.Count(a => !a.IsPresent)
                            })
                            .ToList();

                        ViewBag.RecentClasses = teacherClasses;
                        ViewBag.TotalStudents = teacherClasses.Sum(c => c.PresentCount + c.AbsentCount);
                        ViewBag.TotalClasses = teacherClasses.Count;
                        ViewBag.AverageAttendance = teacherClasses.Any() 
                            ? Math.Round(teacherClasses.Average(c => (double)c.PresentCount / (c.PresentCount + c.AbsentCount) * 100), 1)
                            : 0;

                        return View("TeacherDashboard", teacher);
                    }
                }

                // If no specific role or data found, show basic profile
                return View("UserProfile", currentUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "حدث خطأ أثناء عرض الملف الشخصي");
                TempData["Error"] = "حدث خطأ أثناء عرض الملف الشخصي";
                return RedirectToAction("Index", "Home");
            }
        }
    }
} 