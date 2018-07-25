using Framework.Core;
using Framework.Model;
using Framework.Repository.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Framework.Repository
{
    /// <summary>
    /// Represents a generic entity repository.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public class FWRepository<TEntity> : IFWRepository<TEntity>, IDisposable
        where TEntity : class, IFWEntity<long>
    {
        /// <summary>
        /// Gets or sets if the repository will use the ef change tracker.
        /// </summary>
        public bool ChangeTrackerEnabled { get; set; } = true;

        /// <summary>
        /// Gets the repository context.
        /// </summary>
        protected DbContext Context { get; private set; }

        /// <summary>
        /// Initializes a new instance of the Framework.Repository.FWGenericRepository class.
        /// </summary>
        /// <param name="context"></param>
        public FWRepository(DbContext context)
        {
            Context = context;
            _dbSet = Context.Set<TEntity>();
        }

        #region Sync methods
        /// <summary>
        /// Gets all records from the database.
        /// </summary>
        /// <returns>The list of entities found.</returns>
        public IEnumerable<TEntity> GetAll()
        {
            IQueryable<TEntity> obj = _dbSet;
            return obj;
        }

        /// <summary>
        /// Gets all records from the database, projected into a different object.
        /// </summary>
        /// <typeparam name="TResult">The result type.</typeparam>
        /// <param name="selector">A projection funtion the be applied.</param>
        /// <returns>The list of entities found.</returns>
        public IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
            IQueryable<TResult> obj = _dbSet.Select(selector);
            return obj;
        }

        /// <summary>
        /// Gets all records from the database paginated, projected into a different object.
        /// </summary>
        /// <typeparam name="TResult">The result type.</typeparam>
        /// <param name="selector">A projection funtion the be applied.</param>
        /// <param name="queryOptions">The data manipulation object reference</param>
        /// <returns>The list of entities found.</returns>
        public List<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector, IFWDataOptions queryOptions)
        {
            IQueryable<TEntity> query = AddOrderBy(queryOptions, _dbSet);

            var result = query.Select(selector);
            queryOptions.Total = result.Count();

            int skip = queryOptions.Display * (queryOptions.Page - 1);
            if (queryOptions.Page > 0)
                return result.Skip(skip).Take(queryOptions.Display).ToList();
            else
                return result.ToList();
        }

        /// <summary>
        /// Searchs the database for an entity.
        /// </summary>
        /// <param name="keyValue">The value of the primary key.</param>
        /// <param name="includes">Specifies related entities.</param>
        /// <returns>The entity, if any.</returns>
        public TEntity GetByID(long keyValue, params Expression<Func<TEntity, object>>[] includes)
        {
            var dbSet = Context.Set<TEntity>();

            if (includes != null && includes.Length > 0)
            {
                IQueryable<TEntity> query = dbSet;
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
                return query.AsNoTracking().FirstOrDefault(x => x.Id == keyValue);
            }
            else
            {
                var entity = dbSet.Find(keyValue);
                Context.Entry(entity).State = EntityState.Detached;
                return entity;
            }
        }

        /// <summary>
        /// Searchs the database, based on a specification.
        /// </summary>
        /// <typeparam name="TResult">The result type.</typeparam>
        /// <param name="selector">A projection funtion the be applied.</param>
        /// <param name="specification">The query specification.</param>
        /// <returns>The list of entities found.</returns>
        public List<TResult> Select<TResult>(Expression<Func<TEntity, TResult>> selector,
                        IFWSpecification<TEntity> specification)
        {
            List<TResult> result;
            if (specification.QueryOptions != null)
            {
                result = Select(selector, specification, out int total);
                specification.QueryOptions.Total = total;
            }
            else
            {
                result = _dbSet.Where(specification.CreateExpression()).Select(selector).ToList();
            }

            return result;
        }

        /// <summary>
        /// Searchs the database, based on a specification.
        /// </summary>
        /// <typeparam name="TResult">The result type.</typeparam>
        /// <param name="selector">A projection funtion the be applied.</param>
        /// <param name="specification">The query specification.</param>        
        /// <param name="total">Total records found.</param>
        /// <returns>The list of entities found.</returns>
        public List<TResult> Select<TResult>(Expression<Func<TEntity, TResult>> selector,
                        IFWSpecification<TEntity> specification, out int total)
        {
            var queryOptions = specification.QueryOptions;

            int skip = queryOptions.Display * (queryOptions.Page - 1);
            var query = this._dbSet.Where(specification.CreateExpression());
            query = AddOrderBy(queryOptions, query);

            var result = query.Select(selector);
            total = result.Count();

            if (queryOptions.Page > 0)
                return result.Skip(skip).Take(queryOptions.Display).ToList();
            else
                return result.ToList();
        }

        /// <summary>
        /// Adds the 'OrderBy' clause to the query.
        /// </summary>
        private static IQueryable<TEntity> AddOrderBy(IFWDataOptions queryOptions, IQueryable<TEntity> query)
        {
            if (queryOptions.SortInfo != null && queryOptions.SortInfo.Count > 0)
            {
                query = (queryOptions.SortInfo[0].SortDirection == FWSortDirection.Ascending) ?
                            query.OrderBy(queryOptions.SortInfo[0].SortName) :
                            query.OrderByDescending(queryOptions.SortInfo[0].SortName);

                if (queryOptions.SortInfo.Count > 1)
                {
                    for (int i = 1; i < queryOptions.SortInfo.Count; i++)
                    {
                        query = (queryOptions.SortInfo[i].SortDirection == FWSortDirection.Ascending) ?
                            query.ThenBy(queryOptions.SortInfo[i].SortName) :
                            query.ThenByDescending(queryOptions.SortInfo[0].SortName);
                    }
                }
            }
            else
            {
                query = query.OrderBy(f => f.Id);
            }

            return query;
        }

        /// <summary>
        /// Inserts or updates an entity and saves all changes made in this context to the underlying database.
        /// </summary>
        /// <param name="entity">The entity object.</param>
        /// <returns>The entity primary key.</returns>
        public long Save(TEntity entity)
        {
            if (entity.Id == 0)
            {
                Insert(entity);
            }
            else
            {
                Update(entity);
            }
            SaveChanges();

            return entity.Id;
        }

        /// <summary>
        /// Inserts a new entity on the database.
        /// </summary>
        /// <param name="entity">The entity object.</param>
        public void Insert(TEntity entity)
        {
            if (ChangeTrackerEnabled)
            {
                var graph = new FWEntityGraph(Context);
                graph.TrackChange(entity);
            }

            _dbSet.Add(entity);
        }

        /// <summary>
        /// Updates an entity.
        /// </summary>
        /// <param name="entity">The entity object.</param>
        public void Update(TEntity entity)
        {
            if (ChangeTrackerEnabled)
            {
                var graph = new FWEntityGraph(Context);
                graph.TrackChange(entity);
            }

            _dbSet.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Removes an entity from the database.
        /// </summary>
        /// <param name="keyValues">The values of the primary key.</param>
        public void Delete(params object[] keyValues)
        {
            TEntity obj = _dbSet.Find(keyValues);
            if (obj != null)
            {
                Delete(obj);
            }
        }

        /// <summary>
        /// Removes an entity from the database.
        /// </summary>
        /// <param name="entity">The entity object.</param>
        public virtual void Delete(TEntity entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);

            if (ChangeTrackerEnabled)
            {
                var graph = new FWEntityGraph(Context);
                graph.TrackDelete(entity);
            }
        }
        #endregion

        #region Async methods

        ///// <summary>
        ///// Gets all records from the database, projected into a different object.
        ///// </summary>
        ///// <typeparam name="TResult">The result type.</typeparam>
        ///// <param name="selector">The result projection.</param>
        ///// <returns>The list of entities found.</returns>
        //public async Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, TResult>> selector)
        //{
        //    //TODO: Validate async perforance.
        //    IQueryable<TResult> obj = dbSet.Select(selector);
        //    return await obj.ToListAsync();
        //}

        #endregion

        /// <summary>
        /// Return the number of elements.
        /// </summary>
        /// <returns>The number of elements in the input sequence.</returns>
        public int Count()
        {
            return _dbSet.Count();
        }

        /// <summary>
        /// Saves all changes made.
        /// </summary>
        public void SaveChanges()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Disposes the repository context.
        /// </summary>
        public void Dispose()
        {
            if (Context != null)
            {
                Context.Dispose();
                Context = null;
            }
        }

        private DbSet<TEntity> _dbSet;
    }
}
