using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Framework.Core
{
    /// <summary>
    /// Extension methods for the MemberInfo class.
    /// </summary>
    public static class MemberInfoExtensions
    {
        /// <summary>
        /// Sets the field or property value of a specified object.
        /// </summary>
        /// <param name="member">The MemberInfo object referece.</param>
        /// <param name="obj">The object whose field or property will be set.</param>
        /// <param name="value">The new value.</param>
        public static void SetValue(this MemberInfo member, object obj, object value)
        {
            if (member is PropertyInfo propertyInfo)
            {
                propertyInfo.SetValue(obj, value);
            }
            else if (member is FieldInfo fieldInfo)
            {
                fieldInfo.SetValue(obj, value);
            }
        }

        /// <summary>
        /// Gets the field or property value of a specified object.
        /// </summary>
        /// <param name="member">The MemberInfo object referece.</param>
        /// <param name="obj">The object whose field or property will be set.</param>
        /// <returns>Returns the value of the field or property.</returns>
        public static object GetValue(this MemberInfo member, object obj)
        {
            if (member is PropertyInfo propertyInfo)
            {
                return propertyInfo.GetValue(obj);
            }
            else if (member is FieldInfo fieldInfo)
            {
                return fieldInfo.GetValue(obj);
            }

            throw new NotSupportedException($"This type is not suppoerted.");
        }

        /// <summary>
        /// Gets the type of this field or property.
        /// </summary>
        /// <param name="member">The MemberInfo object referece.</param>
        /// <returns>Returns the type of the field or property.</returns>
        public static Type GetMemberType(this MemberInfo member)
        {
            if (member is PropertyInfo propertyInfo)
            {
                return propertyInfo.PropertyType;
            }
            else if (member is FieldInfo fieldInfo)
            {
                return fieldInfo.FieldType;
            }

            throw new NotSupportedException($"This type is not suppoerted.");
        }
    }
}
