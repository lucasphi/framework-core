using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core
{
    /// <summary>
    /// Extension class for System.Text.
    /// </summary>
    public static class FWTextExtensions
    {
        /// <summary>
        /// Strips the string to a max length value, adding three dots at the end
        /// </summary>
        /// <param name="inputString">Input string to validate</param>
        /// <param name="maxLength">String maximum length</param>
        /// <returns></returns>
        public static string Strip(this string inputString, int maxLength)
        {
            if (inputString.Length > maxLength)
            {
                return string.Format("{0}...", inputString.Substring(0, maxLength - 3));
            }
            return inputString;
        }
    }
}
