using Framework.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mvc.Exceptions
{
    /// <summary>
    /// Represents an exception thrown a model has two or more properties decorated with FWKeyAttribute.
    /// </summary>
    public class FWMultipleKeyException : FWException
    {
        /// <summary>
        /// Creates a new instance of the Framework.Web.FWMultipleKeyException class.
        /// </summary>
        /// <param name="className">The class name.</param>
        /// <param name="innerException">The inner exception.</param>
        public FWMultipleKeyException(string className, Exception innerException)
            : base(string.Format(Resources.Resources.FWMultipleKeyException, className), innerException)
        { }
    }
}
