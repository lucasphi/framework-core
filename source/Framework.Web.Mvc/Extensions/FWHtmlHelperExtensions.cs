using Framework.Web.Mvc.Helper;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mvc
{
    /// <summary>
    /// Extension methods for <see cref="IHtmlHelper"/> interface.
    /// </summary>
    public static class FWHtmlHelperExtensions
    {
        /// <summary>
        /// Creates the mvvm databind. Should only be called from partial views.
        /// </summary>
        /// <param name="htmlHelper">The IHtmlHelper.</param>
        /// <returns>The MVVM model binding.</returns>
        public static IHtmlContent Datamodel(this IHtmlHelper htmlHelper)
        {
            var modelname = htmlHelper.ViewData.ModelMetadata.UnderlyingOrModelType.Name;

            var id = Guid.NewGuid();

            var helper = FWDataModelResolverHelper.Serialize(htmlHelper.ViewContext);
            var viewmodel = helper.ToScript(modelname);

            if (!string.IsNullOrWhiteSpace(viewmodel))
            {
                return new HtmlString($"<div id=\"{id}\"></div><script>{viewmodel} fw.bind.add('{modelname}', fw.{modelname}, $('#{id}').closest('[data-type=\"body\"]')[0]);</script>");
            }
            return new HtmlString(string.Empty);
        }
    }
}