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
    /// Defines the interface for query filtering.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public interface IFWSpecification<TEntity>
    {
        /// <summary>
        /// Gets or sets the query options for the specification.
        /// </summary>
        IFWDataOptions QueryOptions { get; set; }

        /// <summary>
        /// Creates the expression for the 'where' clause.
        /// </summary>
        /// <returns></returns>
        Expression<Func<TEntity, bool>> CreateExpression();
    }
}
