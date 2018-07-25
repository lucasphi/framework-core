using Framework.Model;
using Framework.Web.Mvc.Html.Elements;
using Framework.Web.Mvc.Resources;
using System;

namespace Framework.Web.Mvc.Html.Controls
{
    internal class FWPaginator
    {
        public void CreatePagination(FWDivElement parentElement)
        {
            if (Total > Display)
            {
                FWListElement ul = new FWListElement();
                ul.AddCssClass("fw-pager-nav");

                //Calculates the indexes
                int totalPages = (int)Math.Ceiling((double)(Total / (double)Display));

                int lowerLimit, upperLimit;
                lowerLimit = upperLimit = Page;
                //Search boundaries
                for (var b = 1; b < 5 && b < totalPages;)
                {
                    if (lowerLimit > 1) { lowerLimit--; b++; }
                    if (b < 5 && upperLimit < totalPages) { upperLimit++; b++; }
                }

                bool isFirstPage = (Page == 1);
                //Adds the first button
                ul.Add(CreatePaginationItem("fw-pager-link--first", ViewResources.Pagination_First, "<i class=\"fa fa-angle-double-left\"></i>", 1, isFirstPage));
                ul.Add(CreatePaginationItem("fw-pager-link--prev", ViewResources.Pagination_Prev, "<i class=\"fa fa-angle-left\"></i>", Page - 1, isFirstPage));

                for (int i = lowerLimit; i <= upperLimit; i++)
                {
                    ul.Add(CreatePaginationItem((i == Page) ? 
                                            "fw-pager-link-number fw-pager-link--active" : 
                                            "fw-pager-link-number", null, i.ToString(), i));
                }

                bool isLastPage = (Page == totalPages);
                ul.Add(CreatePaginationItem("fw-pager-link--next", ViewResources.Pagination_Next, "<i class=\"fa fa-angle-right\"></i>", Page + 1, isLastPage));
                ul.Add(CreatePaginationItem("fw-pager-link--last", ViewResources.Pagination_Last, "<i class=\"fa fa-angle-double-right\"></i>", totalPages, isLastPage));
                parentElement.Add(ul);
            }
        }

        private FWListItemElement CreatePaginationItem(string cssclass, string title, string content, int page, bool disabled = false)
        {
            FWListItemElement li = new FWListItemElement();

            FWAnchorElement a = new FWAnchorElement("javascript:;");
            a.AddCssClass("fw-pager-link");
            a.AddCssClass(cssclass);

            if (disabled)
            {
                a.AddCssClass("fw-pager-link--disabled");
            }
            else
            {
                a.Attributes.Add("data-paginate-btn-page", page.ToString());
            }

            if (title != null)
                a.Title = title;

            a.Add(content);

            li.Add(a);
            return li;
        }

        public FWPaginator(IFWPagination paginator)
        {
            if (paginator != null)
            {
                CurrentMin = ((paginator.Page - 1) * paginator.Display) + 1;
                CurrentMax = CurrentMin + paginator.Display - 1;
                if (CurrentMax > paginator.Total)
                    CurrentMax = paginator.Total;

                Display = paginator.Display;
                Page = paginator.Page;
                Total = paginator.Total;
            }
        }

        public int Page { get; private set; }

        /// <summary>
        /// Gets the total items.
        /// </summary>
        public int Total { get; private set; }

        /// <summary>
        /// Gets the amount of items beeing displayed.
        /// </summary>
        public int Display { get; private set; }

        /// <summary>
        /// Gets the min range value for the page.
        /// </summary>
        public int CurrentMin { get; private set; }

        /// <summary>
        /// Gets the max range value for the page.
        /// </summary>
        public int CurrentMax { get; private set; }
    }
}
