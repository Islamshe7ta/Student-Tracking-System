using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using StudentTrackingSystem.BLL.Interfaces;
using StudentTrackingSystem.DAL.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace StudentTrackingSystem.Pl.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IStudentRepository _studentRepo;
        private readonly ITeatcherRepository _teacherRepo;
        private readonly IGenaricRepository<Attendance> _attendanceRepo;
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(
            IStudentRepository studentRepo,
            ITeatcherRepository teacherRepo,
            IGenaricRepository<Attendance> attendanceRepo,
            ILogger<ProfileController> logger)
        {
            _studentRepo = studentRepo;
            _teacherRepo = teacherRepo;
            _attendanceRepo = attendanceRepo;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var userEmail = User.Identity?.Name;
                if (string.IsNullOrEmpty(userEmail))
                {
                    TempData["Error"] = "لم يتم العثور على البريد الإلكتروني للمستخدم";
                    return RedirectToAction("SignIn", "Account");
                }

                // Log user roles
                var roles = User.Claims.Where(c => c.Type == ClaimTypes.Role)
                                     .Select(c => c.Value);
                ViewBag.UserRoles = string.Join(", ", roles);

                if (User.IsInRole("Student"))
                {
                    var students = await _studentRepo.GetAllAsync();
                    var student = students.FirstOrDefault(s => s.EmailAddress == userEmail);

                    if (student != null)
                    {
                        // Get attendance statistics
                        var attendances = await _attendanceRepo.GetAllAsync();
                        var studentAttendances = attendances.Where(a => a.StudentId == student.Id).ToList();
                        
                        // Get recent attendance (last 10 records)
                        ViewBag.RecentAttendance = studentAttendances
                            .OrderByDescending(a => a.Date)
                            .Take(10)
                            .ToList();
                        
                        ViewBag.TotalDays = studentAttendances.Count;
                        ViewBag.PresentDays = studentAttendances.Count(a => a.IsPresent);
                        ViewBag.AbsentDays = studentAttendances.Count(a => !a.IsPresent);
                        ViewBag.AttendanceRate = ViewBag.TotalDays > 0 
                            ? Math.Round((double)ViewBag.PresentDays / ViewBag.TotalDays * 100, 1) 
                            : 0;

                        return View("StudentProfile", student);
                    }
                    else
                    {
                        TempData["Error"] = $"لم يتم العثور على طالب بالبريد الإلكتروني: {userEmail}";
                        return RedirectToAction("Index", "Home");
                    }
                }
                else if (User.IsInRole("Teacher"))
                {
                    var teachers = await _teacherRepo.GetAllAsync();
                    var teacher = teachers.FirstOrDefault(t => t.Email == userEmail);

                    if (teacher != null)
                    {
                        // Get attendance statistics for teacher's classes
                        var attendances = await _attendanceRepo.GetAllAsync();
                        
                        // Get recent classes with attendance (last 10 days)
                        var recentClasses = attendances
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

                        ViewBag.RecentClasses = recentClasses;
                        ViewBag.TotalStudents = teachers.Count();
                        ViewBag.TotalClasses = recentClasses.Count;
                        ViewBag.AverageAttendance = recentClasses.Any() 
                            ? Math.Round(recentClasses.Average(c => (double)c.PresentCount / (c.PresentCount + c.AbsentCount) * 100), 1)
                            : 0;

                        return View("TeacherProfile", teacher);
                    }
                    else
                    {
                        TempData["Error"] = $"لم يتم العثور على معلم بالبريد الإلكتروني: {userEmail}";
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    TempData["Error"] = "المستخدم ليس طالباً ولا معلماً";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"حدث خطأ: {ex.Message}";
                return RedirectToAction("Index", "Home");
            }
        }
    }
} 