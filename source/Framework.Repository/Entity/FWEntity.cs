using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Repository
{
    /// <summary>
    /// Represents a basic entity with key.
    /// </summary>
    public abstract class FWEntity : IFWEntity<long>
    {
        /// <summary>
        /// Gets or sets the item primary key.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }       

        /// <summary>
        /// Marks the entity as been removed.
        /// </summary>
        public void Remove()
        {
            _isMarkedRemoved = true;
        }

        internal bool IsRemoved()
        {
            return _isMarkedRemoved;
        }

        private bool _isMarkedRemoved;
    }
}
