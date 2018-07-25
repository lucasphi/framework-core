using Framework.Model;
using Framework.Web.Mvc.Html.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace Framework.Web.Mvc.Html.Controls.Grid
{
    /// <summary>
    /// Data object for grid configuration.
    /// </summary>
    public class FWGridOptions : IFWTemplateOptions
    {
        /// <summary>
        /// Gets or sets the grid id.
        /// </summary>
        public string Id { get; set; }

        /// <inheritdoc />
        public Type ItemType { get => ServiceMethod.ReturnParameter.ParameterType.GetGenericArguments().First(); }

        /// <summary>
        /// Gets or sets the grid cache id. 
        /// </summary>
        internal string CacheKey { get; }

        /// <summary>
        /// Gets or sets the grid fluent configurator.
        /// </summary>
        internal List<Action<FWGridControl>> FluentConfiguration { get; set; } = new List<Action<FWGridControl>>();

        /// <summary>
        /// Gets the service name to load the data.
        /// </summary>
        internal Type ServiceType { get; set; }

        /// <summary>
        /// Gets or sets the grid reflected service method.
        /// </summary>
        internal MethodInfo ServiceMethod { get; set; }

        /// <summary>
        /// Gets or sets the list filter object.
        /// </summary>
        internal Type FilterType { get => ServiceMethod.GetParameters().FirstOrDefault()?.ParameterType; }

        /// <summary>
        /// Stores the view path of the component tag.
        /// </summary>
        internal string ViewPath { get; set; }

        /// <summary>
        /// Gets or sets if the table columns have sizes defined.
        /// </summary>
        internal bool AutoSizeColumns { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWGridOptions"/> class.
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        public FWGridOptions(string cacheKey)
        {
            CacheKey = cacheKey;
        }
    }
}
