using Framework.Model.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Security.Mapper
{
    /// <summary>
    /// Injection for <see cref="FWSecureLong"/>.
    /// </summary>
    public class FWSecureLongInjection : IFWInjection
    {
        /// <summary>
        /// Maps the value into another.
        /// </summary>
        /// <param name="source">The source value.</param>
        /// <param name="target">The target value.</param>
        /// <returns>The mapped value.</returns>
        public object Map(object source, object target)
        {
            if (source is FWSecureLong secureLong)
            {
                return (long)(source as FWSecureLong);
            }           
            else if (source is long sourceLong)
            {
                (target as FWSecureLong).Value = (long)source;
                return target;
            }

            return null;
        }
    }

    /// <summary>
    /// Injection for <see cref="FWSecureInt"/>.
    /// </summary>
    public class FWSecureIntInjection : IFWInjection
    {
        /// <summary>
        /// Maps the value into another.
        /// </summary>
        /// <param name="source">The source value.</param>
        /// <param name="target">The target value.</param>
        /// <returns>The mapped value.</returns>
        public object Map(object source, object target)
        {
            if (source is FWSecureInt secureInt)
            {
                return (int)source;
            }
            else if (source is int sourceInt)
            {
                (target as FWSecureInt).Value = (int)source;
                return target;
            }

            return null;
        }
    }
}
