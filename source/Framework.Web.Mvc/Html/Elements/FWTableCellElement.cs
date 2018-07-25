using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Web.Mvc.Html.Elements
{
    /// <summary>
    /// Represents a HTML table cell element.
    /// </summary>
    public class FWTableCellElement : FWTagBuilder
    {   
        /// <summary>
        /// Gets or sets the value of the cell.
        /// </summary>
        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                this.Add(value);
                _value = value;
            }
        }

        /// <summary>
        /// Creates a new HTML table cell element.
        /// </summary>
        /// <param name="header">The current FWHtmlHelper.</param>
        public FWTableCellElement(bool header)
            : base(!header ? "td" : "th")
        { }

        private string _value;
    }
}
