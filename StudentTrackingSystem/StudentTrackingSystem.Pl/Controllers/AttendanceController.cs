//using Microsoft.AspNetCore.Mvc;
//using StudentTrackingSystem.BLL.Interfaces;
//using StudentTrackingSystem.DAL.Models;
//using StudentTrackingSystem.Pl.DTOs;
//using System;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc.Rendering;

//namespace StudentTrackingSystem.Pl.Controllers
//{
//    public class AttendanceController : Controller
//    {
//        private readonly IStudentRepository _studentRepo;
//        private readonly IGenaricRepository<Attendance> _attendanceRepo;
//        private readonly IGradeRepository _gradeRepo;
//        private readonly IUnitOfWork _unitOfWork;

//        public AttendanceController(
//            IStudentRepository studentRepo, 
//            IGenaricRepository<Attendance> attendanceRepo, 
//            IGradeRepository gradeRepo,
//            IUnitOfWork unitOfWork)
//        {
//            _studentRepo = studentRepo;
//            _attendanceRepo = attendanceRepo;
//            _gradeRepo = gradeRepo;
//            _unitOfWork = unitOfWork;
//        }

//        //public async Task<IActionResult> AddAttendance(string grade = null)
//        //{
//        //    var students = await _studentRepo.GetAllAsync();
//        //    if (!string.IsNullOrEmpty(grade))
//        //    {
//        //        students = students.Where(s => s.Grade == grade).ToList();
//        //    }

//        //    var studentDTOs = students.Select(s => new AttendanceDTO
//        //    {
//        //        StudentId = s.Id,
//        //        StudentName = s.FullName,
//        //        Grade = s.Grade,
//        //        Date = DateTime.Now
//        //    }).ToList();

//        //    // Get available grades from database
//        //    var grades = await _gradeRepo.GetAllAsync();
//        //    ViewBag.Grades = grades.ToList();

//        //    return View(studentDTOs);
//        //}
//        public async Task<IActionResult> AddAttendance(string grade = null)
//        {
//            var allStudents = await _studentRepo.GetAllAsync();

//            if (!string.IsNullOrEmpty(grade))
//            {
//                allStudents = allStudents.Where(s => s.Grade == grade).ToList();
//            }

//            // جلب حضور اليوم
//            var today = DateTime.Today;
//            var todaysAttendances = await _attendanceRepo.GetAllAsync();
//            var attendancesToday = todaysAttendances
//                                    .Where(a => a.Date.Date == today)
//                                    .Select(a => a.StudentId)
//                                    .ToList();

//            // استبعاد الطلاب اللي أخدوا حضور اليوم
//            var studentsToMark = allStudents
//                                    .Where(s => !attendancesToday.Contains(s.Id))
//                                    .ToList();

//            var studentDTOs = studentsToMark.Select(s => new AttendanceDTO
//            {
//                StudentId = s.Id,
//                StudentName = s.FullName,
//                Grade = s.Grade,
//                Date = DateTime.Now
//            }).ToList();

//            var grades = await _gradeRepo.GetAllAsync();
//            ViewBag.Grades = grades.ToList();

//            return View(studentDTOs);
//        }

//        [HttpPost]
//        public async Task<IActionResult> MarkAttendance(int studentId, bool isPresent)
//        {
//            var attendance = new Attendance
//            {
//                StudentId = studentId,
//                Date = DateTime.Now,
//                IsPresent = isPresent
//            };

//            await _attendanceRepo.AddAsync(attendance);
//            await _unitOfWork.CompleteAsync();
//            return Json(new { success = true, attendanceId = attendance.Id });
//        }

//        public async Task<IActionResult> ShowAttendance(string grade = null)
//        {
//            var attendances = await _attendanceRepo.GetAllAsync();
//            var students = await _studentRepo.GetAllAsync();

//            var attendanceDTOs = (from a in attendances
//                                join s in students on a.StudentId equals s.Id
//                                select new AttendanceDTO
//                                {
//                                    Id = a.Id,
//                                    StudentId = s.Id,
//                                    StudentName = s.FullName,
//                                    Grade = s.Grade,
//                                    Date = a.Date,
//                                    IsPresent = a.IsPresent
//                                }).ToList();

//            if (!string.IsNullOrEmpty(grade))
//            {
//                attendanceDTOs = attendanceDTOs.Where(a => a.Grade == grade).ToList();
//            }

//            // Get available grades for filter
//            var grades = await _gradeRepo.GetAllAsync();
//            ViewBag.Grades = grades.ToList();

//            return View(attendanceDTOs);
//        }
//    }
//} 

using Microsoft.AspNetCore.Mvc;
using StudentTrackingSystem.BLL.Interfaces;
using StudentTrackingSystem.DAL.Models;
using StudentTrackingSystem.Pl.DTOs;
using System;
using System.Linq;
using System.Threading.Tasks;

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

        // الصفحة لاظهار الطلاب اللي هيتم تسجيل حضورهم (فقط الطلاب اللي لم يؤخذ لهم حضور اليوم)
        public async Task<IActionResult> AddAttendance(string grade = null)
        {
            var today = DateTime.Today;

            var allStudents = await _studentRepo.GetAllAsync();

            // نجيب حضور اليوم لكل الطلاب
            var todaysAttendance = await _attendanceRepo.GetAllAsync();
            var todaysAttendanceForStudents = todaysAttendance
                .Where(a => a.Date.Date == today)
                .Select(a => a.StudentId)
                .ToList();

            // نجيب الطلاب اللي ماخدوش حضور النهاردة (يعني اللي مش موجودين في حضور اليوم)
            var studentsToTakeAttendance = allStudents
                .Where(s => !todaysAttendanceForStudents.Contains(s.Id))
                .ToList();

            if (!string.IsNullOrEmpty(grade))
            {
                studentsToTakeAttendance = studentsToTakeAttendance
                    .Where(s => s.Grade == grade)
                    .ToList();
            }

            var dtoList = studentsToTakeAttendance.Select(s => new AttendanceDTO
            {
                StudentId = s.Id,
                StudentName = s.FullName,
                Grade = s.Grade,
                Date = today
            }).ToList();

            ViewBag.Grades = (await _gradeRepo.GetAllAsync()).ToList();

            return View(dtoList);
        }

        // اضافة حضور جديد
        [HttpPost]
        public async Task<IActionResult> MarkAttendance(int studentId, bool isPresent)
        {
            var attendance = new Attendance
            {
                StudentId = studentId,
                Date = DateTime.Today,
                IsPresent = isPresent
            };

            await _attendanceRepo.AddAsync(attendance);
            await _unitOfWork.CompleteAsync();

            return Json(new { success = true, attendanceId = studentId }); // برجّع StudentId للتمييز
        }

        // عرض كل الحضور مع فلترة الدرجة
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

            ViewBag.Grades = (await _gradeRepo.GetAllAsync()).ToList();

            return View(attendanceDTOs);
        }
    }
}
