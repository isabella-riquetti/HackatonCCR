using HackathonCCR.EDM.Models;

namespace HackathonCCR.EDM.Repository.Base
{
    public interface IBulkWorker<TEntity> where TEntity : ModelBase
    {
    }
}