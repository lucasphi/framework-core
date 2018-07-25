using Framework.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mvc.Exceptions
{
    /// <summary>
    /// Represents an exception thrown when a control template is not configurated.
    /// </summary>
    public class FWMissingTemplateException : FWException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FWMissingTemplateException" />.
        /// </summary>
        public FWMissingTemplateException(string controlId) 
            : base(string.Format(Resources.Resources.FWMissingTemplateException, controlId))
        { }
    }
}
