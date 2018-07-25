using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Web.Mvc
{
    /// <summary>
    /// Enumerates the possible state colors.
    /// </summary>
    public enum FWStateColors
    {
        /// <summary>
        /// The success state color.
        /// </summary>
        [Description("success")]
        Success,

        /// <summary>
        /// The info state color.
        /// </summary>
        [Description("info")]
        Info,

        /// <summary>
        /// The warning state color.
        /// </summary>
        [Description("warning")]
        Warning,

        /// <summary>
        /// The danger state color.
        /// </summary>
        [Description("danger")]
        Danger,

        /// <summary>
        /// The primary state color.
        /// </summary>
        [Description("primary")]
        Primary,

        /// <summary>
        /// The secondary state color.
        /// </summary>
        [Description("secondary")]
        Secondary,

        /// <summary>
        /// The accent state color.
        /// </summary>
        [Description("accent")]
        Accent,

        /// <summary>
        /// The metal state color.
        /// </summary>
        [Description("metal")]
        Metal,

        /// <summary>
        /// The light state color.
        /// </summary>
        [Description("light")]
        Light
    }
}
