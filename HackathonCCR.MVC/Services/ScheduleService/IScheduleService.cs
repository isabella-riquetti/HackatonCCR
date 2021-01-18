using HackathonCCR.EDM.Models;
using HackathonCCR.MVC.Models.Schedule;
using System;
using System.Collections.Generic;

namespace HackathonCCR.MVC.Services
{
    public interface IScheduleService
    {
        List<UserSchedule> GetUserScheduledMentorship();
        List<Schedule> GetUserSchedules();
        List<Schedule> GetMentorAvailableSchedules(Guid mentorId);
        List<UserSchedule> GetCategoryAvailableSchedules(Guid categoryId);
        SchedulesByDate GetDateAvailableSchedules(DateTime date);
        List<UserSchedule> GetCurrentAvailableSchedules(Guid? categoryId);
        int CreateAgenda(DateTime start, DateTime end, Guid categoryId);
        UserSchedule Get(Guid scheduleId);
        void Schedule(Guid scheduleId);
        void CancelSchedule(Guid scheduleId);
    }
}
