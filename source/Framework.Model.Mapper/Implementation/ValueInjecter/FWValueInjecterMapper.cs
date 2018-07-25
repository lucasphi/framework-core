using Omu.ValueInjecter;
using Omu.ValueInjecter.Injections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Framework.Model.Mapper.Implementation.ValueInjecter
{
    /// <summary>
    /// Mapper implementation using Value Injecter package.
    /// </summary>
    public class FWValueInjecterMapper : IFWMapper
    {
        /// <summary>
        /// Initializes the mapper.
        /// </summary>
        static FWValueInjecterMapper()
        {
            Omu.ValueInjecter.Mapper.DefaultMap = (src, resType, tag) =>
            {
                if (tag == null)
                    tag = Activator.CreateInstance(resType);
                tag.InjectFrom<FWInjection>(src);
                return tag;
            };
        }

        /// <summary>
        /// Adds a custom mapping function.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TResult">The result type.</typeparam>
        /// <typeparam name="TInjection">The concrete mapper type.</typeparam>
        public void AddMap<TSource, TResult, TInjection>()
            where TResult : new()
            where TInjection : IFWInjection, new()
        {
            Omu.ValueInjecter.Mapper.AddMap<TSource, TResult>((src) =>
            {
                var tag = new TResult();
                tag = (TResult)tag.InjectFrom<TInjection>(src);
                return tag;
            });
        }

        /// <summary>
        /// Maps an object to another type.
        /// </summary>
        /// <typeparam name="TDestination">The type to map the object.</typeparam>
        /// <param name="source">The object to map.</param>
        /// <param name="targetObject">The referece to an existent target object.</param>
        /// <returns>The mapped object.</returns>
        public TDestination Map<TDestination>(IFWMap source, TDestination targetObject = null)
            where TDestination : class, new()
        {
            return Omu.ValueInjecter.Mapper.Map<TDestination>(source, targetObject);
        }

        /// <summary>
        /// Maps an object to another type.
        /// </summary>
        /// <typeparam name="TDestination">The type to map the object.</typeparam>
        /// <typeparam name="TInjection">The injection map type.</typeparam>
        /// <param name="source">The object to map.</param>
        /// <param name="targetObject">The referece to an existent target object.</param>
        /// <returns>The mapped object.</returns>
        public TDestination Map<TDestination, TInjection>(IFWMap source, TDestination targetObject = null)
            where TDestination : class, new()
            where TInjection : IFWInjection, new()
        {
            var mapper = Omu.ValueInjecter.Mapper.Instance;
            return mapper.MapInject<TInjection>(typeof(TDestination), source) as TDestination;
        }

        /// <summary>
        /// Maps a list of objects.
        /// </summary>
        /// <typeparam name="TDestination">The type to map the object.</typeparam>
        /// <param name="source">The list of objects to map.</param>
        /// <returns>The list of mapped objects.</returns>
        public IEnumerable<TDestination> Map<TDestination>(IEnumerable<IFWMap> source)
            where TDestination : class, new()
        {
            var mapper = Omu.ValueInjecter.Mapper.Instance;
            var genericType = source.GetType().GetGenericTypeDefinition().MakeGenericType(typeof(TDestination));
            var tmpList = mapper.Map(genericType, source) as IList<TDestination>;
            return tmpList;
        }

        /// <summary>
        /// Transforms an object into another.
        /// </summary>
        /// <typeparam name="TDestination">The type to map the object.</typeparam>
        /// <param name="source">The object to transform.</param>
        /// <returns>The mapped object.</returns>
        public TDestination Transform<TDestination>(object source)
        {
            return Omu.ValueInjecter.Mapper.Map<TDestination>(source);
        }

        /// <summary>
        /// Transforms an object into another.
        /// </summary>
        /// <param name="type">The type to map the object.</param>
        /// <param name="source">The object to transform.</param>
        /// <returns>The mapped object.</returns>
        public object Transform(Type type, object source)
        {
            var mapper = Omu.ValueInjecter.Mapper.Instance;
            return mapper.Map(type, source);
        }

        /// <summary>
        /// Attemps to transform an object into another. 
        /// </summary>
        /// <typeparam name="TInjection">The injection map type.</typeparam>
        /// <param name="type">The type to map the object.</param>
        /// <param name="source">The object to map.</param>
        /// <returns>The mapped object.</returns>
        public object Transform<TInjection>(Type type, object source)
            where TInjection : IFWInjection, new()
        {
            var mapper = Omu.ValueInjecter.Mapper.Instance;
            return mapper.MapInject<TInjection>(type, source);
        }
    }
}
