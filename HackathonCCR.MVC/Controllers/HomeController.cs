using HackathonCCR.EDM.Models;
using HackathonCCR.EDM.UnitOfWork;
using HackathonCCR.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HackathonCCR.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            if (HttpContext.User != null && HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
            {
                return Dash();
            }
            var aux = _unitOfWork.RepositoryBase.Get<User>();
            return View();
        }

        public IActionResult Dash()
        {
            if (User.IsInRole("0"))
                return DashStudent();
            else if (User.IsInRole("1"))
                return DashMentor();
            return RedirectToAction("LogOff", "Authentication");
        }

        public IActionResult DashMentor()
        {
            return View();
        }

        public IActionResult DashStudent()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
