using System;
using System.Collections.Generic;

namespace HackathonCCR.EDM.Models
{
    public class Category : ModelBase
    {
        public Category() : base("Category", "CategoryId")
        {
            Schedules = new List<Schedule>();
            Mentors = new List<User>();
        }

        public Guid CategoryId { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual ICollection<User> Mentors { get; set; }
    }
}
