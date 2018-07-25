using Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Repository
{
    /// <summary>
    /// Defines the common interface for a generic repository.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public interface IFWRepository<TEntity>
        where TEntity : class, IFWEntity<long>
    {
        /// <summary>
        /// Gets all records from the database.
        /// </summary>
        /// <returns>The list of entities found.</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Gets all records from the database, projected into a different object.
        /// </summary>
        /// <typeparam name="TResult">The result type.</typeparam>
        /// <param name="selector">A projection funtion the be applied.</param>
        /// <returns>The list of entities found.</returns>
        IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector);

        /// <summary>
        /// Gets all records from the database paginated, projected into a different object.
        /// </summary>
        /// <typeparam name="TResult">The result type.</typeparam>
        /// <param name="selector">A projection funtion the be applied.</param>
        /// <param name="pagination">The pagination object reference.</param>
        /// <returns>The list of entities found.</returns>
        List<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector, IFWDataOptions pagination);

        /// <summary>
        /// Searchs the database for an entity.
        /// </summary>
        /// <param name="keyValue">The value of the primary key.</param>
        /// <param name="includes">Specifies related entities.</param>
        /// <returns>The entity, if any.</returns>
        TEntity GetByID(long keyValue, params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Searchs the database, based on a specification.
        /// </summary>
        /// <typeparam name="TResult">The result type.</typeparam>
        /// <param name="projection">The result projection.</param>
        /// <param name="specification">The query specification.</param>
        /// <returns>The list of entities found.</returns>
        List<TResult> Select<TResult>(Expression<Func<TEntity, TResult>> projection,
                        IFWSpecification<TEntity> specification);

        /// <summary>
        /// Searchs the database, based on a specification.
        /// </summary>
        /// <typeparam name="TResult">The result type.</typeparam>
        /// <param name="projection">The result projection.</param>
        /// <param name="specification">The query specification.</param>
        /// <param name="total">Total records found.</param>
        /// <returns>The list of entities found.</returns>
        List<TResult> Select<TResult>(Expression<Func<TEntity, TResult>> projection,
                        IFWSpecification<TEntity> specification, out int total);

        /// <summary>
        /// Inserts a new entity on the database.
        /// </summary>
        /// <param name="entity">The entity object.</param>
        /// <returns>The entity primary key.</returns>
        void Insert(TEntity entity);

        /// <summary>
        /// Updates an entity.
        /// </summary>
        /// <param name="entity">The entity object.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Removes an entity from the database.
        /// </summary>
        /// <param name="keyValues">The values of the primary key.</param>
        void Delete(params object[] keyValues);

        /// <summary>
        /// Removes an entity from the database.
        /// </summary>
        /// <param name="entity">The entity object.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Return the number of elements.
        /// </summary>
        /// <returns>The number of elements in the input sequence.</returns>
        int Count();

        /// <summary>
        /// Saves all changes made.
        /// </summary>
        void SaveChanges();
    }
}