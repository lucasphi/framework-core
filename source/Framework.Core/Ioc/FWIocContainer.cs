﻿using Autofac;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.Ioc
{
    /// <summary>
    /// Encapsulates the Ioc container.
    /// </summary>
    public class FWIocContainer
    {
        /// <summary>
        /// Register a component to be created through reflection.
        /// </summary>
        /// <typeparam name="TType">The registered type.</typeparam>
        /// <typeparam name="TConcrete">The concrete type.</typeparam>
        public void Register<TType, TConcrete>()
        {
            _builder.RegisterType<TConcrete>().As<TType>();
        }

        /// <summary>
        /// Register a component to be created through reflection under a specific name.
        /// </summary>
        /// <typeparam name="TType">The registered type.</typeparam>
        /// <typeparam name="TConcrete">The concrete type.</typeparam>
        /// <param name="name">The name to associate with the component.</param>
        public void Register<TType, TConcrete>(string name)
        {
            _builder.RegisterType<TConcrete>().As<TType>().Named<TType>(name);
        }

        /// <summary>
        /// Register a component to be created through reflection.
        /// </summary>
        /// <param name="type">The registered type.</param>
        /// <param name="concrete">The concrete type.</param>
        public void Register(Type type, Type concrete)
        {
            _builder.RegisterType(concrete).As(type);
        }

        /// <summary>
        /// Register a component to be created through reflection under a specific name.
        /// </summary>
        /// <param name="type">The registered type.</param>
        /// <param name="concrete">The concrete type.</param>
        /// <param name="name">The associated name of the component</param>
        public void Register(Type type, Type concrete, string name)
        {
            _builder.RegisterType(concrete).As(type).Named(name, type);
        }

        /// <summary>
        /// Creates a new IoC manager with the component registrations that have been made.
        /// </summary>
        public FWIocManager Build()
        {
            var container = _builder.Build();
            return new FWIocManager(container);
        }

        private ContainerBuilder _builder = new ContainerBuilder();

        internal static IContainer Container { get; set; }
    }
}
