using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Web.Mvc.Html.Elements
{
    /// <summary>
    /// This class can be used to keep reference to a part of an element.
    /// </summary>
    public class FWEmptyElement : FWTagBuilder
    {
        /// <summary>
        /// Creates a new FWEmptyElement element.
        /// </summary>
        public FWEmptyElement()
            : base(string.Empty)
        { }

        /// <summary>
        /// Clears any string previously added and adds the html string to the body of the tag.
        /// </summary>
        /// <param name="html"></param>
        public void AddOnce(string html)
        {
            HtmlList.Clear();
            base.Add(html);
        }
    }
}
