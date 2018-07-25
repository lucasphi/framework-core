using Framework.Core;
using Framework.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core
{
    /// <summary>
    /// Helper class to work with enums.
    /// </summary>
    public static class FWEnumHelper
    {
        /// <summary>
        /// Gets the value of a property from an attribute of enum type.
        /// </summary>
        /// <typeparam name="TReturn">The return type.</typeparam>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <param name="enumObject"></param>
        /// <param name="property">The property to get the value from.</param>
        /// <returns>Returns the TReturn type.</returns>
        public static TReturn GetAttributeProperty<TReturn, TAttribute>(Enum enumObject, string property)
            where TAttribute : Attribute
        {
            return GetAttributeProperty<TReturn>(enumObject, typeof(TAttribute), property);
        }

        /// <summary>
        /// Gets the value of a property from an attribute of enum type.
        /// </summary>
        /// <typeparam name="TReturn">The return type.</typeparam>
        /// <param name="enumObject">The enum object.</param>
        /// <param name="attributeType">The attribute type.</param>
        /// <param name="property">The property to get the value from.</param>
        /// <returns>Returns the TReturn type.</returns>
        public static TReturn GetAttributeProperty<TReturn>(Enum enumObject, Type attributeType, string property)
        {
            var enumType = enumObject.GetType();
            string cacheKey = string.Format("{0}{1}.{2}", enumType.ToString(), enumObject.ToString(), property);

            if (!attrCache.ContainsKey(cacheKey))
            {
                FieldInfo fi = enumType.GetRuntimeField(enumObject.ToString());

                if (fi != null)
                {
                    var attribute = fi.GetCustomAttributes(attributeType, true).SingleOrDefault();

                    if (attribute != null)
                    {
                        PropertyInfo pi = attribute.GetType().GetRuntimeProperty(property);
                        var piValue = pi.GetValue(attribute);
                        attrCache.Add(cacheKey, piValue);
                    }
                    else
                    {
                        throw new FWAttributeException(enumObject.GetType(), attributeType);
                    }
                }
            }

            return (TReturn)attrCache[cacheKey];
        }

        /// <summary>
        /// Checks if the enum field is decorated with an attribute.
        /// </summary>
        /// <typeparam name="TAttribute">The attribute type.</typeparam>
        /// <param name="enumObject">The enum object.</param>
        /// <returns>True if the attribute exists. False otherwise.</returns>
        public static bool HasAttribute<TAttribute>(Enum enumObject)
            where TAttribute : Attribute
        {
            var enumType = enumObject.GetType();
            FieldInfo fi = enumType.GetRuntimeField(enumObject.ToString());
            if (fi != null)
            {
                var attribute = fi.GetCustomAttributes<TAttribute>();
                return attribute.Any();
            }
            return false;
        }

        /// <summary>
        /// Generates a list from the enum values.
        /// </summary>
        /// <param name="enumtype">The enum type.</param>
        /// <returns>A list with the enum values.</returns>
        public static List<string> GetEnumAsList(Type enumtype)
        {
            List<string> listBuilder = new List<string>();

            if (FWReflectionHelper.IsNullable(enumtype))
            {
                listBuilder.Add(string.Empty);
                enumtype = Nullable.GetUnderlyingType(enumtype);
            }

            foreach (var item in Enum.GetValues(enumtype))
            {
                if (!HasAttribute<FWIgnoreAttribute>(item as Enum))
                    listBuilder.Add(((int)item).ToString());
            }

            return listBuilder;
        }

        /// <summary>
        /// Generates a dictionary from an enum where the key is the enum string name and the value is the enum int value.
        /// </summary>
        /// <param name="enumtype">The enum type.</param>
        /// <returns>The dictionary generated from the enum.</returns>
        public static Dictionary<string, string> GetEnumAsDictonary(Type enumtype)
        {
            if (!FWReflectionHelper.IsEnum(enumtype))
                throw new FWArgumentException("enumtype");

            Dictionary<string, string> dictonaryBuilder = new Dictionary<string, string>();

            //Gets the true type, whether is nullable or not.
            enumtype = FWReflectionHelper.GetUnderlyingType(enumtype);

            foreach (var item in Enum.GetValues(enumtype))
            {
                if (!HasAttribute<FWIgnoreAttribute>(item as Enum))
                    dictonaryBuilder.Add(((int)item).ToString(), item.ToString());
            }

            return dictonaryBuilder;
        }

        /// <summary>
        /// Converts all values from an Enum to an enumerable.
        /// </summary>
        /// <param name="enuminput">The enum object.</param>
        /// <returns>The enum value enumerable.</returns>
        public static IEnumerable<string> GetValues(Enum enuminput)
        {
            if (enuminput != null)
            {
                var type = enuminput.GetType();
                if (type.IsDefined(FWKnownTypes.Flags, false))
                {
                    foreach (Enum value in Enum.GetValues(type))
                    {
                        if (enuminput.HasFlag(value))
                        {
                            var intVal = Convert.ToInt32(value);
                            if (intVal > 0)
                                yield return intVal.ToString();
                        }
                    }
                }
                else
                {
                    yield return Convert.ToInt32(enuminput).ToString();
                }
            }
            yield break;
        }

        private static FWAttributeCache attrCache = new FWAttributeCache();
    }
}
