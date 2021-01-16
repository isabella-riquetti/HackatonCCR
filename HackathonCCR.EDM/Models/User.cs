using System;
using System.Collections.Generic;

namespace HackathonCCR.EDM.Models
{
    public class User : ModelBase
    {
        public User() : base("User", "UserId")
        {
            Mentorings = new List<Schedule>();
            Schedules = new List<Schedule>();
        }

        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public Guid? GraduationId { get; set; }
        public int? RemainingMissingHours { get; set; }
        public string WorkingField { get; set; }
        public Enums.User.Type Type { get; set; }
        public byte[] Picture { get; set; }

        public virtual Category Graduation { get; set; }

        public virtual ICollection<Schedule> Mentorings { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
