using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentTrackingSystem.BLL.Interfaces;
using StudentTrackingSystem.DAL.Models;
using StudentTrackingSystem.PL.DTOs;

namespace StudentTrackingSystem.PL.Controllers
{
    public class TeatcherController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TeatcherController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var teatchers = await _unitOfWork.TeatcherRepository.GetAllAsync();
            return View(teatchers);
        }

        public async Task<IActionResult> Create()
        {
            var subjects = await _unitOfWork.SubjectRepository.GetAllAsync();
            ViewBag.Subjects = new SelectList(subjects, "Id", "Name");
            return View(new TeatcherDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TeatcherDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var subjects = _unitOfWork.SubjectRepository.GetAllAsync().Result;
                ViewBag.Subjects = new SelectList(subjects.ToList(), "Id", "Name");
                return View(dto);
            }

            var teatcher = new Teatcher
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                SubjectId = dto.SubjectId,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
            };

            _unitOfWork.TeatcherRepository.AddAsync(teatcher).Wait();
            _unitOfWork.CompleteAsync().Wait();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var teatcher = await _unitOfWork.TeatcherRepository.GetAsync(id);
            if (teatcher == null) return NotFound();

            var dto = new TeatcherDTO
            {
                Id = teatcher.Id,
                FullName = teatcher.FullName,
                Email = teatcher.Email,
                PhoneNumber = teatcher.PhoneNumber,
                SubjectId = teatcher.SubjectId,
                DateOfBirth = teatcher.DateOfBirth,
                Gender = teatcher.Gender,
            };

            var subjects = await _unitOfWork.SubjectRepository.GetAllAsync();
            ViewBag.Subjects = new SelectList(subjects.ToList(), "Id", "Name", dto.SubjectId);
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TeatcherDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var subjects = await _unitOfWork.SubjectRepository.GetAllAsync();
                ViewBag.Subjects = new SelectList(subjects.ToList(), "Id", "Name", dto.SubjectId);
                return View(dto);
            }

            var teatcher = await _unitOfWork.TeatcherRepository.GetAsync(id);
            if (teatcher == null) return NotFound();

            teatcher.FullName = dto.FullName;
            teatcher.Email = dto.Email;
            teatcher.PhoneNumber = dto.PhoneNumber;
            teatcher.SubjectId = dto.SubjectId;
            teatcher.DateOfBirth = dto.DateOfBirth;
            teatcher.Gender = dto.Gender;

            _unitOfWork.TeatcherRepository.Update(teatcher);
            await _unitOfWork.CompleteAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var teatcher = await _unitOfWork.TeatcherRepository.GetAsync(id);
            if (teatcher == null) return NotFound();

            return View(teatcher);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var teatcher = await _unitOfWork.TeatcherRepository.GetAsync(id);
            if (teatcher == null) return NotFound();

            return View(teatcher);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teatcher = await _unitOfWork.TeatcherRepository.GetAsync(id);
            if (teatcher == null) return NotFound();

            _unitOfWork.TeatcherRepository.Delete(teatcher);
            await _unitOfWork.CompleteAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}

