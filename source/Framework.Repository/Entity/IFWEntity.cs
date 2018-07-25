using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Repository
{
    /// <summary>
    /// Common interface for an entity type.
    /// </summary>
    /// <typeparam name="TKey">The key type.</typeparam>
    public interface IFWEntity<TKey> : IFWEntity
    {
        /// <summary>
        /// Gets or sets the item primary key.
        /// </summary>
        TKey Id { get; set; }
    }

    /// <summary>
    /// Common interface for an entity type.
    /// </summary>
    public interface IFWEntity
    { }
}
