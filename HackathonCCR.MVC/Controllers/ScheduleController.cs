using HackathonCCR.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackathonCCR.MVC.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly IScheduleService _scheduleService;
        private readonly IEmailService _emailService;
        public ScheduleController(IScheduleService scheduleService, IEmailService emailService)
        {
            _scheduleService = scheduleService;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetUserSchedules()
        {
            var result = _scheduleService.GetUserSchedules();
            return Json(result, new JsonSerializerSettings());
        }

        [HttpGet]
        public JsonResult GetMentorAvailableSchedules(Guid mentorId)
        {

        }

        [HttpGet]
        public JsonResult GetCategoryAvailableSchedules(Guid categoryId)
        {

        }

        [HttpGet]
        public JsonResult GetDateAvailableSchedules(DateTime date)
        {

        }

        [HttpGet]
        public JsonResult GetCurrentSchedules()
        {

        }

        [HttpGet]
        int CreateAgenda(DateTime start, DateTime end, Guid categoryId)
        {

        }

        [HttpGet]
        void Schedule(Guid scheduleId)
        {

        }

        [HttpGet]
        void CancelSchedule(Guid scheduleId)
        {

        }

        [HttpGet]
    }
}
