using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Framework.Web
{
    /// <summary>
    /// Provides helper methods for http requests.
    /// </summary>
    public static class FWHttpUtility
    {
        /// <summary>
        /// Creates a new absolute Uri.
        /// </summary>
        /// <param name="url">The url string.</param>
        /// <returns>The absolute uri.</returns>
        public static Uri CreateUri(string url)
        {
            Uri uri = new Uri(url, UriKind.RelativeOrAbsolute);
            if (!uri.IsAbsoluteUri)
            {
                var request = FWHttpContext.Current.Request;
                var absoluteUri = new UriBuilder()
                {
                    Scheme = request.Scheme,
                    Host = request.Host.Host,
                    Path = request.Path,
                    Query = request.QueryString.ToString()
                };
                if (request.Host.Port.HasValue && request.Host.Port.Value != 80)
                    absoluteUri.Port = request.Host.Port.Value;
                uri = new Uri(absoluteUri.Uri, url);
            }
            return uri;
        }

        /// <summary>
        /// Adds or updates a parameter to the url query string.
        /// </summary>
        /// <param name="url">The url.</param>
        /// <param name="paramName">The new parameter name.</param>
        /// <param name="paramValue">The new parameter value.</param>
        /// <returns>The updated URL.</returns>
        public static string UpdateQueryString(string url, string paramName, string paramValue)
        {
            Uri uri = CreateUri(url);
            return UpdateQueryString(uri, paramName, paramValue);
        }

        /// <summary>
        /// Adds or updates a parameter to the uri query string.
        /// </summary>
        /// <param name="uri">The uri.</param>
        /// <param name="paramName">The new parameter name.</param>
        /// <param name="paramValue">The new parameter value.</param>
        /// <returns>The updated URL.</returns>
        public static string UpdateQueryString(Uri uri, string paramName, string paramValue)
        {
            var queries = QueryHelpers.ParseQuery(uri.Query);
            if (queries.ContainsKey(paramName))
                queries[paramName] = paramValue;
            else
                queries.Add(paramName, paramValue);
            
            UriBuilder uriBuilder = new UriBuilder(uri);            
            uriBuilder.Query = string.Join("&", queries.Keys
                                .SelectMany(key => queries[key]
                                    .Select(value => string.Format("{0}={1}", WebUtility.UrlEncode(key), WebUtility.UrlEncode(value))))
                                .ToArray());
            return uriBuilder.Uri.PathAndQuery;
        }
    }
}
