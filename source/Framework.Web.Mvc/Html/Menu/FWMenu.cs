using Framework.Web.Mvc.Html.Elements;
using Framework.Web.Mvc.Html.Menu;
using Framework.Web.Mvc.Html.Menu.Components;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mvc.Html
{
    /// <summary>
    /// Represents a generic framework menu.
    /// </summary>
    public class FWMenu : IFWMenu, IFWMenuComponent
    {
        /// <summary>
        /// Gets the menu hierarchy level.
        /// </summary>
        public int Level { get; internal set; }

        /// <summary>
        /// Gets the current Http Context.
        /// </summary>
        public HttpContext HttpContext
        {
            get { return FWHttpContext.Current; }
        }

        /// <summary>
        /// Adds a new menu item.
        /// </summary>
        /// <param name="text">The menu item text.</param>
        /// <param name="icon">The menu item icon.</param>
        /// <returns>The fluent object reference for the child menu.</returns>
        public IFWMenu AddMenu(string text, string icon = null)
        {
            var childMenu = new FWMenu()
            {
                Level = Level + 1
            };
            Items.Add(new FWMenuGroup() { Text = text, Icon = icon, Level = Level });
            Items.Add(childMenu);
            return childMenu;
        }

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
        public void AddLink(string text, string action, string controller = null, object routeValues = null, string icon = null, Action<FWAnchorElement> configure = null)
        {
            Items.Add(new FWMenuLink()
            {
                Text = text,
                Action = action,
                Controller = controller,
                RouteValues = routeValues,
                Icon = icon,
                Level = Level,
                Configure = configure });
        }

        /// <summary>
        /// Adds a label to the menu.
        /// </summary>
        /// <param name="text">The label text.</param>
        /// <returns>The fluent object reference for the child menu.</returns>
        public void AddLabel(string text)
        {
            Items.Add(new FWMenuLabel() { Text = text, Level = Level });
        }

        /// <summary>
        /// Adds a separator to the menu items.
        /// </summary>
        public void AddSeparator()
        {
            Items.Add(new FWMenuSeparator() { Level = Level });
        }

        /// <summary>
        /// Creates a new menu.
        /// </summary>
        /// <param name="builder">The concrete builder.</param>
        public IFWMenuBuilder Build(IFWMenuBuilder builder)
        {
            int level = Level;
            builders[Level] = builder;

            foreach (var item in Items)
            {
                var innerBuilder = item.Build(builders[item.Level]);
                if (innerBuilder != null)
                    builders[item.Level + 1] = innerBuilder;
            }

            return builder;
        }

        private Dictionary<int, IFWMenuBuilder> builders = new Dictionary<int, IFWMenuBuilder>();

        private List<IFWMenuComponent> Items { get; set; } = new List<IFWMenuComponent>();
    }
}
