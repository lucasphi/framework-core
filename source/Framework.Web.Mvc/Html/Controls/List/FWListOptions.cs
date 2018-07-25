using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Framework.Web.Mvc.Html.Controls.List
{
    /// <summary>
    /// Data object for list configuration.
    /// </summary>
    public class FWListOptions : IFWTemplateOptions
    {
        /// <summary>
        /// Gets the list id.
        /// </summary>
        public string Id { get; set; }

        /// <inheritdoc />
        public Type ItemType { get => ServiceMethod.ReturnParameter.ParameterType.GetGenericArguments().First(); }

        /// <summary>
        /// Gets or sets the grid cache id. 
        /// </summary>
        internal string CacheKey { get; }

        /// <summary>
        /// Gets or sets the list fluent configurator.
        /// </summary>
        internal List<Action<FWListControl>> FluentConfiguration { get; set; } = new List<Action<FWListControl>>();        

        /// <summary>
        /// Gets the service name to load the data.
        /// </summary>
        internal Type ServiceType { get; set; }

        /// <summary>
        /// Gets or sets the list reflected service method.
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
        /// Initializes a new instance of the <see cref="FWListOptions"/> class.
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        public FWListOptions(string cacheKey)
        {
            CacheKey = cacheKey;
        }
    }
}
