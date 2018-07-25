using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Model
{
    /// <summary>
    /// Defines a custom name for mapping purposes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class FWMapNameAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the Framework.Model.DataAnnotations.FWMapNameAttribute class.
        /// </summary>
        /// <param name="name"></param>
        public FWMapNameAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Gets the name of the map.
        /// </summary>
        public string Name { get; private set; }
    }
}
