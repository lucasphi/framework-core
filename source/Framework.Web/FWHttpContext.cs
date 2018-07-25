using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Web
{
    /// <summary>
    /// Encapsulates the Microsoft.AspNetCore.Http.HttpContext to expose it statically.
    /// </summary>
    public static class FWHttpContext
    {
        private static IHttpContextAccessor m_httpContextAccessor;

        /// <summary>
        /// Configures the http context.
        /// </summary>
        /// <param name="httpContextAccessor">The http context accessor.</param>
        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            if (m_httpContextAccessor == null)
                m_httpContextAccessor = httpContextAccessor;
            else
                throw new InvalidOperationException("HttpContext cannot be configured more then once.");
        }

        /// <summary>
        /// Gets the current Microsoft.AspNetCore.Http.HttpContext.
        /// </summary>
        public static HttpContext Current
        {
            get
            {
                return m_httpContextAccessor.HttpContext;
            }
        }
    }
}
