using HackathonCCR.EDM.Context;

namespace HackathonCCR.EDM.Repository
{
    public class StoredProcedureRepository : IStoredProcedureRepository
    {
        private readonly IBaseContext _context;

        public StoredProcedureRepository(IBaseContext context)
        {
            _context = context;
        }

        public bool AddMoovie()
        {
            return true;
        }

    }
}
