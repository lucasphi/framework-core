using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mvc.Html.Controls
{
    /// <summary>
    /// Common interface for control color.
    /// </summary>
    public interface IFWStateControl
    {
        /// <summary>
        /// Sets the control state color.
        /// </summary>
        /// <param name="color">The control color.</param>
        void Color(FWStateColors color);

        /// <summary>
        /// Defines the display size of the control.
        /// </summary>
        /// <param name="size">The size enum.</param>
        void Size(FWElementSize size);
    }
}
