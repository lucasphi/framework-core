using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mvc.Models
{
    /// <summary>
    /// Represents a basic datasource item.
    /// </summary>
    public sealed class FWDatasourceItem : IFWDatasourceItem
    {
        /// <summary>
        /// The item id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The item value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWDatasourceItem"/> class.
        /// </summary>
        public FWDatasourceItem()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWDatasourceItem"/> class.
        /// </summary>
        /// <param name="id">The item id.</param>
        /// <param name="value">The item value.</param>
        public FWDatasourceItem(string id, string value)
        {
            Id = id;
            Value = value;
        }
    }
}
