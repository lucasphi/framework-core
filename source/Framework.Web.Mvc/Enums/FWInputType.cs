using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Web.Mvc
{
    /// <summary>
    /// Enumerates the input types.
    /// </summary>
    public enum FWInputType
    {
        /// <summary>
        /// Textbox input.
        /// </summary>
        Textbox,

        /// <summary>
        /// Password input.
        /// </summary>
        Password,

        /// <summary>
        /// Checkbox input.
        /// </summary>
        Checkbox,

        /// <summary>
        /// Radio input.
        /// </summary>
        Radio,

        /// <summary>
        /// Hidden input.
        /// </summary>
        Hidden,

        /// <summary>
        /// Date input.
        /// </summary>
        Date,

        /// <summary>
        /// Datetime input.
        /// </summary>
        Datetime
    }
}
