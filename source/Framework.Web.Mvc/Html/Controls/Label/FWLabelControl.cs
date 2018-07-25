using Framework.Web.Mvc.Html.Elements;

namespace Framework.Web.Mvc.Html.Controls
{
    /// <summary>
    /// Represents a Label control.
    /// </summary>
    public class FWLabelControl : FWControl
    {
        /// <summary>
        /// Creates the control main element.
        /// </summary>
        /// <returns></returns>
        protected override IFWHtmlElement CreateControl()
        {
            FWLabelElement label = new FWLabelElement();
            label.MergeAttributes(Attributes);
            if (!string.IsNullOrWhiteSpace(CustomCss))
                label.AddCssClass(CustomCss);

            label.For = _propertyName;

            if (_isRequired)
            {
                FWSpanElement span = new FWSpanElement();
                span.AddCssClass("required");
                span.Attributes.Add("aria-required", "true");
                span.Add("*");
                label.Add(span);
            }

            if (!string.IsNullOrWhiteSpace(_displayName))
            {
                label.Add(_displayName);

                if (_tooltip != null)
                {
                    label.Add($" <i class=\"fa fa-info-circle\" data-toggle=\"tooltip\" data-skin=\"dark\" title=\"{_tooltip}\"></i>");
                }
            }

            return label;
        }

        /// <summary>
        /// Creates a new Label control.
        /// </summary>
        /// <param name="propertyName">The name of the parent control.</param>
        /// <param name="displayName">The display name of the parent control.</param>
        /// <param name="isRequired">Informs wheather the parent control is required or not.</param>
        /// <param name="tooltip">The label tooltip.</param>
        public FWLabelControl(string propertyName, string displayName, bool isRequired, string tooltip)
        {
            _displayName = displayName;
            _isRequired = isRequired;
            _propertyName = propertyName;
            _tooltip = tooltip;
        }

        private string _propertyName;
        private string _displayName;
        private string _tooltip;
        private bool _isRequired;
    }
}
