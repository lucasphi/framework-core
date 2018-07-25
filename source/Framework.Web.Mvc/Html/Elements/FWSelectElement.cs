using Framework.Web.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Web.Mvc.Html.Elements
{
    /// <summary>
    /// Represents a HTML select element.
    /// </summary>
    public class FWSelectElement : FWTagBuilder
    {
        /// <summary>
        /// Gets or sets the 'data-bind' attribute.
        /// </summary>
        internal string DataBind
        {
            get => Attributes["data-bind"];
            set => Attributes["data-bind"] = value;
        }

        /// <summary>
        /// Adds a new option to the select control.
        /// </summary>
        /// <param name="option">The option information.</param>
        /// <param name="isSelected">Inform if the option is selected or not.</param>
        public void Add(IFWDatasourceItem option, bool isSelected = false)
        {
            Add(option.Id, option.Value, isSelected);
        }

        /// <summary>
        /// Adds a new option to the select control.
        /// </summary>
        /// <param name="id">The option key.</param>
        /// <param name="value">The option description.</param>
        /// <param name="isSelected">Inform if the option is selected or not.</param>
        public void Add(string id, string value, bool isSelected = false)
        {
            var selected = (isSelected) ? " selected" : string.Empty;
            Add(string.Format("<option value=\"{0}\"{1}>{2}</option>", id, selected, value));
        }

        /// <summary>
        /// Creates a new HTML select element.
        /// </summary>
        public FWSelectElement(string name)
            : base("select")
        {
            Attributes.Add("name", name);
        }        
    }
}
