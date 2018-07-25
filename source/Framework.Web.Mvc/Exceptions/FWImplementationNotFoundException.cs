using Framework.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mvc.Exceptions
{
    /// <summary>
    /// Represents an exception thrown the IoC container cannot resolve a type.
    /// </summary>
    public class FWImplementationNotFoundException : FWException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FWImplementationNotFoundException" />.
        /// </summary>
        /// <param name="serviceType">The service type.</param>
        public FWImplementationNotFoundException(Type serviceType)
            : base(string.Format(Resources.Resources.FWImplementationNotFoundException, serviceType.ToString()))
        { }
    }
}
