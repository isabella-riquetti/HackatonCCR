using HackatonCCR.EDM.Context;
using HackatonCCR.EDM.Models;
using HackatonCCR.EDM.Repository;
using System;

namespace HackatonCCR.EDM.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public readonly IBaseContext _context;

        public Repository.Base.IRepositoryBase<ModelBase> RepositoryBase { get; set; }
        public IStoredProcedureRepository StoredProcedure { get; set; }

        public UnitOfWork()
        {
            _context = new BaseContext();
            RepositoryBase = new Repository.Base.RepositoryBase<ModelBase>(_context);
            StoredProcedure = new StoredProcedureRepository(_context);
        }

        // TODO: Injeção de dependência
        //public UnitOfWork(IRepositoryBase<ModelBase> repositoryBase,
        //    IStoredProcedureRepository storedProcedure)
        //{
        //    RepositoryBase = repositoryBase;
        //    StoredProcedure = storedProcedure;

        //    _context = RepositoryBase.GetContext();
        //}

        private bool _disposed;

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Clear(true);
            GC.SuppressFinalize(this);
        }

        private void Clear(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        ~UnitOfWork()
        {
            Clear(false);
        }
    }
}
