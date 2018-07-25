using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using SmartFormat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Web.Mvc.Html.Controls.Table
{
    /// <summary>
    /// Represents a generic table column.
    /// </summary>
    public class FWTableColumnCustom : FWTableColumn
    {
        /// <summary>
        /// Gets the column text value.
        /// </summary>
        /// <param name="requestContext">The request helper.</param>
        /// <param name="item">The column object item.</param>
        /// <param name="index">The line index.</param>
        /// <returns>The column text value.</returns>
        public override string GetValue(FWRequestContext requestContext, object item, object index)
        {
            HtmlContentBuilder builder = new HtmlContentBuilder();
            foreach (var template in _templates)
            {
                // Formats the template the the object values.
                Dictionary<string, string> dictionary = item.GetType().GetProperties()
                                                        .ToDictionary(x => x.Name, x => x.GetValue(item)?.ToString() ?? "");
                dictionary.Add("FWTemplateIndex", index.ToString());
                var html = Smart.Format(template, dictionary);
                builder.AppendHtml(html);
            }
            return builder.GetContent();
        }

        /// <summary>
        /// Initializes a new instance of the Framework.Web.Mvc.Html.Controls.Table.FWTableColumnCustom class.
        /// </summary>
        /// <param name="templates">The column custom controls.</param>
        public FWTableColumnCustom(IEnumerable<string> templates)
            : base()
        {
            _templates = templates;
        }
        
        private IEnumerable<string> _templates;
    }
}
