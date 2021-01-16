using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackathonCCR.EDM.Models
{
    public class Category : ModelBase
    {
        public Category() : base("Category", "CategoryId")
        {
            Schedules = new List<Schedule>();
        }

        public Guid CategoryId { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
