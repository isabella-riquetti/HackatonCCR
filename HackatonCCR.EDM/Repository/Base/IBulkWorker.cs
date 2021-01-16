using HackatonCCR.EDM.Models;

namespace HackatonCCR.EDM.Repository.Base
{
    public interface IBulkWorker<TEntity> where TEntity : ModelBase
    {
    }
}