using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Web.Mvc
{
    /// <summary>
    /// Application static information.
    /// </summary>
    public static class FWApp
    {
        /// <summary>
        /// Gets the application default namespace.
        /// </summary>
        public static string DefaultNamespace { get; internal set; }

        /// <summary>
        /// Gets if the application uses authentication.
        /// </summary>
        public static bool IsSecure { get; internal set; }

        /// <summary>
        /// Gets the list of available cultures.
        /// </summary>
        public static IEnumerable<CultureInfo> Cultures { get; internal set; }

        /// <summary>
        /// Gets the application web root path.
        /// </summary>
        public static string WebRootPath { get; internal set; }

        internal static string GetCulture(string culture)
        {
            foreach (var cultureInfo in Cultures)
            {
                if (cultureInfo.Name.ToLower().Contains(culture.ToLower()))
                    return cultureInfo.Name;
            }

            return Cultures.First().Name;
        }
    }
}
