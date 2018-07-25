using Framework.Model;
using Framework.Web.Mvc.Html.Elements;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Web.Mvc.Html.Controls
{
    /// <summary>
    /// Represents a Timeline control.
    /// </summary>
    public class FWTimelineControl : FWControl
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
                DataType = "fw-timeline"
            };
            element.MergeAttributes(Attributes);
            if (!string.IsNullOrWhiteSpace(CustomCss))
                element.AddCssClass(CustomCss);

            element.AddCssClass("m-timeline-3");

            var body = new FWDivElement();
            body.AddCssClass("m-timeline-3__items");

            foreach (var group in _entries)
            {
                var dateDiv = new FWDivElement();
                dateDiv.AddCssClass("m-timeline-3__date");
                dateDiv.Add(group.Key.Date.ToShortDateString());
                body.Add(dateDiv);

                foreach (var entry in group)
                {
                    body.Add(CreateTimelineEntry(entry));
                }
            }

            element.Add(body);

            return element;
        }

        private FWDivElement CreateTimelineEntry(FWTimelineEntry entry)
        {
            var div = new FWDivElement();
            var entryState = entry.State.GetDescription();
            div.AddCssClass($"m-timeline-3__item m-timeline-3__item--{entryState}");

            // Adds the entry date
            var entryDate = new FWSpanElement(entry.Date.ToShortTimeString());
            entryDate.AddCssClass("m-timeline-3__item-time");
            div.Add(entryDate);

            // Adds the entry text.
            var text = new FWDivElement();
            if (entry.Url != null)
            {
                text.Add($"<a href=\"{entry.Url(_requestContext.Url)}\">");
            }

            text.AddCssClass("m-timeline-3__item-desc");
            var textSpan = new FWSpanElement(entry.Text);
            textSpan.AddCssClass("m-timeline-3__item-text");
            text.Add(textSpan);

            // Adds the entry source.
            text.Add("<br />");
            var sourceSpan = new FWSpanElement();
            sourceSpan.AddCssClass("m-timeline-3__item-user-name");
            var innerSpan = sourceSpan.Add(new FWSpanElement(entry.Source));
            innerSpan.AddCssClass("m-link m-link--metal m-timeline-3__item-link");
            text.Add(sourceSpan);

            if (entry.Url != null)
            {
                text.Add("</a>");
            }

            div.Add(text);

            return div;
        }

        /// <summary>
        /// Creates a new <see cref="FWTimelineControl"/> control.
        /// </summary>
        /// <param name="id">The control id.</param>
        /// <param name="entries">The timeline entries.</param>
        /// <param name="requestContext">The request context.</param>
        public FWTimelineControl(string id, IEnumerable<FWTimelineEntry> entries, FWRequestContext requestContext)
            : base(id)
        {
            _requestContext = requestContext;
            _entries = entries.OrderByDescending(o => o.Date).GroupBy(g => g.Date.Date);
        }

        private IEnumerable<IGrouping<DateTime, FWTimelineEntry>> _entries { get; set; }

        private FWRequestContext _requestContext;
    }
}
