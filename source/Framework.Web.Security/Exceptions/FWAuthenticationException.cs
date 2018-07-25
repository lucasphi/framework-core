using Framework.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Security
{
    /// <summary>
    /// Represents an exception thrown on a failed authentication attempt.
    /// </summary>
    public class FWAuthenticationException : FWException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FWAuthenticationException" />.
        /// </summary>
        public FWAuthenticationException() 
            : base(Resources.Resources.FWAuthenticationException)
        { }
    }
}
