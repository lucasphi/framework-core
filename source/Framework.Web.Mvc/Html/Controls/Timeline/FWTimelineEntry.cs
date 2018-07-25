using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mvc.Html
{
    /// <summary>
    /// Represents a timeline item.
    /// </summary>
    public class FWTimelineEntry
    {
        /// <summary>
        /// Gets or sets the entry date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the entry color state.
        /// </summary>
        public FWStateColors State { get; set; }
        
        /// <summary>
        /// Gets or sets the primary entry text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the secondary text entry.
        /// </summary>
        public string Source { get; set; } = " ";

        /// <summary>
        /// Gets or sets the entry url.
        /// </summary>
        public Func<IUrlHelper, string> Url { get; set; }
    }
}
