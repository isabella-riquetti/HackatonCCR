using System.Data.Entity;

namespace HackatonCCR.EDM.Programmability.Stored_Procedures
{
    public class StoredProcedures
    {
        private readonly DbContext _dbContext;
        public StoredProcedures(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
