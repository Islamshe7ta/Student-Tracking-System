using Microsoft.AspNetCore.Mvc;
using StudentTrackingSystem.BLL.Interfaces;
using StudentTrackingSystem.DAL.Models;

namespace StudentTrackingSystem.PL.Controllers
{
    public class SubjectController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubjectController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var subjects = await _unitOfWork.SubjectRepository.GetAllAsync();
            return View(subjects);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Subject subject)
        {
            if (!ModelState.IsValid)
                return View(subject);

            await _unitOfWork.SubjectRepository.AddAsync(subject);
            await _unitOfWork.CompleteAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            var subject = await _unitOfWork.SubjectRepository.GetAsync(id);
            if (subject == null) return NotFound();

            return View(subject);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var subject = await _unitOfWork.SubjectRepository.GetAsync(id);
            if (subject == null) return NotFound();

            return View(subject); // لو عامل ViewModel ابعت DTO هنا
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Subject subject)
        {
            if (!ModelState.IsValid)
                return View(subject);

            _unitOfWork.SubjectRepository.Update(subject);
            await _unitOfWork.CompleteAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var subject = await _unitOfWork.SubjectRepository.GetAsync(id);
            if (subject == null) return NotFound();

            return View(subject);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subject = await _unitOfWork.SubjectRepository.GetAsync(id);
            if (subject == null) return NotFound();

            _unitOfWork.SubjectRepository.Delete(subject);
            await _unitOfWork.CompleteAsync();

            return RedirectToAction(nameof(Index));
        }


    }
}
