using HackatonCCR.Data;
using HackatonCCR.EDM.Context;
using HackatonCCR.EDM.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace HackatonCCR.EDM.Repository.Base
{
    public interface IRepositoryBase<TEntity> where TEntity : ModelBase
    {
        /// <summary>
        /// Retorna a instância do DbContext usada pelo RepositoryBase no momento.
        /// </summary>
        IBaseContext GetContext();

        /// <summary>
        /// Adiciona item no banco utilizando o contexto do entity framework. Necessário utilizar o método Commit para confirmar a transação.
        /// </summary>
        /// <param name="entity">Entidade a ser adicionada</param>
        /// <returns></returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// Adiciona item no banco utilizando o contexto do entity framework. Necessário utilizar o método Commit para confirmar a transação.
        /// </summary>
        /// <param name="entity">Entidade a ser adicionada</param>
        /// <returns></returns>
        T Add<T>(T entity) where T : ModelBase;

        /// <summary>
        /// Adiciona itens utilizando o contexto do entity framework. Necessário utilizar o método Commit para confirmar a transação.
        /// </summary>
        /// <param name="entity">Entidade a ser adicionada</param>
        void AddAll(List<TEntity> entity);

        /// <summary>
        /// Adiciona itens utilizando o contexto do entity framework. Necessário utilizar o método Commit para confirmar a transação.
        /// </summary>
        /// <param name="entity">Entidade a ser adicionada</param>
        void AddAll<T>(List<T> entity) where T : ModelBase;

        /// <summary>
        /// Atualiza item utilizando o contexto do entity framework. Necessário utilizar o método Commit para confirmar a transação.
        /// </summary>
        /// <param name="entity">Entidade a ser atualizada</param>
        void Edit(TEntity entity);

        /// <summary>
        /// Atualiza item utilizando o contexto do entity framework. Necessário utilizar o método Commit para confirmar a transação.
        /// </summary>
        /// <param name="entity">Entidade a ser atualizada</param>
        void Edit<T>(T entity) where T : ModelBase;

        /// <summary>
        /// /// Atualiza itens utilizando o contexto do entity framework. Necessário utilizar o método Commit para confirmar a transação.
        /// </summary>
        /// <param name="entity">Entidade a ser atualizada</param>
        void EditAll(List<TEntity> entity);

        /// <summary>
        /// Atualiza itens utilizando o contexto do entity framework. Necessário utilizar o método Commit para confirmar a transação.
        /// </summary>
        /// <param name="entity">Entidade a ser atualizada</param>
        void EditAll<T>(List<T> entity) where T : ModelBase;

        void Delete(TEntity entity);

        void Delete<T>(T entity) where T : ModelBase;

        List<T> Get<T>(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] includes) where T : ModelBase;

        List<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] includes);

        IQueryable<TEntity> GetIQueryable(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] includes);

        IQueryable<T> GetIQueryable<T>(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] includes) where T : ModelBase;

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);

        T FirstOrDefault<T>(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null) where T : ModelBase;

        void RemoveRange(Expression<Func<TEntity, bool>> filter = null);

        void RemoveRange<T>(Expression<Func<T, bool>> filter = null);
    }
}
