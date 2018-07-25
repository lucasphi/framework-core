using Framework.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mvc.Exceptions
{
    /// <summary>
    /// Represents an exception thrown the request is interrupted before it finishes its life cycle.
    /// </summary>
    public class FWRequestInterruptException : FWException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FWRequestInterruptException" />.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="responseCode">The interruption response code.</param>
        public FWRequestInterruptException(string message, int responseCode = 500) 
            : base(message)
        {
            ResponseCode = responseCode;
        }

        /// <summary>
        /// Gets the interruption response code.
        /// </summary>
        public int ResponseCode { get; private set; }
    }
}
