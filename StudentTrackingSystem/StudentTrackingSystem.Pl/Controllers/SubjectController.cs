using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentTrackingSystem.BLL.Interfaces;
using StudentTrackingSystem.DAL.Data.Contexts;
using StudentTrackingSystem.DAL.Models;
using System.Threading.Tasks;

namespace StudentTrackingSystem.Controllers
{
    //public class SubjectController : Controller
    //{
    //    private readonly IUnitOfWork _unitOfWork;

    //    public SubjectController(IUnitOfWork unitOfWork)
    //    {
    //        _unitOfWork = unitOfWork;
    //    }

    //    public async Task<IActionResult> Index()
    //    {
    //        var subjects = await _unitOfWork.SubjectRepository.GetAllAsync();
    //        return View(subjects);
    //    }

    //    public IActionResult Create()
    //    {
    //        return View();
    //    }

    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Create(Subject subject)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            await _unitOfWork.SubjectRepository.AddAsync(subject);
    //            await _unitOfWork.CompleteAsync(); // لازم تحفظ التغييرات
    //            return RedirectToAction(nameof(Index));
    //        }
    //        return View(subject);
    //    }

    //    public async Task<IActionResult> Edit(int id)
    //    {
    //        var subject = await _unitOfWork.SubjectRepository.GetAsync(id);
    //        if (subject == null) return NotFound();
    //        return View(subject);
    //    }

    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Edit(Subject subject)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            _unitOfWork.SubjectRepository.Update(subject);
    //            await _unitOfWork.CompleteAsync(); // برضو لازم تحفظ
    //            return RedirectToAction(nameof(Index));
    //        }
    //        return View(subject);
    //    }

    //    public async Task<IActionResult> Delete(int id)
    //    {
    //        var subject = await _unitOfWork.SubjectRepository.GetAsync(id);
    //        if (subject == null)
    //        {
    //            return NotFound();
    //        }
    //        return View(subject); // هنعمل له صفحة تأكيد أو pop-up
    //    }

    //    [HttpPost, ActionName("Delete")]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> DeleteConfirmed(int id)
    //    {
    //        var subject = await _unitOfWork.SubjectRepository.GetAsync(id);

    //        if (subject == null)
    //        {
    //            return NotFound();
    //        }

    //        _unitOfWork.SubjectRepository.Delete(subject);
    //        await _unitOfWork.CompleteAsync(); // الحفظ
    //        return RedirectToAction(nameof(Index));
    //    }
    //}

    public class SubjectController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _appDb;

        public SubjectController(IUnitOfWork unitOfWork, AppDbContext appDb)
        {
            _unitOfWork = unitOfWork;
            _appDb = appDb;
        }

        public async Task<IActionResult> Index()
        {
            var subjects = await _appDb.Subjects
                .Include(s => s.Grade)
                .ToListAsync();

            return View(subjects);
        }


        public async Task<IActionResult> Create()
        {
            var grades = await _unitOfWork.GradeRepository.GetAllAsync();
            ViewBag.Grades = new SelectList(grades, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Subject subject)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.SubjectRepository.AddAsync(subject);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }

            var grades = await _unitOfWork.GradeRepository.GetAllAsync();
            ViewBag.Grades = new SelectList(grades, "Id", "Name", subject.GradeId);
            return View(subject);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subject = await _unitOfWork.SubjectRepository.GetAsync(id);

            if (subject == null)
            {
                return NotFound();
            }

            _unitOfWork.SubjectRepository.Delete(subject);
            await _unitOfWork.CompleteAsync(); // الحفظ
            return RedirectToAction(nameof(Index));
        }

    }

}
