using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using StudentTrackingSystem.BLL.Interfaces;
using StudentTrackingSystem.Models;
using StudentTrackingSystem.PL.DTOs;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using StudentTrackingSystem.DAL.Models;

namespace StudentTrackingSystem.PL.Controllers
{
    //[Authorize]
    public class StudentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var students = await _unitOfWork.StudentRepository.GetAllAsync();

            var studentDTOs = students.Select(student => new StudentDTO
            {
                StudentId = student.Id,
                FullName = student.FullName,
                DateOfBirth = student.DateOfBirth,
                EmailAddress = student.EmailAddress,
                PhoneNo = student.PhoneNo,
                Address = student.Address,
                Grade = student.Grade,
                Gender = student.Gender,
                ImagePath = student.ImagePath,
                ParentName = student.Parent?.FullName,
                ParentPhone = student.Parent?.PhoneNo,
                ParentEmail = student.Parent?.EmailAddress
            }).ToList();

            return View("Index", studentDTOs);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new StudentDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentDTO studentDTO, IFormFile studentImage)
        {
            if (ModelState.IsValid)
            {
                // Handle student image
                string imagePath = null;
                if (studentImage != null && studentImage.Length > 0)
                {
                    var fileName = Path.GetFileName(studentImage.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await studentImage.CopyToAsync(stream);
                    }

                    imagePath = "/images/" + fileName;  // Store the relative path
                }

                // Create a new parent entry (if needed)
                var parent = new Parent
                {
                    FullName = studentDTO.ParentName,
                    PhoneNo = studentDTO.ParentPhone,
                    EmailAddress = studentDTO.ParentEmail
                };

                await _unitOfWork.ParentRepository.AddAsync(parent);
                await _unitOfWork.CompleteAsync();  // Save parent first to get ParentId

                // Create the student with the parent info
                var student = new Student
                {
                    FullName = studentDTO.FullName,
                    DateOfBirth = studentDTO.DateOfBirth,
                    EmailAddress = studentDTO.EmailAddress,
                    PhoneNo = studentDTO.PhoneNo,
                    Address = studentDTO.Address,
                    Grade = studentDTO.Grade,
                    Gender = studentDTO.Gender,
                    Password = studentDTO.Password,  // Save the password
                    ParentId = parent.Id,  // Set the ParentId for the student
                    ImagePath = imagePath   // Set the student's image path
                };

                await _unitOfWork.StudentRepository.AddAsync(student);
                int result = await _unitOfWork.CompleteAsync();

                if (result > 0)
                {
                    TempData["Message"] = "Student Added Successfully";
                    return RedirectToAction("Index");
                }
            }

            return View(studentDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return BadRequest();

            var student = await _unitOfWork.StudentRepository.GetAsync(id.Value);
            if (student is null) return NotFound();

            // تحويل Student إلى StudentDTO
            var studentDTO = new StudentDTO
            {
                StudentId = student.Id,
                FullName = student.FullName,
                Address = student.Address,
                EmailAddress = student.EmailAddress,
                PhoneNo = student.PhoneNo,
                DateOfBirth = student.DateOfBirth,
                Grade = student.Grade,
                Gender = student.Gender,
                Password = student.Password,
                ImagePath = student.ImagePath,
                ParentName = student.Parent?.FullName,
                ParentEmail = student.Parent?.EmailAddress,
                ParentPhone = student.Parent?.PhoneNo
            };

            return View(studentDTO);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var student = await _unitOfWork.StudentRepository.GetAsync(id.Value);
            if (student is null) return NotFound();

            var studentDTO = new StudentDTO
            {
                FullName = student.FullName,
                DateOfBirth = student.DateOfBirth,
                EmailAddress = student.EmailAddress,
                PhoneNo = student.PhoneNo,
                Address = student.Address,
                Grade = student.Grade,
                Gender = student.Gender,
                ImagePath = student.ImagePath, // Add ImagePath for editing
                ParentName = student.Parent?.FullName, // Parent info
                ParentPhone = student.Parent?.PhoneNo,
                ParentEmail = student.Parent?.EmailAddress
            };

            return View(studentDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, StudentDTO studentDTO, IFormFile studentImage)
        {
            if (id is null || !ModelState.IsValid)
                return View(studentDTO);

            string imagePath = studentDTO.ImagePath; // Keep the original image path
            if (studentImage != null && studentImage.Length > 0)
            {
                var fileName = Path.GetFileName(studentImage.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await studentImage.CopyToAsync(stream);
                }

                imagePath = "/images/" + fileName;  // Store the new relative path
            }

            var student = await _unitOfWork.StudentRepository.GetAsync(id.Value);
            if (student == null)
            {
                return NotFound();
            }

            student.FullName = studentDTO.FullName;
            student.DateOfBirth = studentDTO.DateOfBirth;
            student.EmailAddress = studentDTO.EmailAddress;
            student.PhoneNo = studentDTO.PhoneNo;
            student.Address = studentDTO.Address;
            student.Grade = studentDTO.Grade;
            student.Gender = studentDTO.Gender;
            student.Password = studentDTO.Password;  // Update password
            student.ImagePath = imagePath;

            // If Parent data is changed, add or update the Parent entity
            if (!string.IsNullOrEmpty(studentDTO.ParentName) &&
                !string.IsNullOrEmpty(studentDTO.ParentPhone) &&
                !string.IsNullOrEmpty(studentDTO.ParentEmail))
            {
                var parent = new Parent
                {
                    FullName = studentDTO.ParentName,
                    PhoneNo = studentDTO.ParentPhone,
                    EmailAddress = studentDTO.ParentEmail
                };

                // Add the parent to the ParentRepository
                await _unitOfWork.ParentRepository.AddAsync(parent);
                await _unitOfWork.CompleteAsync();  // Ensure the parent is saved to get ParentId

                student.ParentId = parent.Id;  // Now you can assign the new ParentId
            }

            _unitOfWork.StudentRepository.Update(student);
            int result = await _unitOfWork.CompleteAsync();

            if (result > 0)
            {
                TempData["Message"] = "Student Updated Successfully";
                return RedirectToAction("Index");
            }

            return View(studentDTO);
        }
        
        
        
        
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var student = await _unitOfWork.StudentRepository.GetAsync(id.Value);
            if (student is null) return NotFound();

            // تحويل الـ Student إلى StudentDTO
            var studentDTO = new StudentDTO
            {
                StudentId = student.Id,
                FullName = student.FullName,
                Address = student.Address,
                EmailAddress = student.EmailAddress,
                PhoneNo = student.PhoneNo,
                DateOfBirth = student.DateOfBirth,
                Grade = student.Grade,
                Gender = student.Gender,
                Password = student.Password,
                ImagePath = student.ImagePath,
                ParentName = student.Parent?.FullName, // لو في Parent
                ParentEmail = student.Parent?.EmailAddress, // لو في Parent
                ParentPhone = student.Parent?.PhoneNo // لو في Parent
            };

            return View(studentDTO);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _unitOfWork.StudentRepository.GetAsync(id);
            if (student is null) return NotFound();

            // حذف الطالب من الـ Repository
            _unitOfWork.StudentRepository.Delete(student);
            await _unitOfWork.CompleteAsync();

            return RedirectToAction(nameof(Index)); // بعد الحذف تروح على الصفحة الرئيسية أو أي صفحة تانية.
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id is null) return BadRequest();

            var student = await _unitOfWork.StudentRepository.GetAsync(id.Value);
            if (student is null) return NotFound();

            _unitOfWork.StudentRepository.Delete(student);
            int result = await _unitOfWork.CompleteAsync();

            if (result > 0)
            {
                TempData["Message"] = "Student Deleted Successfully";
                return RedirectToAction("Index");
            }

            return View("Delete", student);
        }
    }
}
