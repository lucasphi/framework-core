using Framework.Model;
using Framework.Model.Mapper;
using Framework.Repository.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Repository
{
    /// <summary>
    /// Extension methods for mapping entity objects.
    /// </summary>
    public static class FWEntityMapperExtensions
    {
        /// <summary>
        /// Maps an object to an entity.
        /// </summary>
        /// <typeparam name="TDestination">The entity type.</typeparam>
        /// <param name="source">The source object.</param>
        /// <returns>The mapped entity.</returns>
        public static TDestination MapEntity<TDestination>(this IFWMap source)
            where TDestination : class, IFWEntity, new()
        {
            return source.Map<TDestination, FWEntityInjection>();
        }
    }
}
