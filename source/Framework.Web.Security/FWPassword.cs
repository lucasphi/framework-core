using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Framework.Web.Security
{
    /// <summary>
    /// Helper class for passwords.
    /// </summary>
    public static class FWPassword
    {
        /// <summary>
        /// Generates a Random Password
        /// respecting the given strength requirements.
        /// </summary>
        /// <param name="opts">A valid PasswordOptions object
        /// containing the password strength requirements.</param>
        /// <returns>A random password</returns>
        public static string GenerateRandomPassword(FWPasswordOptions opts = null)
        {
            if (opts == null)
                opts = new FWPasswordOptions();

            string[] randomChars = new[] {
                "ABCDEFGHJKLMNPQRSTUVWXYZ",   // uppercase 
                "abcdefghijkmnpqrstuvwxyz",   // lowercase
                "123456789",                  // digits
                "@$?_-"                       // non-alphanumeric
            };

            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }

        /// <summary>
        /// Checks if a password meets the minimum requirements to be valid.
        /// </summary>
        /// <param name="password">The password to validate.</param>
        /// <param name="opts">The password requirements options.</param>
        /// <returns></returns>
        public static (bool Valid, List<string> Errors) ValidatePasswordStrength(string password, FWPasswordOptions opts = null)
        {
            if (opts == null)
                opts = new FWPasswordOptions();

            var errors = new List<string>();
            
            // Checks the password length
            if (password.Length < opts.RequiredLength)
                errors.Add(string.Format(Resources.Resources.FWInvalidPassword_RequiredLength, opts.RequiredLength));

            // Checks the number of unique characters
            if (password.Distinct().Count() < opts.RequiredUniqueChars)
                errors.Add(string.Format(Resources.Resources.FWInvalidPassword_RequiredUniqueChars, opts.RequiredUniqueChars));

            // Checks for digits
            if (opts.RequireDigit && !Regex.IsMatch(password, @"\d"))
                errors.Add(Resources.Resources.FWInvalidPassword_RequireDigit);

            // Checks for lowercase
            if (opts.RequireLowercase && !Regex.IsMatch(password, @"[a-z]"))
                errors.Add(Resources.Resources.FWInvalidPassword_RequireLowercase);

            // Checks for uppercase
            if (opts.RequireUppercase && !Regex.IsMatch(password, @"[A-Z]"))
                errors.Add(Resources.Resources.FWInvalidPassword_RequireUppercase);

            // Checks for nonalphanumeric
            if (opts.RequireNonAlphanumeric && !Regex.IsMatch(password, @"\W|_", RegexOptions.ECMAScript))
                errors.Add(Resources.Resources.FWInvalidPassword_RequireNonAlphanumeric);

            return (errors.Count == 0, errors);
        }
    }
}
