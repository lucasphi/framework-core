using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Security.Util
{
    /// <summary>
    /// Recaptcha configuration options.
    /// </summary>
    public class FWRecaptchaOptions
    {
        /// <summary>
        /// Gets or sets the site key.
        /// </summary>
        public string SiteKey { get; set; }

        /// <summary>
        /// Gets or sets the comunication key.
        /// </summary>
        public string PrivateKey { get; set; }

        /// <summary>
        /// Gets or sets the api endpoint.
        /// </summary>
        public string ApiEndpoint { get; set; } = "https://www.google.com/recaptcha/api/siteverify";

        /// <summary>
        /// Gets or sets the javascript address.
        /// </summary>
        public string ScriptAddress { get; set; } = "https://www.google.com/recaptcha/api.js";

        /// <summary>
        /// Gets or sets the number of failed attempts to login before displaying the captcha.
        /// </summary>
        public int RequiredAttempts { get; set; }
    }
}
