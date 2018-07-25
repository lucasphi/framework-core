using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Security
{
    /// <summary>
    /// Password options POCO.
    /// </summary>
    public class FWPasswordOptions
    {
        /// <summary>
        /// Gets or sets the required length of the password.
        /// </summary>
        public int RequiredLength { get; set; } = 8;

        /// <summary>
        /// Gets or sets the mininum number of unique characters.
        /// </summary>
        public int RequiredUniqueChars { get; set; } = 4;

        /// <summary>
        /// Gets or sets if the password requires at least one digit.
        /// </summary>
        public bool RequireDigit { get; set; } = true;

        /// <summary>
        /// Gets or sets if the password has lower case characters.
        /// </summary>
        public bool RequireLowercase { get; set; } = true;

        /// <summary>
        /// Gets or sets if the password has upper case characters.
        /// </summary>
        public bool RequireUppercase { get; set; } = true;

        /// <summary>
        /// Gets or sets if the password has non alphanumeric characters.
        /// </summary>
        public bool RequireNonAlphanumeric { get; set; } = true;
    }
}
