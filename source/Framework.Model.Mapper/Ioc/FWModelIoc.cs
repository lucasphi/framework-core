using Framework.Core.Ioc;
using Framework.Model.Mapper;
using Framework.Model.Mapper.Implementation.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Model.Internal.Ioc
{
    /// <summary>
    /// Register all default types to the IoC.
    /// </summary>
    public class FWModelIoc : IFWIocModule
    {
        /// <summary>
        /// Method invoked when the IoC is beeing initialized to register the default modules.
        /// </summary>
        /// <param name="container">THe IoC container reference.</param>
        public void Register(FWIocContainer container)
        {
            // TODO: allow the customization of the concrete mapper, using the project.json file to inform the class.
            container.Register<IFWMapper, FWValueInjecterMapper>();
        }
    }
}
