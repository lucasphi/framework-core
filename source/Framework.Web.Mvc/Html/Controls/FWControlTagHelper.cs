using Framework.Web.Mvc.Html.Controls;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Threading.Tasks;

namespace Framework.Web.Mvc.Html
{
    /// <summary>
    /// Framework base Tag Helper.
    /// </summary>
    public abstract class FWControlTagHelper : TagHelper
    {
        /// <summary>
        /// Gets or sets the control Id.
        /// </summary>
        [HtmlAttributeName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the required policy to visualize the control.
        /// </summary>
        [HtmlAttributeName("asp-policy")]
        public string Policy { get; set; }        

        /// <summary>
        /// Creates the framework control.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        /// <returns>The control instance.</returns>
        protected abstract IFWHtmlElement RenderControl(TagHelperContext context, TagHelperOutput output);

        /// <summary>
        /// Gets the request helper.
        /// </summary>
        protected FWRequestContext RequestContext { get; private set; }

        /// <summary>
        /// Gets the tag child content.
        /// </summary>
        protected TagHelperContent ChildContent
        {
            get
            {
                return childContent.Invoke().Result;
            }
        }

        private Func<Task<TagHelperContent>> childContent;

        /// <summary>
        /// Executes the <see cref="TagHelper"/> with the given context and output.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (Policy != null)
            {
                if (!RequestContext.HttpContext.User.HasClaim(f => f.Type == Policy))
                {
                    output.SuppressOutput();
                    return;
                }
            }            

            // Stores the inner content of the control. Uses a function to allow the invocation of the children after the parent.
            childContent = () => output.GetChildContentAsync();

            // Clears the output.
            output.SuppressOutput();

            // Renders the control html.
            var control = RenderControl(context, output);

            if (control != null)
            {
                if (context.IsInnerContent())
                {
                    // If the control is an inner control, remove the tag data-control.
                    control.Attributes.Remove("data-control");
                }

                // Adds the html standard attributes
                foreach (var attr in output.Attributes)
                {
                    if (attr.Name == "class")
                    {
                        control.AddCssClass(attr.Value.ToString());
                    }
                    else
                    {
                        control.Attributes.Add(attr.Name, attr.Value.ToString());
                    }
                }

                output.Content.AppendHtml(control);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWControlTagHelper" />.
        /// </summary>
        /// <param name="urlHelperFactory">The url helper factory.</param>
        /// <param name="actionAccessor">The action accessor.</param>
        protected FWControlTagHelper(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionAccessor)
        {
            RequestContext = new FWRequestContext(urlHelperFactory, actionAccessor);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWControlTagHelper" />.
        /// </summary>
        /// <param name="urlHelperFactory">The url helper factory.</param>
        /// <param name="actionAccessor">The action accessor.</param>
        /// <param name="metadataProvider">The model metadata provider.</param>
        protected FWControlTagHelper(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionAccessor, IModelMetadataProvider metadataProvider)
        {
            RequestContext = new FWRequestContext(urlHelperFactory, actionAccessor, metadataProvider);
        }
    }
}
