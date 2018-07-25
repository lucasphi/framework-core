using Framework.Web.Mvc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Framework.Web.Mvc
{
    /// <summary>
    /// Extension methods for <see cref="HttpContext"/> class.
    /// </summary>
    internal static class FWHttpContextExtensions
    {
        internal static string GetViewPath(this HttpContext httpContext)
        {
            return httpContext.Items["ViewVirtualPath"]?.ToString();
        }

        internal static string GetViewName(this HttpContext httpContext)
        {
            return httpContext.Items["ViewName"]?.ToString();
        }

        internal static void SetView(this HttpContext httpContext, string viewPath)
        {
            httpContext.Items["ViewVirtualPath"] = Path.GetDirectoryName(viewPath);
            httpContext.Items["ViewName"] = Path.GetFileNameWithoutExtension(viewPath);
        }

        internal static string GetMainPageName(this HttpContext httpContext)
        {
            return httpContext.Items["MainViewName"]?.ToString();
        }

        internal static void SetMainPageName(this HttpContext httpContext, string viewPath)
        {
            httpContext.Items["MainViewName"] = Path.GetFileNameWithoutExtension(viewPath);
        }

        /// <summary>
        /// Adds a MVVM datasource to the current HttpContext.
        /// </summary>
        /// <param name="httpContext">The current HttpContext.</param>
        /// <param name="key">The datasource key.</param>
        /// <param name="modelType">The datasource model type.</param>
        /// <param name="items">The datasource items.</param>
        internal static void AddDatasource(this HttpContext httpContext, string key, Type modelType, IEnumerable<IFWDatasourceItem> items)
        {
            if (!httpContext.Items.ContainsKey("fwdatasources"))
            {
                httpContext.Items["fwdatasources"] = new Dictionary<string, FWContextDatasource>();
            }

            var ds = httpContext.Items["fwdatasources"] as Dictionary<string, FWContextDatasource>;
            if (!ds.ContainsKey(key))
            {
                ds.Add(key, new FWContextDatasource(modelType, items));
            }
        }

        /// <summary>
        /// Gets all datasources added to the current HttpContext.
        /// </summary>
        /// <param name="httpContext">The current HttpContext.</param>
        /// <returns>A dictionary with the datasources names and items.</returns>
        internal static Dictionary<string, FWContextDatasource> GetAllDatasources(this HttpContext httpContext)
        {
            if (httpContext.Items.ContainsKey("fwdatasources"))
            {
                return httpContext.Items["fwdatasources"] as Dictionary<string, FWContextDatasource>;
            }
            return null;
        }        
    }

    struct FWContextDatasource
    {
        public Type ModelType { get; private set; }

        public IEnumerable<IFWDatasourceItem> Items { get; private set; }

        public FWContextDatasource(Type modelType, IEnumerable<IFWDatasourceItem> items)
        {
            ModelType = modelType;
            Items = items;
        }
    }
}
