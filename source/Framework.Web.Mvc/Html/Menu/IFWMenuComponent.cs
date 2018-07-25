using Framework.Web.Mvc.Html.Menu;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mvc.Html
{
    /// <summary>
    /// Declares the common interface for menu components.
    /// </summary>
    public interface IFWMenuComponent
    {
        /// <summary>
        /// Creates a new menu.
        /// </summary>
        /// <param name="builder">The concrete builder.</param>
        IFWMenuBuilder Build(IFWMenuBuilder builder);

        /// <summary>
        /// Gets the menu hierarchy level.
        /// </summary>
        int Level { get; }
    }
}
