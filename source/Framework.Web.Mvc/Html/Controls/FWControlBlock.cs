using Framework.Web.Mvc.Html.Elements;
using System.Text;

namespace Framework.Web.Mvc.Html.Controls
{
    /// <summary>
    /// Represents a block control.
    /// </summary>
    public abstract class FWControlBlock : FWControl
    {
        /// <summary>
        /// Gets or sets the control child body.
        /// </summary>
        public string ChildBody { get; set; }

        /// <summary>
        /// Adds a control to the inner html of the current block control.
        /// </summary>
        /// <param name="control">The control object.</param>
        internal virtual void Add(FWControl control)
        {
            StringBuilder sb = new StringBuilder(ChildBody);
            sb.Append(control.ToString());
            ChildBody = sb.ToString();
        }

        /// <summary>
        /// Adds an html to the inner html of the current block control.
        /// </summary>
        /// <param name="html">The tag object.</param>
        internal virtual void Add(string html)
        {
            StringBuilder sb = new StringBuilder(ChildBody);
            sb.Append(html);
            ChildBody = sb.ToString();
        }

        /// <summary>
        /// Adds a tag to the inner html of the current block control.
        /// </summary>
        /// <typeparam name="TTag">The element type.</typeparam>
        /// <param name="tag">The tag object.</param>
        internal virtual TTag Add<TTag>(TTag tag)
            where TTag : FWTagBuilder
        {
            StringBuilder sb = new StringBuilder(ChildBody);
            sb.Append(tag.ToString());
            ChildBody = sb.ToString();
            return tag;
        }

        /// <summary>
        /// Initializes a new instance of the Framework.Web.Mvc.Html.FWControlBlock class.
        /// </summary>
        /// <param name="id">The control Id.</param>
        public FWControlBlock(string id)
            : base(id)
        { }
    }
}
