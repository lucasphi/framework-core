using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mvc.Html
{
    /// <summary>
    /// Common interface for template options.
    /// </summary>
    public interface IFWTemplateOptions
    {
        /// <summary>
        /// Gets the template control Id.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Gets the type of the template item.
        /// </summary>
        Type ItemType { get; }
    }
}
