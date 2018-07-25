using Framework.Core.Ioc;
using Framework.Web.Mvc.Html;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mvc
{
    /// <summary>
    /// Extension methods for the service collection interface.
    /// </summary>
    public static class FWServiceCollectionExtensions
    {
        /// <summary>
        /// Adds an application menu to the service collection.
        /// </summary>
        /// <typeparam name="TImplementation">The type of the <see cref="IFWMenuComponent" /> to use.</typeparam>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to add the service to.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddMenu<TImplementation>(this IServiceCollection serviceCollection)
            where TImplementation : class, IFWMenuComponent
        {
            if (_hasMenu)
                throw new InvalidOperationException("Only one main menu can be registered.");

            _hasMenu = true;
            serviceCollection.AddSingleton<IFWMenuComponent, TImplementation>();
            return serviceCollection;
        }

        /// <summary>
        /// Adds a top menu to the service collection.
        /// </summary>
        /// <typeparam name="TImplementation">The type of the <see cref="IFWMenuComponent" /> to use.</typeparam>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to add the service to.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddTopMenu<TImplementation>(this IServiceCollection serviceCollection)
            where TImplementation : class, IFWMenuComponent
        {
            _iocContainer.Register<IFWMenuComponent, TImplementation>(_topMenuCount.ToString());
            _topMenuCount += 1;
            return serviceCollection;
        }

        internal static IEnumerable<TType> GetRegisteredTopMenus<TType>()
        {
            if (_iocManager == null)
                _iocManager = _iocContainer.Build();

            List<TType> response = new List<TType>();
            int i = 0;
            while (_iocManager.IsRegistered<TType>(i.ToString()))
            {
                response.Add(_iocManager.Resolve<TType>(i.ToString()));
                i += 1;
            }
            return response;
        }

        private static FWIocContainer _iocContainer = new FWIocContainer();
        private static FWIocManager _iocManager;

        private static bool _hasMenu = false;
        private static int _topMenuCount = 0;
    }
}
