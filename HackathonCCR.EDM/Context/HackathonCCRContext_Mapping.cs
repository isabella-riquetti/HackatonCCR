using HackathonCCR.EDM.Models.Mapping;
using System.Data.Entity;

namespace HackathonCCR.EDM.Context
{
    public partial class HackathonCCRContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<HackathonCCRContext>(null);
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}
