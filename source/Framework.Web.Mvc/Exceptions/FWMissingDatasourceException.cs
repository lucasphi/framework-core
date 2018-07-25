using Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Web.Mvc.Exceptions
{
    /// <summary>
    /// Represents an exception thrown when a datasource is not set.
    /// </summary>
    public class FWMissingDatasourceException : FWException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FWMissingDatasourceException" />.
        /// </summary>
        /// <param name="property">The name of the property.</param>
        public FWMissingDatasourceException(string property)
            : base(string.Format(Resources.Resources.FWMissingDatasourceException, property))
        { }
    }
}
