using HackathonCCR.EDM.Models;
using System;
using System.Collections.Generic;

namespace HackathonCCR.MVC.Services
{
    public interface IScheduleService
    {
        List<Schedule> GetUserScheduledMentorship();
        List<Schedule> GetUserSchedules();
        List<Schedule> GetMentorAvailableSchedules(Guid mentorId);
        List<Schedule> GetCategoryAvailableSchedules(Guid categoryId);
        List<Schedule> GetDateAvailableSchedules(DateTime date);
        List<Schedule> GetCurrentAvailableSchedules();
        int CreateAgenda(DateTime start, DateTime end, Guid categoryId);
        void Schedule(Guid scheduleId);
        void CancelSchedule(Guid scheduleId);
    }
}
