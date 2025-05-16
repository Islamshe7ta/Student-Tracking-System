using Microsoft.AspNetCore.Mvc;
using StudentTrackingSystem.BLL.Interfaces;
using StudentTrackingSystem.DAL.Models;
using StudentTrackingSystem.Pl.DTOs;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StudentTrackingSystem.Pl.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly IStudentRepository _studentRepo;
        private readonly IGenaricRepository<Attendance> _attendanceRepo;
        private readonly IGradeRepository _gradeRepo;
        private readonly IUnitOfWork _unitOfWork;

        public AttendanceController(
            IStudentRepository studentRepo, 
            IGenaricRepository<Attendance> attendanceRepo, 
            IGradeRepository gradeRepo,
            IUnitOfWork unitOfWork)
        {
            _studentRepo = studentRepo;
            _attendanceRepo = attendanceRepo;
            _gradeRepo = gradeRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> AddAttendance(string grade = null)
        {
            var students = await _studentRepo.GetAllAsync();
            if (!string.IsNullOrEmpty(grade))
            {
                students = students.Where(s => s.Grade == grade).ToList();
            }
            
            var studentDTOs = students.Select(s => new AttendanceDTO
            {
                StudentId = s.Id,
                StudentName = s.FullName,
                Grade = s.Grade,
                Date = DateTime.Now
            }).ToList();

            // Get available grades from database
            var grades = await _gradeRepo.GetAllAsync();
            ViewBag.Grades = grades.ToList();

            return View(studentDTOs);
        }

        [HttpPost]
        public async Task<IActionResult> MarkAttendance(int studentId, bool isPresent)
        {
            var attendance = new Attendance
            {
                StudentId = studentId,
                Date = DateTime.Now,
                IsPresent = isPresent
            };

            await _attendanceRepo.AddAsync(attendance);
            await _unitOfWork.CompleteAsync();
            return Json(new { success = true, attendanceId = attendance.Id });
        }

        public async Task<IActionResult> ShowAttendance(string grade = null)
        {
            var attendances = await _attendanceRepo.GetAllAsync();
            var students = await _studentRepo.GetAllAsync();

            var attendanceDTOs = (from a in attendances
                                join s in students on a.StudentId equals s.Id
                                select new AttendanceDTO
                                {
                                    Id = a.Id,
                                    StudentId = s.Id,
                                    StudentName = s.FullName,
                                    Grade = s.Grade,
                                    Date = a.Date,
                                    IsPresent = a.IsPresent
                                }).ToList();

            if (!string.IsNullOrEmpty(grade))
            {
                attendanceDTOs = attendanceDTOs.Where(a => a.Grade == grade).ToList();
            }

            // Get available grades for filter
            var grades = await _gradeRepo.GetAllAsync();
            ViewBag.Grades = grades.ToList();

            return View(attendanceDTOs);
        }
    }
} 