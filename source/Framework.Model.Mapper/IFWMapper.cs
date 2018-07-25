using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Model.Mapper
{
    /// <summary>
    /// Defines the interface for the mapper.
    /// </summary>
    public interface IFWMapper
    {
        /// <summary>
        /// Adds a custom mapping function.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TResult">The result type.</typeparam>
        /// <typeparam name="TInjection">The concrete mapper type.</typeparam>
        void AddMap<TSource, TResult, TInjection>()
            where TResult : new()
            where TInjection : IFWInjection, new();

        /// <summary>
        /// Maps an object to another type.
        /// </summary>
        /// <typeparam name="TDestination">The type to map the object.</typeparam>
        /// <param name="source">The object to map.</param>
        /// <param name="targetObject">The referece to an existent target object.</param>
        /// <returns>The mapped object.</returns>
        TDestination Map<TDestination>(IFWMap source, TDestination targetObject = null)
            where TDestination : class, new();

        /// <summary>
        /// Maps an object to another type.
        /// </summary>
        /// <typeparam name="TDestination">The type to map the object.</typeparam>
        /// <typeparam name="TInjection">The injection map type.</typeparam>
        /// <param name="source">The object to map.</param>
        /// <param name="targetObject">The referece to an existent target object.</param>
        /// <returns>The mapped object.</returns>
        TDestination Map<TDestination, TInjection>(IFWMap source, TDestination targetObject = null)
            where TDestination : class, new()
            where TInjection : IFWInjection, new();

        /// <summary>
        /// Maps a list of objects.
        /// </summary>
        /// <typeparam name="TDestination">The type to map the object.</typeparam>
        /// <param name="source">The list of objects to map.</param>
        /// <returns>The list of mapped objects.</returns>
        IEnumerable<TDestination> Map<TDestination>(IEnumerable<IFWMap> source)
            where TDestination : class, new();

        /// <summary>
        /// Transforms an object into another.
        /// </summary>
        /// <typeparam name="TDestination">The type to map the object.</typeparam>
        /// <param name="source">The object to transform.</param>
        /// <returns>The mapped object.</returns>
        TDestination Transform<TDestination>(object source);

        /// <summary>
        /// Transforms an object into another.
        /// </summary>
        /// <param name="type">The type to map the object.</param>
        /// <param name="source">The object to transform.</param>
        /// <returns>The mapped object.</returns>
        object Transform(Type type, object source);

        /// <summary>
        /// Attemps to transform an object into another. 
        /// </summary>
        /// <typeparam name="TInjection">The injection map type.</typeparam>
        /// <param name="type">The type to map the object.</param>
        /// <param name="source">The object to map.</param>
        /// <returns>The mapped object.</returns>
        object Transform<TInjection>(Type type, object source)
            where TInjection : IFWInjection, new();
    }
}
