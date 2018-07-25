using Framework.Core;
using Framework.Web.Mvc.Extensions;
using Framework.Web.Mvc.Html.Controls;
using Framework.Web.Security.Util;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace Framework.Web.Mvc.Html
{
    /// <summary>
    /// Framework recaptcha tag helper.
    /// </summary>
    [HtmlTargetElement("recaptcha")]
    public class FWRecaptchaTagHelper : FWControlTagHelper
    {
        /// <summary>
        /// Creates the framework control.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        /// <returns>The control instance.</returns>
        protected override IFWHtmlElement RenderControl(TagHelperContext context, TagHelperOutput output)
        {
            var cacheKey = $"LoginAttempt_{RequestContext.Url.ActionContext.HttpContext.Connection.RemoteIpAddress}";

            if (_memoryCache.GetOrCreate(cacheKey, entry => 0) >= _options.RequiredAttempts)
            {   
                ViewContext.AddRemoteScript(_options.ScriptAddress);

                var control = new FWRecaptchaControl(_options.SiteKey);
                return control;
            }
            return FWControl.EmptyControl;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWRecaptchaTagHelper"/> class.
        /// </summary>
        /// <param name="configuration">The app configuration object.</param>
        /// <param name="memoryCache">The memory cache object.</param>
        /// <param name="urlHelperFactory">The url helper factory.</param>
        /// <param name="actionAccessor">The action accessor.</param>
        public FWRecaptchaTagHelper(IConfiguration configuration, IMemoryCache memoryCache, IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionAccessor) 
            : base(urlHelperFactory, actionAccessor)
        {
            _memoryCache = memoryCache;

            var options = new FWRecaptchaOptions();
            configuration.Bind("Recaptcha", options);
            if (options.SiteKey == null)
                throw new FWSettingsException("Recaptcha:SiteKey");

            _options = options;
        }

        /// <summary>
        /// Gets or sets the view context.
        /// </summary>
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        private FWRecaptchaOptions _options;

        private IMemoryCache _memoryCache;
    }
}
