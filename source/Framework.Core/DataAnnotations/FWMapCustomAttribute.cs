using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Model
{
    /// <summary>
    /// Changes the mapping behavior or the annotated property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FWMapCustomAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FWMapCustomAttribute"/> class.
        /// </summary>
        /// <param name="mapper">The custom mapper type.</param>
        public FWMapCustomAttribute(Type mapper)
        {
            Mapper = mapper;
        }

        /// <summary>
        /// Gets the custom mapper.
        /// </summary>
        public Type Mapper { get; private set; }
    }
}
