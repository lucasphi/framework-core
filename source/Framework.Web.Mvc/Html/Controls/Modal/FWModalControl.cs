using Framework.Model;
using Framework.Web.Mvc.Html.Elements;
using Framework.Web.Mvc.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mvc.Html.Controls
{
    /// <summary>
    /// Represents a Modal control.
    /// </summary>
    public class FWModalControl : FWControlBlock
    {
        /// <summary>
        /// Creates the control main element.
        /// </summary>
        /// <returns>The control IFWHtmlElement interface.</returns>
        protected override IFWHtmlElement CreateControl()
        {
            var modal = new FWDivElement();
            modal.MergeAttributes(Attributes);
            if (!string.IsNullOrWhiteSpace(CustomCss))
                modal.AddCssClass(CustomCss);

            modal.Id = Id;
            modal.DataType = "fw-modal";
            modal.AddCssClass("modal fade");
            modal.Attributes.Add("role", "dialog");
            modal.Attributes.Add("aria-labelledby", $"{Id}Label");

            var dialog = modal.Add(new FWDivElement());
            dialog.AddCssClass($"modal-dialog modal-dialog-centered modal-{_size.GetDescription()}");
            dialog.Attributes.Add("role", "document");

            FWTagBuilder content = dialog.Add(new FWDivElement());
            content.AddCssClass("modal-content");

            content.Add(CreateModalHeader());

            var holder = content;

            if (FormUrl != null)
            {
                holder = content.Add(new FWTagBuilder("form"));
                holder.Attributes.Add("method", "post");
                holder.Attributes.Add("action", FormUrl);
            }

            holder.Add(CreateModalBody());

            if (Footer != null)
            {
                holder.Add(CreateModalFooter());
            }

            return modal;
        }

        private FWDivElement CreateModalHeader()
        {
            var header = new FWDivElement();
            header.AddCssClass("modal-header");

            var title = header.Add(new FWTagBuilder("h5"));
            title.Id = $"{Id}Label";
            title.Add(_title);
            title.AddCssClass("modal-title");

            var closeBtn = header.Add(new FWButtonElement());
            closeBtn.AddCssClass("btn m-btn m-btn--icon m-btn--icon-only fw-btn-close");
            closeBtn.Attributes.Add("data-dismiss", "modal");
            closeBtn.Attributes.Add("aria-label", ViewResources.Btn_Close);
            //closeBtn.Add("<span aria-hidden=\"true\">×</span>");            

            return header;
        }

        private FWDivElement CreateModalBody()
        {
            var body = new FWDivElement();
            body.AddCssClass("modal-body");
            body.Attributes.Add("data-type", "body");

            if (string.IsNullOrWhiteSpace(ContentUrl))
            {                
                body.Add(Body);
            }
            else
            {
                body.Attributes.Add("data-url", ContentUrl);
            }

            return body;
        }

        private FWDivElement CreateModalFooter()
        {
            var footer = new FWDivElement();
            footer.AddCssClass("modal-footer");
            footer.Attributes.Add("data-type", "footer");

            footer.Add(Footer);

            return footer;
        }

        /// <summary>
        /// Creates a new <see cref="FWModalControl"/>.
        /// </summary>
        /// <param name="id">The control id.</param>
        /// <param name="title">The modal title.</param>
        /// <param name="size">The modal size.</param>
        public FWModalControl(string id, string title, FWElementSize size) : base(id)
        {
            _title = title;
            _size = size;
        }

        private string _title;
        private FWElementSize _size;

        /// <summary>
        /// Gets or sets the control body content.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the control footer content.
        /// </summary>
        public string Footer { get; set; }

        /// <summary>
        /// Gets or sets the url to load the control body.
        /// </summary>
        public string ContentUrl { get; set; }

        /// <summary>
        /// Gets or sets the url for the modal form.
        /// </summary>
        public string FormUrl { get; set; }
    }
}
