using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core
{
    /// <summary>
    /// Represents an exception thrown when an error occur while reflecting on a type.
    /// </summary>
    public class FWReflectionException : FWException
    {
        /// <summary>
        /// Initializes a new instance of FWReflectionException.
        /// </summary>
        /// <param name="type">Type that failed the refleciton.</param>
        public FWReflectionException(Type type)
            : this(type, null)
        { }

        /// <summary>
        /// Initializes a new instance of FWReflectionException.
        /// </summary>
        /// <param name="type">Type that failed the refleciton.</param>
        /// <param name="innerException">Inner Exception.</param>
        public FWReflectionException(Type type, Exception innerException)
            : base(string.Format(Resources.Resources.FWReflectionException, type.Name), innerException)
        { }
    }
}
