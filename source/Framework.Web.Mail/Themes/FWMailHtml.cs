using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mail.Themes
{
    /// <summary>
    /// Helper class to create a Html email.
    /// </summary>
    public class FWMailHtml
    {
        /// <summary>
        /// Gets or sets the email message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the email message header.
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWMailHtml" />.
        /// </summary>
        /// <param name="message">The email message.</param>
        public FWMailHtml(string message)
        {
            Message = message;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWMailHtml" />.
        /// </summary>
        /// <param name="message">The email message.</param>
        /// <param name="header">The email message header.</param>
        public FWMailHtml(string message, string header)
            : this(message)
        {   
            Header = header;
        }

        /// <summary>
        /// Wraps the message within a Html theme.
        /// </summary>
        /// <param name="theme">The Html theme.</param>
        /// <returns>The Html email.</returns>
        public string CreateHtmlMail(FWMailThemes theme = FWMailThemes.Default)
        {
            switch(theme)
            {
                case FWMailThemes.Default:
                    return string.Format(FWDefaultTheme.THEME, Header, Message);
            }
            return null;
        }
    }
}
