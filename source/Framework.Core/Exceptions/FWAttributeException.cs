using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Core
{
    /// <summary>
    /// Represents an exception thrown with an attribute of a type.
    /// </summary>
    public class FWAttributeException : FWException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="FWAttributeException"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="attribute">The failed attempted attribute.</param>
        public FWAttributeException(Type type, Type attribute) 
            : base(string.Format(Resources.Resources.FWAttributeException, type.Name, attribute.Name))
        { }
    }
}
