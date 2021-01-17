using System.Collections.Generic;

namespace HackathonCCR.MVC.Models.Schedule
{
    public class SchedulesByDate
    {
        public string Date { get; set; }
        public List<UserSchedule> Schedules { get; set; }
    }
}
