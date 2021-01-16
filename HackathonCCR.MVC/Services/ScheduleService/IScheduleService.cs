using HackathonCCR.EDM.Models;
using System;
using System.Collections.Generic;

namespace HackathonCCR.MVC.Services
{
    public interface IScheduleService
    {
        List<Schedule> GetUserSchedules(Guid? userId = null);
        List<Schedule> GetMentorAvailableSchedules(Guid mentorId);
        List<Schedule> GetCategoryAvailableSchedules(Guid categoryId);
        List<Schedule> GetDateAvailableSchedules(DateTime date);
        List<Schedule> GetCurrentSchedules();
        int CreateAgenda(DateTime start, DateTime end, Guid categoryId);
        void Schedule(Guid scheduleId);
        void CancelSchedule(Guid scheduleId);
    }
}
