using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core
{
    /// <summary>
    /// Base framework exception.
    /// </summary>
    public abstract class FWException : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="FWException"/>.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public FWException(string message)
            : base(message)
        { }

        /// <summary>
        /// Initializes a new instance of FWException.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="innerException">Inner Exception.</param>
        public FWException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
