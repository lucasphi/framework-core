using Framework.Model.Mapper;
using Framework.Model.Mapper.Implementation.ValueInjecter;
using System;
using System.Collections.Generic;

namespace Framework.Model
{
    /// <summary>
    /// Abstract class for mappers.
    /// </summary>
    public static class FWMapperHelper
    {
        #region Extension methods
        /// <summary>
        /// Maps an object to another type.
        /// </summary>
        /// <typeparam name="TDestination">The type to map the object.</typeparam>
        /// <param name="source">The object to map.</param>
        /// <param name="targetObject">The referece to an existent target object.</param>
        /// <returns>The mapped object.</returns>
        public static TDestination Map<TDestination>(this IFWMap source, TDestination targetObject = null)
            where TDestination : class, new()
        {
            var mapper = new FWValueInjecterMapper();
            return mapper.Map(source, targetObject);
        }

        /// <summary>
        /// Maps an object to another type.
        /// </summary>
        /// <typeparam name="TDestination">The type to map the object.</typeparam>
        /// <typeparam name="TInjection">The injection map type.</typeparam>
        /// <param name="source">The object to map.</param>
        /// <param name="targetObject">The referece to an existent target object.</param>
        /// <returns>The mapped object.</returns>
        public static TDestination Map<TDestination, TInjection>(this IFWMap source, TDestination targetObject = null)
            where TDestination : class, new()
            where TInjection : IFWInjection, new()
        {
            var mapper = new FWValueInjecterMapper();
            return mapper.Map<TDestination, TInjection>(source, targetObject);
        }

        /// <summary>
        /// Maps a list of objects.
        /// </summary>
        /// <typeparam name="TDestination">The type to map the object.</typeparam>
        /// <param name="source">The list of objects to map.</param>
        /// <returns>The list of mapped objects.</returns>
        public static IEnumerable<TDestination> Map<TDestination>(this IEnumerable<IFWMap> source)
            where TDestination : class, new()
        {
            var mapper = new FWValueInjecterMapper();
            return mapper.Map<TDestination>(source);
        }
        #endregion

        #region Helper methods
        /// <summary>
        /// Attemps to transform an object into another. 
        /// </summary>
        /// <typeparam name="TDestination">The type to map the object.</typeparam>
        /// <param name="source">The object to map.</param>
        /// <returns>The mapped object.</returns>
        public static TDestination Transform<TDestination>(object source)
        {
            var mapper = new FWValueInjecterMapper();
            return mapper.Transform<TDestination>(source);
        }

        /// <summary>
        /// Attemps to transform an object into another. 
        /// </summary>
        /// <param name="type">The type to map the object.</param>
        /// <param name="source">The object to map.</param>
        /// <returns>The mapped object.</returns>
        public static object Transform(Type type, object source)
        {
            var mapper = new FWValueInjecterMapper();
            return mapper.Transform(type, source);
        }

        /// <summary>
        /// Attemps to transform an object into another. 
        /// </summary>
        /// <typeparam name="TInjection">The injection map type.</typeparam>
        /// <param name="type">The type to map the object.</param>
        /// <param name="source">The object to map.</param>
        /// <returns>The mapped object.</returns>
        public static object Transform<TInjection>(Type type, object source)
            where TInjection : IFWInjection, new()
        {
            var mapper = new FWValueInjecterMapper();
            return mapper.Transform<TInjection>(type, source);
        }

        /// <summary>
        /// Adds a custom mapping function.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TResult">The result type.</typeparam>
        /// <typeparam name="TInjection">The concrete mapper type.</typeparam>
        public static void AddMap<TSource, TResult, TInjection>()
            where TResult : new()
            where TInjection : IFWInjection, new()
        {
            var mapper = new FWValueInjecterMapper();
            mapper.AddMap<TSource, TResult, TInjection>();
        }
        #endregion
    }
}
