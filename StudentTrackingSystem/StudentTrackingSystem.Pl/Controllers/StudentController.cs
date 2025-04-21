using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentTrackingSystem.BLL.Interfaces;
using StudentTrackingSystem.Models;
using StudentTrackingSystem.PL.DTOs;

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
            return View("Index", students); // وضحت المسار بشكل صريح.

        }

        [HttpGet]
  
        public IActionResult Create()
        {
            return View(new StudentDTO());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentDTO studentDTO)
        {
            if (ModelState.IsValid)
            {
                var student = new Student
                {
                    FullName = studentDTO.FullName,
                    DateOfBirth = studentDTO.DateOfBirth,
                    EmailAddress = studentDTO.EmailAddress,
                    PhoneNo = studentDTO.PhoneNo,
                    Address = studentDTO.Address,
                    Grade = studentDTO.Grade
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

            return View(student);
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
                Grade = student.Grade
            };

            return View(studentDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, StudentDTO studentDTO)
        {
            if (id is null || !ModelState.IsValid)
                return View(studentDTO);

            var student = new Student
            {
                Id = id.Value,
                FullName = studentDTO.FullName,
                DateOfBirth = studentDTO.DateOfBirth,
                EmailAddress = studentDTO.EmailAddress,
                PhoneNo = studentDTO.PhoneNo,
                Address = studentDTO.Address,
                Grade = studentDTO.Grade
            };

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

            return View(student);
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
