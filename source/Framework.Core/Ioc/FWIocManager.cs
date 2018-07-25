using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.Ioc
{
    /// <summary>
    /// Resolves the registered types.
    /// </summary>
    public class FWIocManager
    {
        /// <summary>
        /// Gets the default IoC Manager.
        /// </summary>
        public static FWIocManager Default
        {
            get
            {
                try
                {
                    return new FWIocManager(FWIocContainer.Container);
                }
                catch (TypeInitializationException ex)
                {
                    throw new FWIocInitializationException(ex);
                }
            }
        }

        /// <summary>
        /// Determines if a type was registered.
        /// </summary>
        /// <typeparam name="TType">The type to check.</typeparam>
        /// <returns>Returns true if a type was registered. Otherwise returns false.</returns>
        public bool IsRegistered<TType>()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                return scope.IsRegistered<TType>();
            }
        }

        /// <summary>
        /// Determines if a type was registered under a specific name.
        /// </summary>
        /// <typeparam name="TType">The type to check.</typeparam>
        /// <param name="name">The associated name of the component</param>
        /// <returns>Returns true if a type was registered. Otherwise returns false.</returns>
        public bool IsRegistered<TType>(string name)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                return scope.IsRegisteredWithName<TType>(name);
            }
        }

        /// <summary>
        /// Determines if a type was registered.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>Returns true if a type was registered. Otherwise returns false.</returns>
        public bool IsRegistered(Type type)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                return scope.IsRegistered(type);
            }
        }

        /// <summary>
        /// Determines if a type was registered under a specific name.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <param name="name">The associated name of the component</param>
        /// <returns>Returns true if a type was registered. Otherwise returns false.</returns>
        public bool IsRegistered(Type type, string name)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                return scope.IsRegisteredWithName(name, type);
            }
        }

        /// <summary>
        /// Retrieve a concrete object for the registered type. 
        /// </summary>
        /// <typeparam name="TType">The registered type.</typeparam>
        /// <returns>The concrete object.</returns>
        public TType Resolve<TType>()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                if (scope.IsRegistered<TType>())
                    return scope.Resolve<TType>();
                return default(TType);
            }
        }

        /// <summary>
        /// Retrieve a concrete object for the registered type under a specific name. 
        /// </summary>
        /// <typeparam name="TType">The registered type.</typeparam>
        /// <param name="name">The associated name of the component</param>
        /// <returns>The concrete object.</returns>
        public TType Resolve<TType>(string name)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                if (scope.IsRegisteredWithName<TType>(name))
                    return scope.ResolveNamed<TType>(name);
                return default(TType);
            }
        }

        /// <summary>
        /// Retrieve a concrete object for the registered type. 
        /// </summary>
        /// <param name="type">The registered type.</param>
        /// <returns>The concrete object.</returns>
        public object Resolve(Type type)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                if (scope.IsRegistered(type))
                    return scope.Resolve(type);
                return null;
            }
        }

        /// <summary>
        /// Retrieve a concrete object for the registered type under a specific name. 
        /// </summary>
        /// <param name="type">The registered type.</param>
        /// <param name="name">The associated name of the component.</param>
        /// <returns>The concrete object.</returns>
        public object Resolve(Type type, string name)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                if (scope.IsRegisteredWithName(name, type))
                    return scope.ResolveNamed(name, type);
                return null;
            }
        }

        internal FWIocManager(IContainer container)
        {
            _container = container;
        }

        private IContainer _container;
    }
}
