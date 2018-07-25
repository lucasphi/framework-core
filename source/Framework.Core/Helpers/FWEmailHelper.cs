using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Framework.Core
{
    /// <summary>
    /// Helper class for working with emails.
    /// </summary>
    public static class FWEmailHelper
    {
        /// <summary>
        /// Hides the email name, replacing the letters with a '*' character.
        /// </summary>
        /// <param name="email">The email to obscure.</param>
        /// <param name="index">The replace index.</param>
        /// <param name="offset">The offset before the '@' character.</param>
        /// <returns>The obscured email.</returns>
        public static string Obscure(string email, int index = 2, int offset = 1)
        {
            int innerIndex = index;

            char[] tmpEmail = email.ToCharArray();

            while (tmpEmail[innerIndex] != '@' && tmpEmail[innerIndex + offset] != '@' && innerIndex < tmpEmail.Length)
            {
                tmpEmail[innerIndex] = '*';
                innerIndex++;
            }

            return new string(tmpEmail);
        }

        /// <summary>
        /// Checks for a valid email string.
        /// </summary>
        /// <param name="value">The email string.</param>
        /// <remarks>Code based from .NetCore EmailAddressAttribute.cs</remarks>
        /// <returns>True if it is a valid email. False otherwise.</returns>
        public static bool IsValid(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;

            string valueAsString = value as string;

            Regex regex = CreateRegEx();

            if (regex != null)
            {
                return valueAsString != null && regex.Match(valueAsString).Length > 0;
            }
            return false;
        }

        private static Regex CreateRegEx()
        {
            const string pattern = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$";
            const RegexOptions options = RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture;

            TimeSpan matchTimeout = TimeSpan.FromSeconds(2);

            try
            {
                return new Regex(pattern, options, matchTimeout);
            }
            catch
            {
                return null;
            }
        }
    }
}
