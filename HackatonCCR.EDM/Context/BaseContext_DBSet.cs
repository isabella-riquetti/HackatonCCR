using HackatonCCR.EDM.Models;
using System.Data.Entity;

namespace HackatonCCR.EDM.Context
{
    public partial class BaseContext
    {
        public DbSet<User> User { get; set; }
    }
}
