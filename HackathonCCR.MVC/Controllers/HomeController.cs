using HackathonCCR.MVC.Models;
using HackathonCCR.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
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
            var time = DateTime.Now.Date;
            var times = new List<SelectListItem>();
            for (int i = 0; i < 48; i++)
            {
                times.Add(new SelectListItem()
                {
                    Text = time.ToShortTimeString(),
                    Value = time.ToShortTimeString()
                });
                time = time.AddMinutes(30);
            }
            ViewBag.Times = times;
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
