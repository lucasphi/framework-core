using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Repository
{
    /// <summary>
    /// Represents a basic entity object.
    /// </summary>
    public abstract class FWAuditEntity : FWEntity
    {
        /// <summary>
        /// Gets or sets the name of the user that created the object.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date of creation.
        /// </summary>
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the name of the user that modified the object.
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the date of change.
        /// </summary>
        public DateTime? ModifiedOn { get; set; }

    }
}
