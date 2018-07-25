using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core
{
    /// <summary>
    /// Represents an exception thrown when an invalid type cast is attempted.
    /// </summary>
    public class FWInvalidCastException : FWException
    {
        /// <summary>
        /// Initializes a new instance of Framework.Core.FWInvalidCastException.
        /// </summary>
        /// <param name="value">The value attempted to cast.</param>
        /// <param name="type">The type which the value should be cast.</param>
        public FWInvalidCastException(string value, Type type)
            : base(string.Format(Resources.Resources.FWInvalidCastException, value, type.Name))
        { }
    }
}
