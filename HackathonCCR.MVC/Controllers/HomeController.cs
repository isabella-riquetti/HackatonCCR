using HackathonCCR.EDM.Models;
using HackathonCCR.EDM.UnitOfWork;
using HackathonCCR.MVC.Models;
using HackathonCCR.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HackathonCCR.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IScheduleService _scheduleService;

        public HomeController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        public IActionResult Index()
        {
            if (HttpContext.User != null && HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
                return Dash();

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
            var schedules = _scheduleService.GetUserScheduledMentorship();
            return View(schedules);
        }

        public IActionResult DashStudent()
        {
            var schedules = _scheduleService.GetUserScheduledMentorship();
            return View(schedules);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
