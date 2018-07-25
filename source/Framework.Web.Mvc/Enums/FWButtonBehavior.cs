using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Web.Mvc
{
    /// <summary>
    /// Enumerates the button behaviors.
    /// </summary>
    public enum FWButtonBehavior
    {
        /// <summary>
        /// Executes an action behavior.
        /// </summary>
        Action,

        /// <summary>
        /// Add behavior.
        /// </summary>
        Add,

        /// <summary>
        /// Cancel behavior.
        /// </summary>
        Cancel,

        /// <summary>
        /// Confirm and editing behavior.
        /// </summary>
        Confirm,

        /// <summary>
        /// Exclude behavior.
        /// </summary>
        Exclude,

        /// <summary>
        /// Edit behavior.
        /// </summary>
        Edit,

        /// <summary>
        /// Saves the target form behavior.
        /// </summary>
        Save,

        /// <summary>
        /// Search behavior.
        /// </summary>
        Search,

        /// <summary>
        /// Redirects to a custom Url.
        /// </summary>
        Redirect,

        /// <summary>
        /// Resets the closest form.
        /// </summary>
        Reset
    }
}
