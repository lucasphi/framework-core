using Framework.Core.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core
{
    /// <summary>
    /// Helper class to work with Reflections.
    /// </summary>
    public static class FWReflectionHelper
    {
        /// <summary>
        /// Checks if a type is of another type or inherits from another type.
        /// </summary>
        /// <typeparam name="T">The base type.</typeparam>
        /// <param name="type">The type to check.</param>
        /// <returns>True if the type is equal or inherits from T. Otherwise returns false.</returns>
        public static bool CheckType<T>(Type type)
        {
            Type baseType = typeof(T);            
            return (type.Equals(baseType)
                    || type.IsSubclassOf(baseType)
                    || (type.IsGenericType && Nullable.GetUnderlyingType(type).Equals(baseType)));
        }

        /// <summary>
        /// Gets the type or, if it is nullable, gets the underlying type.
        /// </summary>
        /// <param name="type">The type to get.</param>
        /// <returns>The underlying type if its nullable. Otherwise return the type.</returns>
        public static Type GetUnderlyingType(Type type)
        {
            if (!IsNullable(type))
                return type;
            return Nullable.GetUnderlyingType(type);
        }

        /// <summary>
        /// Checks if a type is nullable.
        /// </summary>
        /// <param name="type">The type object.</param>
        /// <returns>True if the type is nullable. False otherwise.</returns>
        public static bool IsNullable(Type type)
        {
            return (type.IsConstructedGenericType && type.GetGenericTypeDefinition() == FWKnownTypes.Nullable);
        }

        /// <summary>
        /// Checks if a type is numeric or not.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>True if the type is numeric. Otherwise returns false.</returns>
        public static bool IsNumeric(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if a type is an integral type.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>True if the type if numeric. False otherwise.</returns>
        // Table of integral types: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/integral-types-table
        public static bool IsIntegral(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if a type is an enumerable or an array.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>True if is an enumerable. Otherwise returns false.</returns>
        public static bool IsCollection(Type type)
        {
            return (type.IsArray || (FWKnownTypes.IEnumerable.IsAssignableFrom(type)) && type != FWKnownTypes.String);
        }

        /// <summary>
        /// Checks if a type allows null values.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>True if the type allows null. Otherwiser returns false.</returns>
        public static bool AllowsNullValue(Type type)
        {
            return (IsNullable(type.UnderlyingSystemType) || !type.IsValueType);
        }

        /// <summary>
        /// Checks if a type is a primitive type. This method considers enums, strings and datetime as primitives.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>True if the type is primitive. Otherwise, returns False.</returns>
        public static bool IsPrimitive(Type type)
        {
            if (type.IsPrimitive || type.IsValueType)
                return true;

            var underlyingtype = GetUnderlyingType(type);
            return (underlyingtype == FWKnownTypes.String ||
                    underlyingtype == FWKnownTypes.DateTime ||
                    underlyingtype == FWKnownTypes.Decimal);
        }

        /// <summary>
        /// Checks if a type is an Enum or Nullable&lt;Enum&gt;.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>True if is an enum. Otherwise returns false.</returns>
        public static bool IsEnum(Type type)
        {
            if (type.IsEnum)
                return true;

            var underlyingType = Nullable.GetUnderlyingType(type.UnderlyingSystemType);
            return ((underlyingType != null) && underlyingType.IsEnum);
        }

        /// <summary>
        /// Searchs for the properties of the given type. 
        /// </summary>
        /// <param name="type">The type to search.</param>
        /// <param name="bindingFlags">The binding flags.</param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> GetProperties(Type type, BindingFlags bindingFlags)
        {
            return type.GetProperties(bindingFlags);
        }

        /// <summary>
        /// Gets all declared public properties that are not set to be ignored.
        /// </summary>
        /// <param name="type">The type reference.</param>
        /// <returns>An enumerable of System.Reflection.PropertyInfo objects representing all properties of the current type.</returns>
        public static IEnumerable<PropertyInfo> GetPublicProperties(Type type)
        {
            return GetPublicProperties(type, BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        }

        /// <summary>
        /// Gets all public properties that are not set to be ignored.
        /// </summary>
        /// <param name="type">The type reference.</param>
        /// <param name="declaredOnly">Informs if inherited properties should be returned or not.</param>
        /// <returns>An enumerable of System.Reflection.PropertyInfo objects representing all properties of the current type.</returns>
        public static IEnumerable<PropertyInfo> GetPublicProperties(Type type, bool declaredOnly)
        {
            BindingFlags bindingFlags;
            if (declaredOnly)
                bindingFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;
            else
                bindingFlags = BindingFlags.Public | BindingFlags.Instance;
            return GetPublicProperties(type, bindingFlags);
        }

        /// <summary>
        /// Gets the default value of a type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The default value of the type.</returns>
        public static object GetDefault(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }

        /// <summary>
        /// Gets all public properties that are not set to be ignored.
        /// </summary>
        /// <param name="type">The type reference.</param>
        /// <param name="bindingFlags">The binding flags.</param>
        /// <returns>An enumerable of System.Reflection.PropertyInfo objects representing all properties of the current type.</returns>
        public static IEnumerable<PropertyInfo> GetPublicProperties(Type type, BindingFlags? bindingFlags)
        {
            var properties = type.GetProperties(bindingFlags.Value);
            return properties.Where(f => !f.IsDefined(FWKnownTypes.IgnoreAttribute));
        }

        /// <summary>
        /// Gets all members of a given type.
        /// </summary>
        /// <param name="type">The type reference.</param>
        /// <param name="bindingFlags">The binding flags.</param>
        /// <returns>An enumerable of System.Reflection.MemberInfo objects representing all members of the current type.</returns>
        public static IEnumerable<MemberInfo> GetMembers(Type type, BindingFlags bindingFlags)
        {
            // TODO: store all members within a cache to improve performance.
            return type.GetMembers(bindingFlags);
        }
    }
}
