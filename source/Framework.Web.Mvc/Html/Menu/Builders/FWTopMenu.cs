using Framework.Core;
using Framework.Web.Mvc.Html.Elements;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Web.Mvc.Html.Menu
{
    /// <summary>
    /// Represents a user menu control.
    /// </summary>
    internal class FWTopMenu : IFWMenuBuilder
    {
        /// <summary>
        /// Creates a new instance of the Framework.Web.Mvc.Html.Menu.FWUserMenu class.
        /// </summary>
        /// <param name="urlHelper">The curent urlhelper</param>
        public FWTopMenu(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;

            _userMenu = new FWListItemElement();
            _userMenu.AddCssClass("m-nav__item m-topbar__user-profile m-topbar__user-profile--img m-dropdown m-dropdown--medium m-dropdown--arrow m-dropdown--header-bg-fill m-dropdown--align-right m-dropdown--mobile-full-width m-dropdown--skin-light");
            _userMenu.Attributes.Add("data-type", "fw-topmenu");
        }

        public IFWMenuBuilder Holder(string label, string icon)
        {
            var anchor = new FWAnchorElement("javascript:;");
            anchor.AddCssClass("m-nav__link m-dropdown__toggle");

            if (!string.IsNullOrWhiteSpace(icon))
            {
                anchor.Add(CreateHolderIcon(icon));
            }

            if (!string.IsNullOrWhiteSpace(label))
            {
                var span = new FWSpanElement();
                span.AddCssClass("topbar-text");
                span.Add(label);
                anchor.Add(span);
                anchor.Add(CreateHolderIcon("fa fa-angle-down"));
            }
            _userMenu.Add(anchor);

            var itemsWrapper = new FWDivElement();
            itemsWrapper.AddCssClass("m-dropdown__wrapper");

            var modalArrow = itemsWrapper.Add(new FWSpanElement());
            modalArrow.AddCssClass("m-dropdown__arrow m-dropdown__arrow--right m-dropdown__arrow--adjust");
            var modalArrowIcon = modalArrow.Add(new FWTagBuilder("i"));
            modalArrowIcon.AddCssClass("fa fa-chevron-up");

            var innerDiv = itemsWrapper.Add(new FWDivElement());
            innerDiv.AddCssClass("m-dropdown__inner");

            var modalBody = innerDiv.Add(new FWDivElement());
            modalBody.AddCssClass("m-dropdown__body");

            var modalContent = modalBody.Add(new FWDivElement());
            modalContent.AddCssClass("m-dropdown__content");

            _menuItems = new FWListElement();
            _menuItems.AddCssClass("m-nav m-nav--skin-light");
            modalContent.Add(_menuItems);

            _userMenu.Add(itemsWrapper);

            return this;
        }

        private static FWSpanElement CreateHolderIcon(string icon)
        {
            var iconSpan = new FWSpanElement();
            iconSpan.AddCssClass("m-nav__link-icon");

            var iconTag = iconSpan.Add(new FWTagBuilder("i"));
            iconTag.AddCssClass(icon);

            return iconSpan;
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
        /// <returns>A user menu fluent configurator.</returns>
        public void Item(string label, string action, string controller, object routeValues, string icon = null, Action<FWAnchorElement> configureAnchor = null)
        {
            FWListItemElement li = new FWListItemElement();
            li.AddCssClass("m-nav__item");

            FWAnchorElement anchor = new FWAnchorElement(_urlHelper.Action(action, controller, routeValues));
            anchor.AddCssClass("m-nav__link");

            if (!string.IsNullOrWhiteSpace(icon))
            {
                FWTagBuilder iconSpan = new FWTagBuilder("i");
                iconSpan.AddCssClass(icon);
                anchor.Add(iconSpan);
            }

            var anchorSpan = new FWSpanElement(label);
            anchorSpan.AddCssClass("m-nav__link-text");
            anchor.Add(anchorSpan);

            // Runs any customizations to the item anchor.
            configureAnchor?.Invoke(anchor);

            li.Add(anchor);

            _menuItems.Add(li);
        }

        /// <summary>
        /// Creates a divider line.
        /// </summary>
        public void Separator()
        {
            _menuItems.Add("<li class=\"m-nav__separator m-nav__separator--fit\"> </li>");
        }

        public void Label(string label)
        {
            // This menu does not have an implementation of Label.
        }

        /// <summary>
        /// Returns the HTML formated string for the Menu.
        /// </summary>
        /// <returns>The HTML string.</returns>
        public string ToHtml()
        {
            return _userMenu.ToString();
        }
        
        private FWListItemElement _userMenu;
        private FWListElement _menuItems;

        private IUrlHelper _urlHelper;
    }
}
