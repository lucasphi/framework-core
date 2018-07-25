using Framework.Web.Mvc.Helper;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mvc.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="RazorPage"/>
    /// </summary>
    public static class FWRazorPageExtensions
    {
        /// <summary>
        /// In layout pages, renders the content of a scripts section.
        /// </summary>
        /// <param name="razorPage">The RazorPage object.</param>
        /// <returns></returns>
        public static HtmlString RenderScripts(this RazorPage razorPage)
        {
            try
            {
                // Checks to see if the method is being called from _Layout pages.
                if (razorPage.PreviousSectionWriters != null)
                {
                    razorPage.RenderSection("scripts", false);
                }
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(Resources.Resources.FWRenderScripts_MethodCannotBeCalled, ex);
            }

            if (razorPage.ViewContext.ViewData["LayoutLateScripts"] is List<string> scripts)
            {
                return new HtmlString(string.Join(' ', scripts));
            }

            return HtmlString.Empty;
        }

        /// <summary>
        /// In a Razor layoutpage, renders the generated mvvm model.
        /// </summary>
        /// <returns>The mvvm model.</returns>
        public static IHtmlContent RenderDataModel(this RazorPage razorPage)
        {
            var modelname = "model";

            var helper = FWDataModelResolverHelper.Serialize(razorPage.ViewContext);
            var viewmodel = helper.ToScript(modelname);

            if (!string.IsNullOrWhiteSpace(viewmodel))
            {
                return new HtmlString($"<script>{viewmodel} fw.bind.add('{modelname}', fw.{modelname}, document.querySelectorAll('[data-type=\"content\"]')[0]);</script>");
            }
            return new HtmlString(string.Empty);
        }
    }
}