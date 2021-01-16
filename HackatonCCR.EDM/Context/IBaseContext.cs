using HackatonCCR.EDM.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace HackatonCCR.EDM.Context
{
    public interface IBaseContext : IDisposable
    {
        Database Database { get; }
        DbSet Set(Type entityType);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry Entry(object entity);
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        DbContextConfiguration Configuration { get; }
        DbSet<User> User { get; set; }
        int SaveChanges();
    }
}