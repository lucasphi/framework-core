using Framework.Core;
using Framework.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Framework.Repository.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="IFWSpecification&lt;TModel&gt;"/>.
    /// </summary>
    public static class FWSpecificationExtensions
    {
        /// <summary>
        /// Adds a sort information to the data options. This method will not do anything if the sortinfo already contains values.
        /// </summary>
        /// <typeparam name="TModel">The model type.</typeparam>
        /// <typeparam name="TProperty">The property type.</typeparam>
        /// <param name="data">The data options object referece.</param>
        /// <param name="prop">The lambda expression for the property to order from.</param>
        /// <param name="overrideExisting">Set to true if the order should override any existing configuration.</param>
        public static void OrderBy<TModel, TProperty>(this IFWSpecification<TModel> data, Expression<Func<TModel, TProperty>> prop, bool overrideExisting = false)
        {
            OrderBy(data, prop, FWSortDirection.Ascending, overrideExisting);
        }

        /// <summary>
        /// Adds a sort information to the data options. This method will not do anything if the sortinfo already contains values.
        /// </summary>
        /// <typeparam name="TModel">The model type.</typeparam>
        /// <typeparam name="TProperty">The property type.</typeparam>
        /// <param name="data">The data options object referece.</param>
        /// <param name="prop">The lambda expression for the property to order from.</param>
        /// <param name="direction">The sort direction.</param>
        /// <param name="overrideExisting">Set to true if the order should override any existing configuration.</param>
        public static void OrderBy<TModel, TProperty>(this IFWSpecification<TModel> data, Expression<Func<TModel, TProperty>> prop, FWSortDirection direction, bool overrideExisting = false)
        {
            if (data.QueryOptions == null)
                throw new ArgumentException("The specification does not have an instance of IFWDataOptions");

            if (data.QueryOptions.SortInfo == null)
                return;

            var member = prop.Body as MemberExpression;
            var propInfo = member.Member as PropertyInfo;

            if (!overrideExisting && data.QueryOptions.SortInfo.Any(f => f.SortName == propInfo.Name))
                return;

            data.QueryOptions.SortInfo.Add(new FWDataSorting() { SortName = propInfo.Name, SortDirection = direction });
        }
    }
}
