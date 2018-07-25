using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;

namespace Framework.Web.Mvc
{
    static class FWHtmlContentExtensions
    {
        public static string GetContent(this IHtmlContent content)
        {
            var writer = new System.IO.StringWriter();
            content.WriteTo(writer, HtmlEncoder.Default);
            return writer.ToString();
        }
    }
}
