using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Model
{
    /// <summary>
    /// Represents a HTML string.
    /// </summary>
    public class FWHtmlString
    {
        /// <summary>
        /// Converts a FWHtmlString to a string.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator string(FWHtmlString value)
        {
            return value.ToString();
        }

        /// <summary>
        /// Converts a string to a FWHtmlString object.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator FWHtmlString(string value)
        {
            return new FWHtmlString(value);
        }

        /// <summary>
        /// Overrides the default ToString method.
        /// </summary>
        /// <returns>The HTML string.</returns>
        public override string ToString()
        {
            return _html;
        }

        /// <summary>
        /// Initializes a new instance of the Framework.Model.FWHtmlString class.
        /// </summary>
        /// <param name="html">The html string content.</param>
        public FWHtmlString(string html)
        {
            _html = html;
        }
        /// <summary>
        /// Initializes a new instance of the Framework.Model.FWHtmlString class.
        /// </summary>
        public FWHtmlString()
        {

        }

        private string _html;
    }
}
