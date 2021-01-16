using HackathonCCR.EDM.Models;
using System.Data.Entity;

namespace HackathonCCR.EDM.Context
{
    public partial class BaseContext
    {
        public DbSet<User> User { get; set; }
    }
}
