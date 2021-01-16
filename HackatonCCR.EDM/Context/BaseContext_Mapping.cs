using HackatonCCR.EDM.Models.Mapping;
using System.Data.Entity;

namespace HackatonCCR.EDM.Context
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
