using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Create()
        {
            return View(new TeatcherDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeatcherDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var teatcher = new Teatcher
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Subject = dto.Subject,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                // لا داعي لإعطاء قيمة لـ Id هنا
            };


            await _unitOfWork.TeatcherRepository.AddAsync(teatcher);
            await _unitOfWork.CompleteAsync();

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
                Subject = teatcher.Subject,
                DateOfBirth= teatcher.DateOfBirth,
                Gender = teatcher.Gender,


            };

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TeatcherDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var teatcher = new Teatcher
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Subject = dto.Subject,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                // لا داعي لإعطاء قيمة لـ Id هنا
            };


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

