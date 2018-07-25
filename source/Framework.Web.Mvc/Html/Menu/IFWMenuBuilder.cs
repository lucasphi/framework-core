using Framework.Web.Mvc.Html.Elements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mvc.Html.Menu
{
    /// <summary>
    /// Defines the interface for menu builders.
    /// </summary>
    public interface IFWMenuBuilder
    {
        /// <summary>
        /// Creates a new menu subitem.
        /// </summary>
        /// <param name="label">The subitem text.</param>
        /// <param name="icon">The subitem icon.</param>
        /// <returns>The fluent object reference.</returns>
        IFWMenuBuilder Holder(string label, string icon);

        /// <summary>
        /// Create a new menu link.
        /// </summary>
        /// <param name="label">The link text.</param>
        /// <param name="action">The url action.</param>
        /// <param name="controller">The url controller.</param>
        /// <param name="routeValues">The url routeValues.</param>
        /// <param name="icon">The link icon.</param>
        /// <param name="configureAnchor">Delegate to configurate the item anchor.</param>
        /// <returns>The fluent object reference.</returns>
        void Item(string label, string action, string controller, object routeValues, string icon = null, Action<FWAnchorElement> configureAnchor = null);

        /// <summary>
        /// Creates a new menu label.
        /// </summary>
        /// <param name="label">The label text.</param>
        /// <returns>The fluent object reference.</returns>
        void Label(string label);

        /// <summary>
        /// Adds a separator to the menu items.
        /// </summary>
        void Separator();

        /// <summary>
        /// Returns the HTML formated string for the Menu.
        /// </summary>
        /// <returns>The HTML string.</returns>
        string ToHtml();
    }
}
