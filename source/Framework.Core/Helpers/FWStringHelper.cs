using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Framework.Core
{
    /// <summary>
    /// Helper to support working with strings.
    /// </summary>
    public static class FWStringHelper
    {
        private const string _hexDigits = "0123456789ABCDEF";
        private const string _maskHex = "{0:X2}";

        /// <summary>
        /// Convert a byte array to a hexadecimal string.
        /// </summary>
        /// <param name="bytes">The byte array.</param>
        /// <returns>The converted string.</returns>
        public static string BytesToHexString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
            {
                sb.AppendFormat(_maskHex, b);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Convert an hexadecimal string to a byte array.
        /// </summary>
        /// <param name="str">The hex string. For example, "FF00EE11"</param>
        /// <returns>An array of bytes</returns>
        public static byte[] HexStringToBytes(string str)
        {
            // Determine how many bytes there are.     
            byte[] bytes = new byte[str.Length >> 1];
            for (int i = 0; i < str.Length; i += 2)
            {
                int highDigit = _hexDigits.IndexOf(char.ToUpperInvariant(str[i]));
                int lowDigit = _hexDigits.IndexOf(char.ToUpperInvariant(str[i + 1]));
                if (highDigit == -1 || lowDigit == -1)
                {
                    throw new FWArgumentException(str);
                }
                bytes[i >> 1] = (byte)((highDigit << 4) | lowDigit);
            }
            return bytes;
        }

        /// <summary>
        /// Returns a part of a string using regex to find it.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="regex">The regex pattern.</param>
        /// <returns>The string match if found.</returns>
        public static string RegexFind(string str, string regex)
        {
            var match = Regex.Match(str, regex);
            if (match.Success)
                return match.Groups[1].Value;

            return null;
        }
    }
}
