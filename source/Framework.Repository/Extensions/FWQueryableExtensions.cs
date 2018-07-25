using Framework.Core;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Framework.Repository
{
    /// <summary>
    /// Extension methods for the IQueryable interface.
    /// </summary>
    public static class FWQueryableExtensions
    {
        private static MethodInfo _orderBy = typeof(Queryable).GetMethods().First(m => m.Name == "OrderBy");
        private static MethodInfo _thenBy = typeof(Queryable).GetMethods().First(m => m.Name == "ThenBy");
        private static MethodInfo _orderByDescending = typeof(Queryable).GetMethods().First(m => m.Name == "OrderByDescending");
        private static MethodInfo _thenByDescending = typeof(Queryable).GetMethods().First(m => m.Name == "ThenByDescending");

        /// <summary>
        /// Orders the query by a property name.
        /// </summary>
        /// <typeparam name="T">The entity type.</typeparam>
        /// <param name="source">The IQueryable list.</param>
        /// <param name="property">The property name to order by.</param>
        /// <returns>The ordered queryable list.</returns>
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string property)
        {
            var tType = typeof(T);
            var propInfo = tType.GetProperty(property, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            var method = _orderBy.MakeGenericMethod(tType, propInfo.PropertyType);
            return OrderBy(source, tType, propInfo, method);
        }

        /// <summary>
        /// Orders the query by a property name.
        /// </summary>
        /// <typeparam name="T">The entity type.</typeparam>
        /// <param name="source">The IQueryable list.</param>
        /// <param name="property">The property name to order by.</param>
        /// <returns>The ordered queryable list.</returns>
        public static IOrderedQueryable<T> ThenBy<T>(this IQueryable<T> source, string property)
        {
            var tType = typeof(T);
            var propInfo = tType.GetProperty(property, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            var method = _thenBy.MakeGenericMethod(tType, propInfo.PropertyType);
            return OrderBy(source, tType, propInfo, method);
        }

        /// <summary>
        /// Orders the query descending by a property name.
        /// </summary>
        /// <typeparam name="T">The entity type.</typeparam>
        /// <param name="source">The IQueryable list.</param>
        /// <param name="property">The property name to order by.</param>
        /// <returns>The ordered queryable list.</returns>
        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string property)
        {
            var tType = typeof(T);
            var propInfo = tType.GetProperty(property, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            var method = _orderByDescending.MakeGenericMethod(tType, propInfo.PropertyType);
            return OrderBy(source, tType, propInfo, method);
        }

        /// <summary>
        /// Orders the query descending by a property name.
        /// </summary>
        /// <typeparam name="T">The entity type.</typeparam>
        /// <param name="source">The IQueryable list.</param>
        /// <param name="property">The property name to order by.</param>
        /// <returns>The ordered queryable list.</returns>
        public static IOrderedQueryable<T> ThenByDescending<T>(this IQueryable<T> source, string property)
        {
            var tType = typeof(T);
            var propInfo = tType.GetProperty(property, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            var method = _thenByDescending.MakeGenericMethod(tType, propInfo.PropertyType);
            return OrderBy(source, tType, propInfo, method);
        }

        private static IOrderedQueryable<T> OrderBy<T>(IQueryable<T> source, System.Type tType, PropertyInfo propInfo, MethodInfo method)
        {
            var p = Expression.Parameter(tType);
            var sortExpr = Expression.Lambda(Expression.Property(p, propInfo), p);

            var expr = source.Expression;
            var call = Expression.Call(method, expr, sortExpr);
            var newQuery = source.Provider.CreateQuery<T>(call);
            return newQuery as IOrderedQueryable<T>;
        }
    }
}
