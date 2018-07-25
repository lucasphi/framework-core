using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Model
{
    /// <summary>
    /// Marks a private field or property as a mapper target.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class FWMapPrivateAttribute : Attribute
    { }
}
