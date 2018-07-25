using Framework.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mvc.Exceptions
{
    /// <summary>
    /// Represents an exception thrown when a control attempts to use an invalid model for its configuration.
    /// </summary>
    public class FWInvalidModelException : FWException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FWInvalidModelException" />.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public FWInvalidModelException(string message) 
            : base(message)
        { }
    }
}
