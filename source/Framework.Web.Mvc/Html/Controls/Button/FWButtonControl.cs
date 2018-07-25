using Framework.Model;
using Framework.Web.Mvc.Html.Elements;
using Framework.Web.Mvc.Resources;
using System;

namespace Framework.Web.Mvc.Html.Controls
{
    /// <summary>
    /// Represents a button control.
    /// </summary>
    public class FWButtonControl : FWControl, IFWStateControl
    {
        /// <summary>
        /// Creates the control main element.
        /// </summary>
        /// <returns>The control IFWHtmlElement interface.</returns>
        protected override IFWHtmlElement CreateControl()
        {
            var button = new FWButtonElement();
            button.MergeAttributes(Attributes);
            if (!string.IsNullOrWhiteSpace(CustomCss))
                button.AddCssClass(CustomCss);

            button.Id = Id ?? Guid.NewGuid().ToString();
            button.DataType = DataType;
            button.ButtonType = _buttonType;
            if (!Grouped)
            {
                button.AddCssClass("btn");
                if (_color.Value == FWStateColors.Secondary)
                {
                    button.AddCssClass("active");
                }
            }
            button.AddCssClass($"m-btn m-btn--square btn-{_color.Value.GetDescription()}");

            // Configures the button attributes.
            ConfigureAttributes(button);

            // Adds the button content (text, icons etc).
            AddButtonContent(button);

            if (_confirmTitle != null)
            {
                button.Attributes.Add("data-toggle", "confirmation");
                button.Attributes.Add("data-confirmtitle", _confirmTitle);
                button.Attributes.Add("data-confirmmessage", _confirmMessage);
                button.Attributes.Add("data-btnok", ViewResources.Yes);
                button.Attributes.Add("data-btncancel", ViewResources.No);
            }

            return button;
        }

        private void AddButtonContent(FWButtonElement button)
        {
            if (!string.IsNullOrWhiteSpace(_icon))
            {
                if (!string.IsNullOrWhiteSpace(Text))
                {
                    //Adds the button icon and text
                    button.AddCssClass("m-btn--icon");
                    var iconSpan = new FWSpanElement();
                    iconSpan.Add(CreateIcon());

                    iconSpan.Add(new FWSpanElement(Text));

                    button.Add(iconSpan);
                }
                else
                {
                    button.AddCssClass("m-btn--icon m-btn--icon-only");
                    button.Add(CreateIcon());
                }
            }
            else
            {
                //Adds the button text
                button.Add(Text);
            }
        }

        private FWTagBuilder CreateIcon()
        {
            var tag = new FWTagBuilder("i");
            tag.AddCssClass(_icon);
            return tag;
        }        

        /// <summary>
        /// Sets an image for the button.
        /// </summary>
        /// <param name="icon">The image name.</param>
        /// <returns>The fluent configurator object.</returns>
        public FWButtonControl Icon(string icon)
        {
            _icon = icon;
            return this;
        }

        /// <summary>
        /// Sets the button type.
        /// </summary>
        /// <param name="type">The button type.</param>
        /// <returns>The fluent configurator object.</returns>
        public FWButtonControl ButtonType(FWButtonType type)
        {
            _buttonType = type;
            if (type == FWButtonType.Reset)
            {
                Behavior(FWButtonBehavior.Reset);
            }
            return this;
        }

        /// <summary>
        /// Sets the button url to redirect on click or the submit form action.
        /// </summary>
        /// <param name="url">The url link.</param>
        /// <returns>The fluent configurator object.</returns>
        public FWButtonControl Url(string url)
        {
            _url = url;
            return this;
        }

        /// <summary>
        /// Sets the button default behavior.
        /// </summary>
        /// <param name="behavior">The button behavior.</param>
        /// <returns>The fluent configurator object.</returns>
        public FWButtonControl Behavior(FWButtonBehavior behavior)
        {
            _behavior = behavior.ToString();
            switch(behavior)
            {
                case FWButtonBehavior.Save:
                    Icon("far fa-save");
                    Title = ViewResources.Btn_Save;
                    _color = FWStateColors.Info;
                    break;
                case FWButtonBehavior.Edit:
                    Icon("far fa-edit");
                    Title = ViewResources.Btn_Edit;
                    break;
                case FWButtonBehavior.Exclude:
                    Icon("far fa-trash-alt");
                    _color = FWStateColors.Danger;
                    Title = ViewResources.Btn_Remove;
                    break;
            }
            return this;
        }

        /// <summary>
        /// Sets a custom behavior for the button.
        /// </summary>
        /// <param name="customBehavior">The custom behavior.</param>
        /// <returns>The fluent configurator object.</returns>
        public FWButtonControl Behavior(string customBehavior)
        {
            _behavior = customBehavior;
            return this;
        }

        /// <summary>
        /// Adds a confirmation message before the button event.
        /// </summary>
        /// <param name="title">The confirmation title.</param>
        /// <param name="message">The message to display.</param>
        /// <returns>The fluent configurator object.</returns>
        public FWButtonControl Confirm(string title, string message)
        {
            _confirmMessage = message;
            _confirmTitle = title;
            return this;
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
        /// Sets the button state color.
        /// </summary>
        /// <param name="color">The button color.</param>
        public void Color(FWStateColors color)
        {
            _color = color;
        }

        /// <summary>
        /// Creates a new Button.
        /// </summary>
        public FWButtonControl()
            : base()
        { }

        /// <summary>
        /// Creates a new Button.
        /// </summary>
        /// <param name="id">The button id.</param>
        public FWButtonControl(string id)
            : base(id)
        { }

        private void ConfigureAttributes(FWButtonElement button)
        {
            button.Size(_size);

            if (IsHidden)
            {
                button.AddCssClass("hidden");
            }

            if (!string.IsNullOrWhiteSpace(Title))
            {
                button.Attributes.Add("title", Title);
            }

            if (_behavior != null)
            {
                button.Attributes.Add("data-behavior", _behavior.ToString());
            }

            if (_url != null)
            {
                if (_buttonType != FWButtonType.Submit)
                    button.Attributes.Add("data-url", _url);
                else
                    button.Attributes.Add("formaction", _url);
            }
        }

        /// <summary>
        /// Gets or sets if the button is inside a btn group.
        /// </summary>
        internal bool Grouped { get; set; }

        /// <summary>
        /// Gets or sets the button text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the button data-type attribute.
        /// </summary>
        public string DataType { get; set; } = "fw-button";

        private string _icon;
        private string _url;
        private string _confirmMessage;
        private string _confirmTitle;
        private FWElementSize _size = FWElementSize.Regular;
        private FWButtonType _buttonType = FWButtonType.Button;
        private string _behavior;
        private FWStateColors? _color = FWStateColors.Secondary;
    }
}