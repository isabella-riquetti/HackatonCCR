using HackatonCCR.EDM.Models;
using HackatonCCR.EDM.Repository;

namespace HackatonCCR.EDM.UnitOfWork
{
    public interface IUnitOfWork
    {
        Repository.Base.IRepositoryBase<ModelBase> RepositoryBase { get; set; }

        IStoredProcedureRepository StoredProcedure { get; set; }

        void Commit();
    }
}
