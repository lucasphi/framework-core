using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Core
{
    /// <summary>
    /// Represents an exception thrown during Ioc initialization.
    /// </summary>
    public class FWIocInitializationException : FWException
    {
        /// <summary>
        /// Initializes a new instance of Framework.Core.FWIocInitializationException.
        /// </summary>
        /// <param name="innerException">The inner exception.</param>
        public FWIocInitializationException(Exception innerException) 
            : base(Resources.Resources.FWIocInitializationException, innerException)
        { }
    }
}
