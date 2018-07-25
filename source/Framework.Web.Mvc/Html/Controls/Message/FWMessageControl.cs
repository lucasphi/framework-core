using Framework.Web.Mvc.Html.Elements;

namespace Framework.Web.Mvc.Html.Controls
{
    /// <summary>
    /// Represents an button control.
    /// </summary>
    public class FWMessageControl : FWControl
    {
        /// <summary>
        /// Creates the control main element.
        /// </summary>
        /// <returns>The control IFWHtmlElement interface.</returns>
        protected override IFWHtmlElement CreateControl()
        {
            var element = new FWDivElement()
            {
                DataType = "fw-message"
            };
            element.Attributes.Add("role", "alert");
            element.AddCssClass($"m-alert m-alert--icon m-alert--icon-solid m-alert--outline alert alert-dismissible fade show {_typeCssClass}");

            var iconDiv = new FWDivElement();
            iconDiv.AddCssClass("m-alert__icon");
            var icon = iconDiv.Add(new FWTagBuilder("i"));
            icon.AddCssClass($"fa {_icon}");
            element.Add(iconDiv);

            var textDiv = new FWDivElement();
            textDiv.AddCssClass("m-alert__text");
            textDiv.Add(_message);
            element.Add(textDiv);

            var buttonDiv = new FWDivElement();
            buttonDiv.AddCssClass("m-alert__close");

            if (_allowClose)
            {
                var button = new FWButtonElement();
                button.Attributes.Add("data-dismiss", "alert");
                button.AddCssClass("btn m-btn m-btn--icon m-btn--icon-only fw-btn-close");
                //button.Add("×");
                buttonDiv.Add(button);
            }

            element.Add(buttonDiv);

            return element;
        }

        /// <summary>
        /// Defines if the message can be closed or not.
        /// </summary>
        /// <param name="allowClose">True to allow closing. False otherwise.</param>
        /// <returns>The fluent configuration object.</returns>
        public FWMessageControl AllowClose(bool allowClose = true)
        {
            _allowClose = allowClose;
            return this;
        }

        /// <summary>
        /// Creates a new Alert.
        /// </summary>
        /// <param name="message">The alert message.</param>
        /// <param name="type">The alert type.</param>
        public FWMessageControl(string message, FWMessageType type)
        {
            _message = message;
            _type = type;

            switch(type)
            {
                case FWMessageType.Success:
                    _typeCssClass = "alert-success";
                    _icon = "fa-check";
                    break;
                case FWMessageType.Error:
                    _typeCssClass = "alert-danger";
                    _icon = "fa-times-circle";
                    break;
                case FWMessageType.Info:
                    _typeCssClass = "alert-info";
                    _icon = "fa-info";
                    break;
                case FWMessageType.Warn:
                    _typeCssClass = "alert-warning";
                    _icon = "fa-warning";
                    break;
            }
        }

        private bool _allowClose = true;
        private string _message;
        private FWMessageType _type;
        private string _typeCssClass;
        private string _icon;
    }
}
