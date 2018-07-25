using Framework.Web.Mvc.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Framework.Web.Mvc.DataAnnotations
{
    /// <summary>
    /// Specifies that a field is required.
    /// </summary>
    public class FWRequiredAttribute : RequiredAttribute
    {
        /// <summary>
        /// Applies formatting to an error message, based on the data field where the error occurred.
        /// </summary>
        /// <param name="name">The name to include in the formatted message.</param>
        /// <returns>An instance of the formatted error message.</returns>
        public override string FormatErrorMessage(string name)
        {
            return string.Format(ViewResources.Validation_Required, name);
        }
    }
}
