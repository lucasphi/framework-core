using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Web.Mvc
{
    /// <summary>
    /// Enumerates the layout options for checkboxes and radiobuttons.
    /// </summary>
    public enum FWOptionLayout
    {
        /// <summary>
        /// The Minimal layout.
        /// </summary>
        [Description("minimal")]
        Minimal,

        /// <summary>
        /// The Square layout.
        /// </summary>
        [Description("square")]
        Square,

        /// <summary>
        /// The Flat layout.
        /// </summary>
        [Description("flat")]
        Flat,

        /// <summary>
        /// The Line layout.
        /// </summary>
        [Description("line")]
        Line
    }
}
