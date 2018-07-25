using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mail
{
    /// <summary>
    /// Represents an email message.
    /// </summary>
    public class FWMail
    {
        /// <summary>
        /// Gets or sets if the email is a test.
        /// </summary>
        public bool Debug { get; set; }

        /// <summary>
        /// Gets or sets the email subject.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the email message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets if the email is plain text or Html.
        /// </summary>
        public bool IsHtml { get; set; }

        /// <summary>
        /// Gets the list of the email destinies.
        /// </summary>
        public List<FWMailTo> To { get; private set; } = new List<FWMailTo>();
    }

    /// <summary>
    /// Represents an email destiny.
    /// </summary>
    public class FWMailTo
    {
        /// <summary>
        /// Gets the email address.
        /// </summary>
        public string Address { get; private set; }

        /// <summary>
        /// Gets the email display name.
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWMailTo" />.
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        public FWMailTo(string emailAddress)
        {
            Address = emailAddress ?? throw new ArgumentNullException("emailAddress");
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="FWMailTo" />.
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        /// <param name="emailDisplayName">The email display name.</param>
        public FWMailTo(string emailAddress, string emailDisplayName)
            : this(emailAddress)
        {
            DisplayName = emailDisplayName ?? throw new ArgumentNullException("emailDisplayName");
        }
    }
}
