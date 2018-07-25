using Framework.Model;
using Framework.Web.Mvc.Html.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Web.Mvc.Html.Controls
{
    /// <summary>
    /// Represents a group of buttons.
    /// </summary>
    public class FWButtonGroupControl : FWControl, IFWStateControl
    {
        /// <summary>
        /// Creates the control main element.
        /// </summary>
        /// <returns>The control IFWHtmlElement interface.</returns>
        protected override IFWHtmlElement CreateControl()
        {
            var group = new FWDivElement();
            group.MergeAttributes(Attributes);
            if (!string.IsNullOrWhiteSpace(CustomCss))
                group.AddCssClass(CustomCss);

            group.AddCssClass("btn-group");

            // Adds the visible group buttons.
            for (int i = 0; i < _numberOfVisibleButtons; i++)
            {
                var btn = _buttons[i];
                var stateBtn = (btn as IFWStateControl);
                if (_color.HasValue)
                    stateBtn.Color(_color.Value);
                if (_size.HasValue)
                    stateBtn.Size(_size.Value);
                group.Add(btn.ToString());
            }

            // Checks if all buttons were rendered already.
            if (_numberOfVisibleButtons < _buttons.Count)
            {
                // Creates the dropdown menu for the remaining buttons.
                var dropdown = group.Add(new FWButtonElement());
                dropdown.AddCssClass("btn");
                if (_color.HasValue)
                    dropdown.AddCssClass($"btn-{_color.GetDescription()}");
                if (_size.HasValue && _size.Value != FWElementSize.Regular)
                    dropdown.AddCssClass($"btn-{_size.GetDescription()}");

                dropdown.Attributes.Add("data-toggle", "dropdown");
                dropdown.Attributes.Add("aria-haspopup", "true");
                dropdown.Attributes.Add("aria-expanded", "false");
                var dropdownSpan = dropdown.Add(new FWSpanElement());
                if (string.IsNullOrWhiteSpace(Title))
                {
                    dropdownSpan.Add("Dropdown");
                    dropdownSpan.AddCssClass("sr-only");
                    dropdown.Add("<span><i class=\"fa fa-chevron-down\"></i></span>");
                }
                else
                {
                    dropdownSpan.Add(Title);
                    dropdownSpan.Add(" <i class=\"fa fa-chevron-down\"></i>");
                }                

                var menu = group.Add(new FWDivElement());
                menu.AddCssClass("dropdown-menu");
                for (int i = _numberOfVisibleButtons; i < _buttons.Count; i++)
                {
                    var btn = _buttons[i];
                    btn.Grouped = true;
                    btn.AddCssClass("dropdown-item");
                    menu.Add(btn.ToString());
                }
            }

            return group;
        }

        /// <summary>
        /// Adds a button.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns>The fluent configurator object.</returns>
        public FWButtonGroupControl AddButton<T>(T button)
            where T : FWButtonControl, IFWStateControl
        {            
            _buttons.Add(button);
            return this;
        }

        /// <summary>
        /// Sets the group color.
        /// </summary>
        /// <param name="color">The color.</param>
        public void Color(FWStateColors color)
        {
            _color = color;
        }

        /// <summary>
        /// Defines the display size of the control.
        /// </summary>
        /// <param name="size">The size enum.</param>
        public void Size(FWElementSize size)
        {
            _size = size;
        }

        /// <summary>
        /// Creates a new <see cref="FWButtonGroupControl"/>.
        /// </summary>
        /// <param name="id">The group id.</param>
        /// <param name="visibleButtons">The number of visible buttons.</param>
        public FWButtonGroupControl(string id, int visibleButtons) 
            : base(id)
        {
            _numberOfVisibleButtons = visibleButtons;
        }

        private IList<FWButtonControl> _buttons = new List<FWButtonControl>();
        private int _numberOfVisibleButtons;
        private FWStateColors? _color;
        private FWElementSize? _size;
    }
}
