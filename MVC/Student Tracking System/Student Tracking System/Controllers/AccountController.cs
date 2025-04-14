using Microsoft.AspNetCore.Mvc;
using Student_Tracking_System.Models;

public class AccountController : Controller
{
    [HttpGet]
    public IActionResult Login()
    {
        return View(); // Looks for Views/Account/Login.cshtml by convention
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // Here you would typically authenticate the user
        // For demo purposes, we'll just redirect based on role
        switch (model.Role.ToLower())
        {
            case "admin":
                return RedirectToAction("Index", "Admin");
            case "teacher":
                return RedirectToAction("Index", "Teacher");
            case "student":
                return RedirectToAction("Index", "Student");
            default:
                ModelState.AddModelError("", "Invalid role selected");
                return View(model);
        }
    }

    [HttpGet]
    public ActionResult ForgotPassword()
    {
        return View();
    }

}