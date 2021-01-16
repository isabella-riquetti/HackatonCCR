using HackathonCCR.EDM.Models;
using HackathonCCR.EDM.Repository;

namespace HackathonCCR.EDM.UnitOfWork
{
    public interface IUnitOfWork
    {
        Repository.Base.IRepositoryBase<ModelBase> RepositoryBase { get; set; }

        IStoredProcedureRepository StoredProcedure { get; set; }

        void Commit();
    }
}
