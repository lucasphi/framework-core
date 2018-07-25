using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mvc.Extensions
{
    static class FWViewContextExtensions
    {
        public static void AddRemoteScript(this ViewContext viewContext, string link)
        {
            var scripts = viewContext.ViewData["LayoutLateScripts"] as List<string> ?? new List<string>();
            scripts.Add($"<script type=\"text/javascript\" src=\"{link}\"></script>");
            viewContext.ViewData["LayoutLateScripts"] = scripts;
        }
    }
}
