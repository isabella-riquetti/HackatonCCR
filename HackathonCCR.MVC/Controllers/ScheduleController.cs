using HackathonCCR.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

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
        public JsonResult GetUserScheduledMentorship()
        {
            var result = _scheduleService.GetUserScheduledMentorship();
            return Json(result, new JsonSerializerSettings());
        }

        [HttpGet]
        public JsonResult GetMentorAvailableSchedules(Guid mentorId)
        {
            var result = _scheduleService.GetUserSchedules();
            return Json(result, new JsonSerializerSettings());
        }

        [HttpGet]
        public JsonResult GetCategoryAvailableSchedules(Guid categoryId)
        {
            var result = _scheduleService.GetUserSchedules();
            return Json(result, new JsonSerializerSettings());
        }

        [HttpGet]
        public PartialViewResult GetDateAvailableSchedules(DateTime date)
        {
            var result = _scheduleService.GetDateAvailableSchedules(date);
            return PartialView("~/Views/Home/_AvailableSchedules.cshtml", result);
        }

        [HttpGet]
        public PartialViewResult GetCurrentAvailableSchedules()
        {
            var result = _scheduleService.GetCurrentAvailableSchedules();
            return PartialView("~/Views/Home/_Online.cshtml", result);
        }

        [HttpGet]
        public JsonResult CreateAgenda(DateTime start, DateTime end, Guid categoryId)
        {
            var result = _scheduleService.CreateAgenda(start, end, categoryId);
            return Json(result, new JsonSerializerSettings());
        }

        [HttpGet]
        public IActionResult Schedule(Guid scheduleId)
        {
            _scheduleService.Schedule(scheduleId);
            return Ok();
        }

        [HttpGet]
        public IActionResult CancelSchedule(Guid scheduleId)
        {
            _scheduleService.CancelSchedule(scheduleId);
            return Ok();
        }
    }
}
