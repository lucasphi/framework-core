using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Model.Mapper.Implementation.ValueInjecter
{
    /// <summary>
    /// Extension methods for ValueInjecter MapperInstance class.
    /// </summary>
    public static class FWValueInjecterExtensions
    {
        /// <summary>
        /// Maps source object to result type.
        /// </summary>
        /// <param name="mapper">The mapper instance.</param>
        /// <param name="resultType">The result type.</param>
        /// <param name="source">The source object.</param>
        /// <param name="tag">The object used to send additional paramaters for the mapping code.</param>
        /// <returns>The mapped object.</returns>
        public static object Map(this MapperInstance mapper, Type resultType, object source, object tag = null)
        {
            if (!TryExistingMappings(mapper, resultType, source, tag, out object targetObject))
            {
                targetObject = mapper.DefaultMap(source, resultType, tag);
            }

            return targetObject;
        }

        /// <summary>
        /// Attempts to transform an object using an existing map. If none is found, maps using the TInjection injection.
        /// </summary>
        /// <typeparam name="TInjection">The <see cref="IFWInjection"/> type.</typeparam>
        /// <param name="mapper">The mapper instance.</param>
        /// <param name="resultType">The result type.</param>
        /// <param name="source">The source object.</param>
        /// <param name="tag">The object used to send additional paramaters for the mapping code.</param>
        /// <returns>The mapped object.</returns>
        public static object MapInject<TInjection>(this MapperInstance mapper, Type resultType, object source, object tag = null)
            where TInjection : IFWInjection, new()
        {
            if (!TryExistingMappings(mapper, resultType, source, tag, out object targetObject))
            {
                targetObject = Activator.CreateInstance(resultType);
                targetObject.InjectFrom<TInjection>(source);
            }

            return targetObject;
        }

        /// <summary>
        /// Checks for primitive mappings.
        /// </summary>
        internal static object MapPrimitive(this MapperInstance mapper, Type resultType, object source)
        {
            var sourceType = source.GetType();
            mapper.Maps.TryGetValue(new Tuple<Type, Type>(sourceType, resultType), out Tuple<object, bool> funct);

            if (funct != null)
            {
                var parameters = funct.Item2 ? new[] { source, null } : new[] { source };
                return funct.Item1.GetType().GetMethod("Invoke").Invoke(funct.Item1, parameters);
            }

            return source;
        }

        /// <summary>
        /// Attempts to transform an object using an existing map.
        /// </summary>
        private static bool TryExistingMappings(MapperInstance mapper, Type resultType, object source, object tag, out object result)
        {
            var sourceType = source.GetType();

            mapper.Maps.TryGetValue(new Tuple<Type, Type>(sourceType, resultType), out Tuple<object, bool> funct);

            if (funct != null)
            {
                var parameters = funct.Item2 ? new[] { source, tag } : new[] { source };
                result = funct.Item1.GetType().GetMethod("Invoke").Invoke(funct.Item1, parameters);
                return true;
            }

            result = null;
            return false;
        }
    }
}
