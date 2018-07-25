using Framework.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Model
{
    /// <summary>
    /// Extension methods for enumerators.
    /// </summary>
    public static class FWEnumExtensions
    {
        /// <summary>
        /// Gets the description attribute value for the enum.
        /// </summary>
        /// <param name="enumobj">The enum object.</param>
        /// <returns>The enum description.</returns>
        public static string GetDescription(this Enum enumobj)
        {
            return FWEnumHelper.GetAttributeProperty<string, DescriptionAttribute>(enumobj, "Description");
        }
    }
}
