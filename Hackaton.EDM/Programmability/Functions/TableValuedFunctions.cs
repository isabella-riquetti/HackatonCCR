using System.Data.Entity;

namespace HackatonCCR.EDM.Programmability.Functions
{
    public class TableValuedFunctions
    {
        public const string ContextName = "BaseContext";
        private readonly DbContext _dbContext;
        public TableValuedFunctions(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
