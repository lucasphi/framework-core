using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Framework.Web.Security.Authentication
{
    /// <summary>
    /// Authentication provider interface.
    /// </summary>
    public interface IFWAuthProvider
    {
        /// <summary>
        /// Attemps to authenticate a user.
        /// </summary>
        /// <param name="user">The user object.</param>
        /// <param name="inputPassword">The input password to validate.</param>
        /// <returns>The ClaimsIdentity object for the user.</returns>
        ClaimsIdentity AuthenticateUser(IFWUser user, string inputPassword);

        /// <summary>
        /// Gets or sets the type authentication used.
        /// </summary>
        string AuthenticationType { get; set; }
    }
}
