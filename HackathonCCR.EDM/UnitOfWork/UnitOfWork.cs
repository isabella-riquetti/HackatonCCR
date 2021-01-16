using HackathonCCR.EDM.Context;
using HackathonCCR.EDM.Models;
using HackathonCCR.EDM.Repository;
using System;

namespace HackathonCCR.EDM.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public readonly IHackathonCCRContext _context;

        public Repository.Base.IRepositoryBase<ModelBase> RepositoryBase { get; set; }
        public IStoredProcedureRepository StoredProcedure { get; set; }

        public UnitOfWork()
        {
            _context = new HackathonCCRContext();
            RepositoryBase = new Repository.Base.RepositoryBase<ModelBase>(_context);
            StoredProcedure = new StoredProcedureRepository(_context);
        }

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
