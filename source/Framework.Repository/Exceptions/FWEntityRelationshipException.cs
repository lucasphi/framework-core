using Framework.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Repository.Exceptions
{
    /// <summary>
    /// Represents an exception thrown when an entity relationship is misconfigured.
    /// </summary>
    public class FWEntityRelationshipException : FWException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FWEntityRelationshipException" />.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public FWEntityRelationshipException(string message)
            : base(message)
        { }
    }
}
