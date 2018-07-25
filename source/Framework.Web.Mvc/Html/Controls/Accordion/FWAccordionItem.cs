using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mvc.Html.Controls.Accordion
{
    internal class FWAccordionItem
    {
        public string Id { get; private set; }

        public string Title { get; private set; }

        public bool IsLazyLoaded { get; private set; }

        public string Content { get; private set; }

        public string Url { get; private set; }

        public string Icon { get; set; }

        public bool Cached { get; set; } = true;

        public static FWAccordionItem CreateContentItem(string title, string content, string itemId)
        {
            var item = new FWAccordionItem()
            {
                Title = title,
                Content = content,
                IsLazyLoaded = false,
                Id = itemId
            };
            return item;
        }

        public static FWAccordionItem CreateLazyItem(string title, string url, string itemId, bool cached)
        {
            var item = new FWAccordionItem()
            {
                Title = title,
                Url = url,
                IsLazyLoaded = true,
                Id = itemId,
                Cached = cached
            };
            return item;
        }
    }
}
