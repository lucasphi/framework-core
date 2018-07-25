using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core
{
    /// <summary>
    /// The exception that is thrown when an invoked method is not supported, or when there is an attempt to read, seek, or write to a stream that does not support the invoked functionality.
    /// </summary>
    public class FWNotSupportedException : FWException
    {
        /// <summary>
        /// Initializes a new instance of FWNotSupportedException.
        /// </summary>
        /// <param name="innerException">Inner Exception.</param>
        public FWNotSupportedException(Exception innerException)
            : base(Resources.Resources.FWNotSupportedException, innerException)
        { }
    }
}
