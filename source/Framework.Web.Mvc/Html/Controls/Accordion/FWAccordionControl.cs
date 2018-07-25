using Framework.Web.Mvc.Html.Controls.Accordion;
using Framework.Web.Mvc.Html.Elements;
using System.Collections.Generic;

namespace Framework.Web.Mvc.Html.Controls
{
    /// <summary>
    /// Represents an Accordion control.
    /// </summary>
    public class FWAccordionControl : FWControlBlock
    {
        /// <summary>
        /// Creates the control main element.
        /// </summary>
        /// <returns>The control IFWHtmlElement interface.</returns>
        protected override IFWHtmlElement CreateControl()
        {
            var element = new FWDivElement()
            {
                Id = Id,
                DataType = "fw-accordion"
            };
            element.AddCssClass("m-accordion m-accordion--bordered m-accordion--solid");
            element.Attributes.Add("role", "tablist");

            element.MergeAttributes(Attributes);
            if (!string.IsNullOrWhiteSpace(CustomCss))
                element.AddCssClass(CustomCss);

            int accNumber = 1;
            foreach (var child in _children)
            {
                var item = new FWDivElement();
                item.AddCssClass("m-accordion__item");

                // Creates the item header content
                item.Add(CreateItemHeader(child, accNumber));

                // Creates the item body content
                item.Add(CreateItemBody(child, accNumber));

                element.Add(item);

                accNumber++;
            }

            return element;
        }

        private FWDivElement CreateItemHeader(FWAccordionItem item, int itemNumber)
        {
            var accItem = new FWDivElement
            {
                Id = $"{item.Id}_head",
                DataType = "fw-accordion-item"
            };
            accItem.AddCssClass("m-accordion__item-head collapsed");
            accItem.Attributes.Add("role", "tab");
            accItem.Attributes.Add("href", $"#{item.Id}_body");
            accItem.Attributes.Add("data-toggle", "collapse");
            accItem.Attributes.Add("aria-expanded", "false");            

            // Adds the header icon
            if (item.Icon != null)
            {
                var iconspan = new FWSpanElement();
                iconspan.AddCssClass("m-accordion__item-icon");
                iconspan.Add($"<i class=\"{item.Icon}\"></i>");
                accItem.Add(iconspan);
            }

            // Adds the header title
            var itemTitle = accItem.Add(new FWSpanElement());
            itemTitle.AddCssClass("m-accordion__item-title");
            itemTitle.Add(item.Title);

            // Adds the header status (open-closed) icon
            var statusIcon = accItem.Add(new FWSpanElement());
            statusIcon.Add("<i class=\"fa fa-chevron-down\"></i>");
            statusIcon.Add("<i class=\"fa fa-chevron-up\"></i>");

            if (item.IsLazyLoaded)
            {
                accItem.Attributes.Add("data-url", item.Url);
                accItem.Attributes.Add("data-targetid", $"{item.Id}_body");
                accItem.Attributes.Add("data-cache", item.Cached.ToString().ToLower());
            }

            return accItem;
        }

        private FWDivElement CreateItemBody(FWAccordionItem item, int accNumber)
        {
            var accItem = new FWDivElement
            {
                Id = $"{item.Id}_body"
            };
            accItem.AddCssClass("m-accordion__item-body collapse");
            accItem.Attributes.Add("role", "tabpanel");
            accItem.Attributes.Add("aria-labelledby", $"{item.Id}_head");
            accItem.Attributes.Add("data-parent", $"#{item.Id}");

            var itemContent = accItem.Add(new FWDivElement());
            itemContent.AddCssClass("m-accordion__item-content");

            if (!item.IsLazyLoaded)
            {
                itemContent.Add(item.Content);
            }

            return accItem;
        }

        /// <summary>
        /// Adds a child item.
        /// </summary>
        /// <param name="title">The item title.</param>
        /// <param name="content">The item html content.</param>
        /// <param name="icon">The header icon.</param>
        /// <returns>The fluent configurator object.</returns>
        public FWAccordionControl AddContentItem(string title, string content, string icon)
        {
            var childNumber = _children.Count + 1;
            FWAccordionItem item = FWAccordionItem.CreateContentItem(title, content, $"{Id}_{childNumber}");
            item.Icon = icon;
            _children.Add(item);

            return this;
        }

        /// <summary>
        /// Adds a child item.
        /// </summary>
        /// <param name="title">The item title.</param>
        /// <param name="url">The url to load the item content.</param>
        /// <param name="cached">Informs if the item content should be cached.</param>
        /// <param name="icon">The header icon.</param>
        /// <returns>The fluent configurator object.</returns>
        public FWAccordionControl AddLazyItem(string title, string url, bool cached, string icon)
        {
            var childNumber = _children.Count + 1;
            FWAccordionItem item = FWAccordionItem.CreateLazyItem(title, url, $"{Id}_{childNumber}", cached);
            item.Icon = icon;
            _children.Add(item);

            return this;
        }

        /// <summary>
        /// Creates a new Accordion control.
        /// </summary>
        /// <param name="id">The control id.</param>
        public FWAccordionControl(string id)
            : base(id)
        { }

        private List<FWAccordionItem> _children = new List<FWAccordionItem>();
    }
}
