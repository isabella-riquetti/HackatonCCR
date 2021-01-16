using HackathonCCR.EDM.Models;
using System.Data.Entity;

namespace HackathonCCR.EDM.Context
{
    public partial class HackathonCCRContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
    }
}
