using HackathonCCR.EDM.Models;
using System;

namespace HackathonCCR.EDM.Models
{
    public class Schedule : ModelBase
    {
        public Schedule() : base("Schedule", "ScheduleId")
        {
        }

        public Guid ScheduleId { get; set; }
        public Guid MentorId { get; set; }
        public Guid MentoredId { get; set; }
        public DateTime ScheduleAt { get; set; }
        public Guid CategoryId { get; set; }
        public Enums.Schedule.Status Status { get; set; }
        public Guid AppointmentId { get; set; }

        public virtual User Mentor { get; set; }
        public virtual User Mentored { get; set; }
        public virtual Category Category { get; set; }
    }
}
