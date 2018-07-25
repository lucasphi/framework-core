using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Core.Ioc
{
    /// <summary>
    /// Defines the interface for IoC Modules.
    /// </summary>
    public interface IFWIocModule
    {
        /// <summary>
        /// Method invoked when the IoC is beeing initialized to register the default modules.
        /// </summary>
        /// <param name="container">THe IoC container reference.</param>
        void Register(FWIocContainer container);
    }
}
