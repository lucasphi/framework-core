using Framework.Web.Mvc.Html.Elements;
using System;

namespace Framework.Web.Mvc.Html.Controls
{
    /// <summary>
    /// Represents a recaptcha control.
    /// </summary>
    public class FWRecaptchaControl : FWControl
    {
        /// <summary>
        /// Creates the control main element.
        /// </summary>
        /// <returns>The control IFWHtmlElement interface.</returns>
        protected override IFWHtmlElement CreateControl()
        {
            var element = new FWDivElement();
            element.AddCssClass("g-recaptcha");
            element.Attributes.Add("data-sitekey", _sitekey);

            return element;
        }

        /// <summary>
        /// Creates a new recaptcha.
        /// </summary>
        public FWRecaptchaControl(string sitekey)
            : base(Guid.NewGuid().ToString())
        {
            _sitekey = sitekey;
        }

        private string _sitekey;
    }
}
