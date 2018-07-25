using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.Helpers
{
    /// <summary>
    /// Helper class to work with globalization.
    /// </summary>
    public static class FWGlobalizationHelper
    {
        /// <summary>
        /// Gets the current locale name.
        /// </summary>
        public static string CurrentLocaleName
        {
            get
            {
                return CultureInfo.CurrentCulture.Name;
            }
        }

        /// <summary>
        /// Gets the current date format.
        /// </summary>
        public static string CurrentDateFormat
        {
            get
            {
                return CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
            }
        }

        /// <summary>
        /// Sets the current context culture.
        /// </summary>
        /// <param name="culture">The culture.</param>
        public static void SetCulture(CultureInfo culture)
        {
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
        }

        /// <summary>
        /// Sets the default culture for threads in the current application domain.
        /// </summary>
        /// <param name="culture">The culture.</param>
        public static void SetThreadCulture(CultureInfo culture)
        {
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }
    }
}
