using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Model
{
    /// <summary>
    /// Skips the property from being mapped.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FWMapperIgnoreAttribute : Attribute
    { }
}
