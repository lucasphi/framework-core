using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mvc
{
    /// <summary>
    /// Razor extension interface.
    /// </summary>
    public interface IFWRazorInjections
    {
        /// <summary>
        /// In a Razor layout page, renders the main menu.
        /// </summary>
        /// <returns>The main menu html.</returns>
        IHtmlContent RenderMenu(IUrlHelper urlHelper);

        /// <summary>
        /// In a Razor layout page, renders the user menu.
        /// </summary>
        /// <param name="urlHelper">The current UrlHelper.</param>
        /// <returns>The user menu html.</returns>
        IHtmlContent RenderTopMenu(IUrlHelper urlHelper);

        /// <summary>
        /// In a Razor layoutpage, renders the last request alerts.
        /// </summary>
        /// <returns>The alerts.</returns>
        IHtmlContent RenderAlerts();
    }
}
