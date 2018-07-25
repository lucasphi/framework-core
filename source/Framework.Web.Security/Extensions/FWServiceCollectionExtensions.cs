using Framework.Web.Security.Authentication;
using Framework.Web.Security.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Security
{
    /// <summary>
    /// Extension methods for <see cref="IServiceCollection"/>
    /// </summary>
    public static class FWServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the framework security services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public static IServiceCollection AddFrameworkSecurity(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IFWAuthProvider, FWAuthProvider>();

            serviceCollection.AddScoped<FWRecaptchaValidationFilterAttribute>();

            return serviceCollection;
        }
    }
}
