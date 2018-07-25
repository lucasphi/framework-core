using Framework.Web.Mvc.Html.Controls;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Framework.Web.Mvc.Html
{
    /// <summary>
    /// Framework button tag helper.
    /// </summary>
    [HtmlTargetElement("button")]
    public class FWButtonTagHelper : FWControlTagHelper
    {
        /// <summary>
        /// Gets or sets the button type.
        /// </summary>
        [HtmlAttributeName("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the button data-url tag.
        /// </summary>
        [HtmlAttributeName("asp-href")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the button default behavior.
        /// </summary>
        [HtmlAttributeName("asp-behavior")]
        public FWButtonBehavior? Behavior { get; set; }

        /// <summary>
        /// Gets or sets the button confirmation title.
        /// </summary>
        [HtmlAttributeName("asp-confirm")]
        public string ConfirmTitle { get; set; }

        /// <summary>
        /// Gets or sets the button confirmation message.
        /// </summary>
        [HtmlAttributeName("asp-confirm-message")]
        public string ConfirmMessage { get; set; }

        /// <summary>
        /// Gets or sets the button color.
        /// </summary>
        [HtmlAttributeName("asp-color")]
        public FWStateColors? Color { get; set; }

        /// <summary>
        /// Gets or sets the button icon.
        /// </summary>
        [HtmlAttributeName("asp-icon")]
        public string Icon { get; set; }

        /// <summary>
        /// Gets or sets the button size.
        /// </summary>
        [HtmlAttributeName("asp-size")]
        public FWElementSize Size { get; set; } = FWElementSize.Regular;

        /// <summary>
        /// Creates the framework control.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        /// <returns>The control instance.</returns>
        protected override IFWHtmlElement RenderControl(TagHelperContext context, TagHelperOutput output)
        {
            var control = new FWButtonControl(Id);

            if ((!ChildContent.IsEmptyOrWhiteSpace))
            {
                control.Text = ChildContent.GetContent();
            }

            control.ButtonType(ResolveButtonType());
            control.Size(Size);

            if (Url != null)
                control.Url(Url);

            if (Behavior.HasValue)
                control.Behavior(Behavior.Value);

            if (ConfirmTitle != null && ConfirmMessage != null)
                control.Confirm(ConfirmTitle, ConfirmMessage);

            if (Color.HasValue)
                control.Color(Color.Value);

            if (Icon != null)
                control.Icon(Icon);

            control.Attributes.Add("data-control", "button");

            if (context.Items.ContainsKey("btngroup"))
            {
                var group = context.Items["btngroup"] as FWButtonGroupControl;
                group.AddButton(control);
            }

            return control;
        }

        private FWButtonType ResolveButtonType()
        {
            switch(Type)
            {
                case "submit":
                    return FWButtonType.Submit;
                case "reset":
                    return FWButtonType.Reset;
            }
            return FWButtonType.Button;
        }

        /// <summary>
        /// Initializes a new instance of the Framework.Web.Mvc.Html.FWButtonTagHelper class.
        /// </summary>
        /// <param name="urlHelperFactory">The url helper factory.</param>
        /// <param name="actionAccessor">The action accessor.</param>
        public FWButtonTagHelper(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionAccessor) 
            : base(urlHelperFactory, actionAccessor)
        { }
    }
}
