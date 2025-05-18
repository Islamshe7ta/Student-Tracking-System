//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace StudentTrackingSystem.PL.Controllers
//{
//    [Authorize]
//    public class DashboardController : Controller
//    {
//        // Admin Dashboard
//        [Authorize(Roles = "admin")]
//        public IActionResult AdminDashboard()
//        {
//            return View();
//        }

//        // Teacher Dashboard
//        [Authorize(Roles = "Teachr")]
//        public IActionResult TeacherDashboard()
//        {
//            return View();
//        }

//        // Student Dashboard
//        [Authorize(Roles = "Studnt")]
//        public IActionResult StudentDashboard()
//        {
//            return View();
//        }
//    }
//}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StudentTrackingSystem.PL.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        [Authorize(Roles = "Teatcher")]
        public IActionResult TeacherDashboard()
        {
            return View();
        }

        [Authorize(Roles = "Student")]
        public IActionResult StudentDashboard()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminDashboard()
        {
            return View();
        }

        //// Admin Dashboard
        //        [Authorize(Roles = "Admin")]
        //         public IActionResult AdminDashboard()
        // {
        //     return View();
        // }
    }
}