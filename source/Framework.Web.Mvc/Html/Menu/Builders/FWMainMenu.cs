using Framework.Web.Mvc.Html.Elements;
using Framework.Web.Mvc.Html.Menu.Builders;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Framework.Web.Mvc.Html.Menu
{
    /// <summary>
    /// Represents a vertical menu control.
    /// </summary>
    internal class FWMainMenu : IFWMenuBuilder
    {
        /// <summary>
        /// Creates a new <see cref="FWMainMenu"/>.
        /// </summary>
        /// <param name="urlHelper">The curent urlhelper.</param>
        public FWMainMenu(IUrlHelper urlHelper)
            : this(FWMenuItemType.Main, urlHelper)
        { }

        private FWMainMenu(FWMenuItemType itemType, IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;

            _currentTag = new FWListElement();
            _currentTag.AddCssClass("m-menu__nav  m-menu__nav--dropdown-submenu-arrow");

            ItemType = itemType;
        }

        /// <summary>
        /// Creates a new menu group.
        /// </summary>
        /// <param name="label">The group text.</param>
        /// <param name="icon">The group icon.</param>
        /// <returns>The fluent FWMenuControl object.</returns>
        public IFWMenuBuilder Holder(string label, string icon)
        {
            FWMainMenu child = new FWMainMenu(FWMenuItemType.Holder, _urlHelper);

            _menuHolder = new FWListItemElement();
            _menuHolder.AddCssClass("m-menu__item  m-menu__item--submenu");            

            _menuAnchor = new FWAnchorElement("javascript:;");
            _menuAnchor.AddCssClass("m-menu__link m-menu__toggle");

            _menuAnchor.Add(CreateItemIcon(icon));
            _menuAnchor.Add(string.Format("<span class=\"m-menu__link-text\">{0}</span>", label));

            _menuArrow = new FWTagBuilder("i")
            {
                DataType = "fw-menu-arrow"
            };
            _menuArrow.AddCssClass("fw-menu-arrow");

            _menuAnchor.Add(_menuArrow);
            _menuHolder.Add(_menuAnchor);

            var subHolder = new FWDivElement();
            subHolder.AddCssClass("m-menu__submenu");
            if (ItemType != FWMenuItemType.Main)
            {
                subHolder.AddCssClass("m-menu__submenu--classic m-menu__submenu--right");
            }

            var subHolderArrow = new FWSpanElement("<span aria-hidden=\"true\"></span>");
            subHolderArrow.AddCssClass("fw-submenu-arrow");
            subHolder.Add(subHolderArrow);

            var innerList = new FWListElement();
            innerList.AddCssClass("m-menu__subnav");
            subHolder.Add(innerList);

            _menuHolder.Add(subHolder);

            this._currentTag.Add(_menuHolder);

            child._currentTag = innerList;
            child._parent = this;
            return child;
        }

        private FWTagBuilder CreateItemIcon(string icon)
        {
            var iconTag = new FWTagBuilder("i");
            if (ItemType == FWMenuItemType.Main || icon != null)
            {
                iconTag.AddCssClass($"m-menu__link-icon {icon}");
            }
            else
            {
                iconTag.AddCssClass("m-menu__link-bullet");
            }
            return iconTag;
        }

        /// <summary>
        /// Creates a new menu link.
        /// </summary>
        /// <param name="label">The link text.</param>
        /// <param name="action">The url action.</param>
        /// <param name="controller">The url controller.</param>
        /// <param name="routeValues">The url routeValues.</param>
        /// <param name="icon">The link icon.</param>
        /// <param name="configureAnchor">Delegate to configurate the item anchor.</param>
        /// <returns>The fluent FWMenuControl object.</returns>
        public void Item(string label, string action, string controller, object routeValues, string icon = null, Action<FWAnchorElement> configureAnchor = null)
        {
            var menuItem = new FWListItemElement();
            menuItem.AddCssClass("m-menu__item");

            bool active = false;

            //Verify if the url is active     
            //if (IsActive(HttpContext.Current.Request.Url.PathAndQuery, link))
            //{
            //    active = true;
            //    menuItem.AddCssClass("active");
            //    if (this.ItemType != MenuItemType.Main)
            //    {
            //        menuItem.AddCssClass("open");
            //        SetMenuActive(this._parent);
            //    }
            //}

            FWAnchorElement anchor = new FWAnchorElement(_urlHelper.Action(action, controller, routeValues));
            anchor.AddCssClass("m-menu__link");

            // Adds a title if the link holder is the main menu. This will help distinguish the icons if the menu is minimized.
            if (ItemType == FWMenuItemType.Main)
            {
                anchor.Title = label;
            }

            var anchorDot = CreateItemIcon(icon);
            anchorDot.Add(new FWSpanElement());
            anchor.Add(anchorDot);

            // Adds the span selected to the anchor.
            if (active)
            {
                AddActiveSpan(anchor);
            }

            FWSpanElement labelSpan = new FWSpanElement(label);
            labelSpan.AddCssClass("m-menu__link-text");
            anchor.Add(labelSpan);

            // Runs any customizations to the item anchor.
            configureAnchor?.Invoke(anchor);

            menuItem.Add(anchor);

            this._currentTag.Add(menuItem);
        }

        /// <summary>
        /// Create a new menu Label.
        /// </summary>
        /// <param name="label">The label text.</param>
        /// <returns>The fluent FWMenuControl object.</returns>
        public void Label(string label)
        {
            var menuLabel = new FWListItemElement();
            menuLabel.AddCssClass("m-menu__section");

            var h3 = new FWTagBuilder("h4");
            h3.Add(label);

            menuLabel.Add(h3);

            this._currentTag.Add(menuLabel);
        }

        /// <summary>
        /// Adds a separator to the menu items.
        /// </summary>
        public void Separator()
        {
            // This menu does not have an implementation of separator.
        }

        private void AddActiveSpan(FWAnchorElement anchor)
        {
            var spanSelected = new FWSpanElement();
            spanSelected.AddCssClass("selected");

            if (this._parent == null)
            {
                anchor.Add(spanSelected);
            }
            else
            {
                FWMainMenu parent = this;
                while (parent._parent != null)
                {
                    parent = parent._parent;
                }
                parent._menuAnchor.Add(spanSelected);
            }
        }

        private bool IsActive(string url, string link)
        {
            url += "/";
            link += "/";
            return url.StartsWith(link);
        }

        private void SetMenuActive(FWMainMenu menu)
        {
            menu._menuHolder.AddCssClass("active open");
            menu._menuArrow.AddCssClass("open");
            if (menu._parent != null)
            {
                SetMenuActive(menu._parent);
            }
        }

        /// <summary>
        /// Returns the HTML formated string for the Menu.
        /// </summary>
        /// <returns>The HTML string.</returns>
        public string ToHtml()
        {
            return _currentTag.ToString();
        }

        /// <summary>
        /// Gets the type of the menu item.
        /// </summary>
        private FWMenuItemType ItemType { get; set; }
        
        private FWTagBuilder _currentTag;
        private FWMainMenu _parent;

        private FWListItemElement _menuHolder;
        private FWTagBuilder _menuArrow;
        private FWAnchorElement _menuAnchor;

        private IUrlHelper _urlHelper;
    }
}
