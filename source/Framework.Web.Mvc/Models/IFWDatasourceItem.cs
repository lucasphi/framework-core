using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mvc.Models
{
    /// <summary>
    /// Common interface for datasource items.
    /// </summary>
    public interface IFWDatasourceItem
    {
        /// <summary>
        /// The item id.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// The item value.
        /// </summary>
        string Value { get; set; }
    }
}
