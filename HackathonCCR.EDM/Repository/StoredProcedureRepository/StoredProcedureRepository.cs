using HackathonCCR.EDM.Context;

namespace HackathonCCR.EDM.Repository
{
    public class StoredProcedureRepository : IStoredProcedureRepository
    {
        private readonly IHackathonCCRContext _context;

        public StoredProcedureRepository(IHackathonCCRContext context)
        {
            _context = context;
        }

        public bool AddMoovie()
        {
            return true;
        }

    }
}
