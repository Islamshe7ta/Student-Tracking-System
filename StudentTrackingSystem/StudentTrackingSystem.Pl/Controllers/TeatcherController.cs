using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using StudentTrackingSystem.BLL.Interfaces;
using StudentTrackingSystem.DAL.Models;
using StudentTrackingSystem.PL.DTOs;

public class TeatcherController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public TeatcherController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // عرض قائمة المدرسين
    public async Task<IActionResult> Index()
    {
        var teachers = await _unitOfWork.TeatcherRepository.GetAllAsync();
        return View(teachers);
    }

    // عرض صفحة إنشاء مدرس جديد
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await LoadSubjectsIntoViewBag();
        return View(new TeatcherDTO());
    }

    // إضافة مدرس جديد (POST)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TeatcherDTO dto)
    {
        if (!ModelState.IsValid)
        {
            await LoadSubjectsIntoViewBag(dto.SubjectId);
            return View(dto);
        }

        var teacher = new Teatcher
        {
            FullName = dto.FullName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            SubjectId = dto.SubjectId,
            DateOfBirth = dto.DateOfBirth,
            Gender = dto.Gender
        };

        await _unitOfWork.TeatcherRepository.AddAsync(teacher);
        await _unitOfWork.CompleteAsync();

        return RedirectToAction("Index");
    }

    // عرض صفحة تعديل مدرس
    public async Task<IActionResult> Edit(int id)
    {
        var teacher = await _unitOfWork.TeatcherRepository.GetAsync(id);
        if (teacher == null)
        {
            return NotFound();
        }

        var dto = new TeatcherDTO
        {
            Id = teacher.Id,
            FullName = teacher.FullName,
            Email = teacher.Email,
            PhoneNumber = teacher.PhoneNumber,
            SubjectId = teacher.SubjectId,
            DateOfBirth = teacher.DateOfBirth,
            Gender = teacher.Gender
        };

        await LoadSubjectsIntoViewBag(dto.SubjectId);
        return View(dto);
    }

    // تعديل مدرس (POST)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, TeatcherDTO dto)
    {
        if (!ModelState.IsValid)
        {
            await LoadSubjectsIntoViewBag(dto.SubjectId);
            return View(dto);
        }

        var teacher = await _unitOfWork.TeatcherRepository.GetAsync(id);
        if (teacher == null)
        {
            return NotFound();
        }

        teacher.FullName = dto.FullName;
        teacher.Email = dto.Email;
        teacher.PhoneNumber = dto.PhoneNumber;
        teacher.SubjectId = dto.SubjectId;
        teacher.DateOfBirth = dto.DateOfBirth;
        teacher.Gender = dto.Gender;

        _unitOfWork.TeatcherRepository.Update(teacher);
        await _unitOfWork.CompleteAsync();

        return RedirectToAction(nameof(Index));
    }

    // عرض تفاصيل مدرس
    public async Task<IActionResult> Details(int id)
    {
        var teacher = await _unitOfWork.TeatcherRepository.GetAsync(id);
        if (teacher == null)
        {
            return NotFound();
        }

        return View(teacher);
    }

    // عرض صفحة حذف مدرس
    public async Task<IActionResult> Delete(int id)
    {
        var teacher = await _unitOfWork.TeatcherRepository.GetAsync(id);
        if (teacher == null)
        {
            return NotFound();
        }

        return View(teacher);
    }

    // تأكيد حذف المدرس
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var teacher = await _unitOfWork.TeatcherRepository.GetAsync(id);
        if (teacher == null)
        {
            return NotFound();
        }

        _unitOfWork.TeatcherRepository.Delete(teacher);
        await _unitOfWork.CompleteAsync();

        return RedirectToAction(nameof(Index));
    }

    // تحميل المواد في الـ ViewBag
    private async Task LoadSubjectsIntoViewBag(object selectedSubjectId = null)
    {
        var subjects = await _unitOfWork.SubjectRepository.GetAllAsync();
        ViewBag.Subjects = new SelectList(subjects, "Id", "Name", selectedSubjectId);
    }
}
