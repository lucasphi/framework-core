using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mvc.Html.Controls
{
    internal class FWTabItem
    {
        public string Id { get; private set; }

        public string Title { get; private set; }

        public bool IsLazyLoaded { get; private set; }
        
        public string Content { get; private set; }
        
        public string Url { get; private set; }

        public bool Cached { get; set; } = true;

        public static FWTabItem CreateContentItem(string title, string content, string tabId)
        {
            var item = new FWTabItem()
            {
                Title = title,
                Content = content,
                IsLazyLoaded = false,
                Id = tabId
            };
            return item;
        }

        public static FWTabItem CreateLazyItem(string title, string url, string tabId, bool cached)
        {
            var item = new FWTabItem()
            {
                Title = title,
                Url = url,
                IsLazyLoaded = true,
                Id = tabId,
                Cached = cached
            };
            return item;
        }
    }
}
