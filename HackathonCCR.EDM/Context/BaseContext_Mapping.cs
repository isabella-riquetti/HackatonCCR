using HackathonCCR.EDM.Models.Mapping;
using System.Data.Entity;

namespace HackathonCCR.EDM.Context
{
    public partial class BaseContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<BaseContext>(null);
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}
