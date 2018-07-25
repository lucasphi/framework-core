using Framework.Web.Mvc.Html.Controls;
using Framework.Web.Mvc.Html.Elements;
using Microsoft.AspNetCore.Mvc;

namespace Framework.Web.Mvc.Html
{
    /// <summary>
    /// Represents a Chart control.
    /// </summary>
    public class FWChartControl : FWControl
    {
        /// <summary>
        /// Creates the control main element.
        /// </summary>
        /// <returns>The control IFWHtmlElement interface.</returns>
        protected override IFWHtmlElement CreateControl()
        {
            var element = new FWDivElement()
            {
                Id = Id,
                DataType = "fw-chart"
            };

            element.MergeAttributes(Attributes);
            if (!string.IsNullOrWhiteSpace(CustomCss))
                element.AddCssClass(CustomCss);

            element.Attributes.Add("data-url", _requestContext.Url.Action("Load", "FWChart", new { area = string.Empty }));

            var canvas = new FWTagBuilder("canvas");
            element.Add(canvas);

            return element;
        }

        /// <summary>
        /// Creates a new <see cref="FWChartControl"/> control.
        /// </summary>
        /// <param name="requestContext">The request helper.</param>
        /// <param name="id">The control id.</param>
        public FWChartControl(FWRequestContext requestContext, string id) 
            : base(id)
        {
            _requestContext = requestContext;
        }

        private FWRequestContext _requestContext;
    }
}
