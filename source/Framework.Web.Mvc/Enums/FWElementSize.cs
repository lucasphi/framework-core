using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Web.Mvc
{
    /// <summary>
    /// Possible sizes for a control.
    /// </summary>
    public enum FWElementSize
    {
        /// <summary>
        /// Small.
        /// </summary>
        [Description("sm")]
        Small,

        /// <summary>
        /// Regular.
        /// </summary>
        [Description("md")]
        Regular,

        /// <summary>
        /// Large
        /// </summary>
        [Description("lg")]
        Large
    }
}
