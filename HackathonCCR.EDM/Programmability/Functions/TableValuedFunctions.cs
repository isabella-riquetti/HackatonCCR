using System.Data.Entity;

namespace HackathonCCR.EDM.Programmability.Functions
{
    public class TableValuedFunctions
    {
        public const string ContextName = "HackathonCCRContext";
        private readonly DbContext _dbContext;
        public TableValuedFunctions(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
