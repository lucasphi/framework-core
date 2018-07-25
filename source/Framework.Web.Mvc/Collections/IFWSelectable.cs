using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Collections
{
    /// <summary>
    /// Defines the interface for classes with a selected value list.
    /// </summary>
    public interface IFWSelectable
    {
        /// <summary>
        /// Gets or sets the selected values.
        /// </summary>
        IEnumerable Selected { get; set; }
    }
}
