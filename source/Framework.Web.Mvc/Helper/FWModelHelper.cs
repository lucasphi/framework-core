using Framework.Core;
using Framework.Model;
using Framework.Web.Mvc.DataAnnotations;
using Framework.Web.Mvc.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Framework.Web.Mvc.Helper
{
    class FWModelHelper
    {
        private static Type KeyAttribute = typeof(FWKeyAttribute);

        /// <summary>
        /// Gets the key property of a model.
        /// </summary>
        /// <param name="model">The model object.</param>
        /// <returns>The key property info.</returns>
        public static PropertyInfo GetKeyProperty(object model)
        {
            Type modelType = model.GetType();
            return GetKeyProperty(modelType);
        }

        /// <summary>
        /// Gets the key property of a model.
        /// </summary>
        /// <param name="modelType">The model type.</param>
        /// <returns>The key property info.</returns>
        public static PropertyInfo GetKeyProperty(Type modelType)
        {
            try
            {
                var propertyInfo = modelType.GetProperties().Where(f => f.IsDefined(KeyAttribute)).SingleOrDefault();
                if (propertyInfo != null)
                {
                    return propertyInfo;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new FWMultipleKeyException(modelType.Name, ex);
            }
        }

        /// <summary>
        /// Gets the value of the key property of a model.
        /// </summary>
        /// <param name="model">The model object.</param>
        /// <returns>The key property value.</returns>
        public static object GetKeyPropertyValue(object model)
        {
            var propertyInfo = GetKeyProperty(model);
            return propertyInfo.GetValue(model);
        }

        /// <summary>
        /// Binds a model from a request, matching the property name with the values from the request form.
        /// </summary>
        /// <param name="model">The model to bind.</param>
        /// <param name="queryString">The context query string collection.</param>
        public static void BindFilterFromQueryString(object model, IQueryCollection queryString)
        {   
            foreach (var property in model.GetType().GetProperties())
            {
                if (queryString.ContainsKey(property.Name))
                {
                    object value;
                    if (property.PropertyType.IsEnum)
                    {
                        // If the property is an enum, bind it separately.
                        Enum.TryParse(property.PropertyType, queryString[property.Name].ToString(), out value);
                    }
                    else
                    {
                        value = Convert.ChangeType(queryString[property.Name].ToString(), property.PropertyType);
                    }
                    property.SetValue(model, value);
                }
            }
        }        
    }
}
