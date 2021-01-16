using System.Data.Entity;


namespace HackatonCCR.EDM.Programmability.Functions
{
    public class ScalarValuedFunctions
    {
        private readonly DbContext _dbContext;
        public ScalarValuedFunctions(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
