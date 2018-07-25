using System;
using System.Collections.Generic;
using System.Text;

namespace System.Security.Claims
{
    /// <summary>
    /// Extension methods for <see cref="ClaimsIdentity" />.
    /// </summary>
    public static class FWClaimsExtension
    {
        /// <summary>
        /// Gets the user Id.
        /// </summary>
        /// <param name="identity">The ClaimsIdentity object.</param>
        /// <returns>The user id.</returns>
        public static T GetId<T>(this ClaimsIdentity identity)
        {
            return (T)Convert.ChangeType(identity.FindFirst(f => f.Type == ClaimTypes.NameIdentifier)?.Value, typeof(T));
        }

        /// <summary>
        /// Gets the user given name.
        /// </summary>
        /// <param name="identity">The ClaimsIdentity object.</param>
        /// <returns>The user given name.</returns>
        public static string GetGivenName(this ClaimsIdentity identity)
        {
            return identity.FindFirst(f => f.Type == ClaimTypes.GivenName)?.Value;
        }
    }
}
