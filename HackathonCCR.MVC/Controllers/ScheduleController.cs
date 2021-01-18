using HackathonCCR.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;

namespace HackathonCCR.MVC.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly IScheduleService _scheduleService;
        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Get(Guid scheduleId)
        {
            var result = _scheduleService.Get(scheduleId);
            return Json(result, new JsonSerializerOptions());
        }

        [HttpGet]
        public JsonResult GetUserScheduledMentorship()
        {
            var result = _scheduleService.GetUserScheduledMentorship();
            return Json(result, new JsonSerializerOptions());
        }

        [HttpGet]
        public JsonResult GetMentorAvailableSchedules(Guid mentorId)
        {
            var result = _scheduleService.GetUserSchedules();
            return Json(result, new JsonSerializerOptions());
        }

        [HttpGet]
        public PartialViewResult GetCategoryAvailableSchedules(Guid categoryId)
        {
            var result = _scheduleService.GetCategoryAvailableSchedules(categoryId);
            return PartialView("~/Views/Home/_Schedules.cshtml", result);
        }

        [HttpGet]
        public PartialViewResult GetDateAvailableSchedules(DateTime date)
        {
            var result = _scheduleService.GetDateAvailableSchedules(date);
            return PartialView("~/Views/Home/_AvailableSchedules.cshtml", result);
        }

        [HttpGet]
        public PartialViewResult GetCurrentAvailableSchedules(Guid? categoryId = null)
        {
            var result = _scheduleService.GetCurrentAvailableSchedules(categoryId);
            return PartialView("~/Views/Home/_Online.cshtml", result);
        }

        [HttpGet]
        public JsonResult CreateAgenda(DateTime start, DateTime end, Guid categoryId)
        {
            var result = _scheduleService.CreateAgenda(start, end, categoryId);
            return Json(result, new JsonSerializerOptions());
        }

        [HttpPost]
        public IActionResult Schedule(Guid scheduleId)
        {
            _scheduleService.Schedule(scheduleId);
            return RedirectToAction("DashStudent", "Home");
        }

        [HttpGet]
        public IActionResult CancelSchedule(Guid scheduleId)
        {
            _scheduleService.CancelSchedule(scheduleId);
            return Ok();
        }
    }
}
