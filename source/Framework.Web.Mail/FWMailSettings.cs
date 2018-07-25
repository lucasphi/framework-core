using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mail
{
    /// <summary>
    /// POCO for the application configuration settings.
    /// </summary>
    public class FWMailSettings
    {
        /// <summary>
        /// Gets or sets the host address.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the host port.
        /// </summary>
        public int Port { get; set; } = 587;

        /// <summary>
        /// Gets or sets the server user.
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Gets or sets the server user password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets if the password is encrypted.
        /// </summary>
        public bool SecurePassword { get; set; }

        /// <summary>
        /// Gets or sets if the mail will use SSL.
        /// </summary>
        public bool EnableSsl { get; set; } = true;

        /// <summary>
        /// Gets or sets if the services allows testing.
        /// </summary>
        public bool Debug { get; set; }

        /// <summary>
        /// Gets or sets the 'from address'.
        /// </summary>
        public string From
        {
            get
            {
                if (_from == null)
                    return User;
                return _from;
            }
            set
            {
                _from = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'from address' display name.
        /// </summary>
        public string FromDisplayName { get; set; }

        private string _from;
    }
}
