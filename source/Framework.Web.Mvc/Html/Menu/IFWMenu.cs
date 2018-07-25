using Framework.Web.Mvc.Html.Elements;
using Framework.Web.Mvc.Html.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Web.Mvc.Html
{
    /// <summary>
    /// Defines de interface for menus.
    /// </summary>
    public interface IFWMenu
    {
        /// <summary>
        /// Adds a new menu item.
        /// </summary>
        /// <param name="text">The menu item text.</param>
        /// <param name="icon">The menu item icon.</param>
        /// <returns>The fluent object reference for the child menu.</returns>
        IFWMenu AddMenu(string text, string icon = null);

        /// <summary>
        /// Adds a link to the menu.
        /// </summary>
        /// <param name="text">The link text.</param>
        /// <param name="action">The url action.</param>
        /// <param name="controller">The url controller.</param>
        /// <param name="routeValues">The url routeValues.</param>
        /// <param name="icon">The link icon.</param>
        /// <param name="configure">An action to configure the link anchor.</param>
        /// <returns>The fluent object reference for the child menu.</returns>
        void AddLink(string text, string action, string controller = null, object routeValues = null, string icon = null, Action<FWAnchorElement> configure = null);

        /// <summary>
        /// Adds a label to the menu.
        /// </summary>
        /// <param name="text">The label text.</param>
        /// <returns>The fluent object reference for the child menu.</returns>
        void AddLabel(string text);

        /// <summary>
        /// Adds a separator to the menu items.
        /// </summary>
        void AddSeparator();
    }
}
