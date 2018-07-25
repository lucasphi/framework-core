using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Security.Authentication
{
    /// <summary>
    /// Common interface for framework authentication users.
    /// </summary>
    public interface IFWUser
    {
        /// <summary>
        /// Gets or set the user unique Id.
        /// </summary>
        long Id { get; set; }

        /// <summary>
        /// Gets or sets the user display name.
        /// </summary>
        string FullName { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        string Username { get; set; }

        /// <summary>
        /// Gets or sets the user password.
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// Gets or sets the password salt.
        /// </summary>
        byte[] Salt { get; set; }

        /// <summary>
        /// Get or sets the user authorization tokens.
        /// </summary>
        List<KeyValuePair<string, string>> Tokens { get; set; }
    }
}
