using Framework.Web.Mvc.Html.Elements;
using System.Collections.Generic;

namespace Framework.Web.Mvc.Html.Controls
{
    /// <summary>
    /// Represents a Tab control.
    /// </summary>
    public class FWTabControl : FWControlBlock
    {
        /// <summary>
        /// Creates the control main element.
        /// </summary>
        /// <returns>The control IFWHtmlElement interface.</returns>
        protected override IFWHtmlElement CreateControl()
        {
            var tab = new FWDivElement()
            {
                Id = Id,
                DataType = "fw-tab"
            };
            tab.MergeAttributes(Attributes);
            if (!string.IsNullOrWhiteSpace(CustomCss))
                tab.AddCssClass(CustomCss);

            var tabList = tab.Add(new FWListElement());
            tabList.AddCssClass("nav nav-tabs m-tabs-line m-tabs-line--2x");
            tabList.Attributes.Add("role", "tablist");

            var tabBody = tab.Add(new FWDivElement());
            tabBody.AddCssClass("tab-content");

            int tabNumber = 1;
            foreach (var item in _tabList)
            {
                // Fills the header
                CreateTabHeader(tabList, tabNumber, item);

                // Fills the body
                CreateTabBody(tabBody, tabNumber, item);

                tabNumber++;
            }

            return tab;
        }

        private void CreateTabHeader(FWListElement tabNav, int tabNumber, FWTabItem item)
        {
            var listItem = tabNav.Add(new FWListItemElement());
            {
                listItem.AddCssClass("nav-item m-tabs__item");

                var anchor = listItem.Add(new FWAnchorElement($"#{item.Id}"));
                anchor.AddCssClass("nav-link m-tabs__link");
                anchor.Add(item.Title);
                anchor.Attributes.Add("data-toggle", "tab");
                anchor.Attributes.Add("role", "tab");
                anchor.Attributes.Add("aria-expanded", (tabNumber == _activeTab).ToString().ToLower());
                //Marks the anchor as binded to prevent adding the load screen
                anchor.Attributes.Add("data-binded", "true");
                anchor.Attributes.Add("data-type", "fw-tab-item");
                if (tabNumber == _activeTab)
                    anchor.AddCssClass("active");

                if (item.IsLazyLoaded)
                {
                    anchor.Attributes.Add("data-url", item.Url);
                    anchor.Attributes.Add("data-targetid", item.Id);
                    anchor.Attributes.Add("data-cache", item.Cached.ToString().ToLower());
                }
            }
        }

        private void CreateTabBody(FWDivElement tabBody, int tabNumber, FWTabItem item)
        {
            var bodyItem = tabBody.Add(new FWDivElement());
            {
                bodyItem.AddCssClass("tab-pane");
                bodyItem.Attributes.Add("role", "tabpanel");
                if (tabNumber == _activeTab)
                    bodyItem.AddCssClass("active");
                bodyItem.Id = item.Id;

                if (!item.IsLazyLoaded)
                {
                    bodyItem.Add(item.Content);
                }
            }
        }

        /// <summary>
        /// Adds a tab.
        /// </summary>
        /// <param name="title">The tab title.</param>
        /// <param name="content">The tab html content.</param>
        /// <returns>The fluent configurator object.</returns>
        public FWTabControl AddContentTab(string title, string content)
        {
            var tabNumber = _tabList.Count + 1;
            _tabList.Add(FWTabItem.CreateContentItem(title, content, $"{Id}_{tabNumber}"));
            return this;
        }

        /// <summary>
        /// Adds a tab.
        /// </summary>
        /// <param name="title">The tab title.</param>
        /// <param name="url">The url to load the tab content.</param>
        /// <param name="cached">Informs if the tab content should be cached.</param>
        /// <returns>The fluent configurator object.</returns>
        public FWTabControl AddLazyTab(string title, string url, bool cached)
        {
            var tabNumber = _tabList.Count + 1;
            _tabList.Add(FWTabItem.CreateLazyItem(title, url, $"{Id}_{tabNumber}", cached));

            return this;
        }

        /// <summary>
        /// Creates a new Tab control.
        /// </summary>
        /// <param name="id">The control id.</param>
        public FWTabControl(string id) 
            : base(id)
        { }

        private int _activeTab = 1;
        private List<FWTabItem> _tabList = new List<FWTabItem>();
    }
}
