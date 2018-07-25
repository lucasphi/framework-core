using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mvc
{
    /// <summary>
    /// Enumerates the devices sizes. Sizes over 768px affects large sizes unless an explicit size is defined.
    /// </summary>
    public enum FWDevice
    {
        /// <summary>
        /// Extra small devices, such as phones (less then 768px)
        /// </summary>
        Phone,

        /// <summary>
        /// Small devices, such as tablets (over 768px)
        /// </summary>
        Tablet,

        /// <summary>
        /// Medium devices, such as desktops (over 992px)
        /// </summary>
        Desktop,

        /// <summary>
        /// Large devices, such as desktops with large screens (over 1200px)
        /// </summary>
        LargeDesktop
    }
}
