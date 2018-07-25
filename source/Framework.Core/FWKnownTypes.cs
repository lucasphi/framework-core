using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core
{
    /// <summary>
    /// Encapsulates the framework known types.
    /// </summary>
    public class FWKnownTypes
    {
        /// <summary>
        /// Gets the IEnumerable interface type.
        /// </summary>
        public static readonly Type IEnumerable = typeof(IEnumerable);

        /// <summary>
        /// Gets the FWIgnoreAttribute type.
        /// </summary>
        public static readonly Type IgnoreAttribute = typeof(FWIgnoreAttribute);

        /// <summary>
        /// Gets the DateTime type.
        /// </summary>
        public static readonly Type DateTime = typeof(DateTime);

        /// <summary>
        /// Gets the FlagsAttribute type.
        /// </summary>
        public static readonly Type Flags = typeof(FlagsAttribute);

        /// <summary>
        /// Gets the Nullable generic type.
        /// </summary>
        public static readonly Type Nullable = typeof(Nullable<>);

        /// <summary>
        /// Gets the long type.
        /// </summary>
        public static readonly Type Long = typeof(long);

        /// <summary>
        /// Gets the bool type.
        /// </summary>
        public static readonly Type Bool = typeof(bool);

        /// <summary>
        /// Gets the string type.
        /// </summary>
        public static readonly Type String = typeof(string);

        /// <summary>
        /// Gets the decimal type.
        /// </summary>
        public static readonly Type Decimal = typeof(decimal);
    }
}
