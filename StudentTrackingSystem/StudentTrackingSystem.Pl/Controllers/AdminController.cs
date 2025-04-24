using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentTrackingSystem.BLL.Interfaces;
using StudentTrackingSystem.DAL.Models;
using StudentTrackingSystem.PL.DTOs;

namespace StudentTrackingSystem.PL.Controllers
{
    [Authorize(Roles = "Admin")] // Restrict to admin users only
    public class AdminController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Dashboard view
        public async Task<IActionResult> Dashboard()
        {
            var studentCount = await _unitOfWork.StudentRepository.GetCountAsync();
            var teacherCount = await _unitOfWork.TeatcherRepository.GetCountAsync();

            var model = new AdminDashboardDTO
            {
                StudentCount = studentCount,
                TeacherCount = teacherCount,
                RecentStudents = await _unitOfWork.StudentRepository.GetRecentAsync(5),
                RecentTeachers = await _unitOfWork.TeatcherRepository.GetRecentAsync(5)
            };

            return View(model);
        }

        // User management
        public async Task<IActionResult> ManageUsers()
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();
            return View(users);
        }

        // Create new user (admin)
        public IActionResult CreateUser()
        {
            return View(new AdminUserDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(AdminUserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                var User = new User
                {
                    Username = userDTO.Username,
                    Email = userDTO.Email,
                    Role = userDTO.Role,
                    // Password would be hashed here in a real implementation
                    PasswordHash = HashPassword(userDTO.Password)
                };

                await _unitOfWork.UserRepository.AddAsync(User);
                int result = await _unitOfWork.CompleteAsync();

                if (result > 0)
                {
                    TempData["Message"] = "User created successfully";
                    return RedirectToAction("ManageUsers");
                }
            }

            return View(userDTO);
        }

        // System settings
        public async Task<IActionResult> SystemSettings()
        {
            var settings = await _unitOfWork.SystemSettingRepository.GetAllAsync();
            return View(settings);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateSettings(List<SystemSetting> settings)
        {
            if (ModelState.IsValid)
            {
                foreach (var setting in settings)
                {
                    _unitOfWork.SystemSettingRepository.Update(setting);
                }

                int result = await _unitOfWork.CompleteAsync();

                if (result > 0)
                {
                    TempData["Message"] = "Settings updated successfully";
                }
            }

            return RedirectToAction("SystemSettings");
        }

        // Audit logs
        public async Task<IActionResult> AuditLogs(int page = 1, int pageSize = 20)
        {
            var logs = await _unitOfWork.AuditLogRepository.GetPagedAsync(page, pageSize);
            return View(logs);
        }

        // Backup/Restore functionality
        public IActionResult Backup()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBackup()
        {
            bool success = await _unitOfWork.DatabaseRepository.CreateBackupAsync();

            if (success)
            {
                TempData["Message"] = "Backup created successfully";
            }
            else
            {
                TempData["Error"] = "Failed to create backup";
            }

            return RedirectToAction("Backup");
        }

        // Helper method - would be implemented properly in a real application
        private string HashPassword(string password)
        {
            // In a real application, use proper password hashing like BCrypt
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
}