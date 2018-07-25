using System;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Serialization;

namespace Framework.Core
{
    /// <summary>
    /// Helper class to work with JSON.
    /// </summary>
    public static class FWJsonHelper
    {
        /// <summary>
        /// Serializes the specified object to a JSON string.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="ignoreNullValues">Ignore null values.</param>
        /// <returns>A JSON string representation of the object.</returns>
        public static string Serialize(object obj, bool ignoreNullValues = false)
        {
            if (obj == null)
                return null;

            var settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = ignoreNullValues ? NullValueHandling.Ignore : NullValueHandling.Include
            };

            return JsonConvert.SerializeObject(obj, Formatting.None, settings);
        }

        /// <summary>
        /// Deserializes a JSON string.
        /// </summary>
        /// <param name="json">The JSON string.</param>
        /// <param name="settings">The serialization settings.</param>
        /// <returns>The deserialized object.</returns>
        public static object Deserialize(string json, JsonSerializerSettings settings = null)
        {
            return Deserialize<object>(json, settings);
        }

        /// <summary>
        /// Deserializes a JSON string.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="json">The JSON string.</param>
        /// <param name="settings">The serialization settings.</param>
        /// <returns>The deserialized object.</returns>
        public static T Deserialize<T>(string json, JsonSerializerSettings settings = null)
        {
            if (settings == null)
            {
                //Default settings
                settings = new JsonSerializerSettings();
            }

            return JsonConvert.DeserializeObject<T>(json, settings);
        }

        /// <summary>
        /// Converts an object to a json valid object.
        /// </summary>
        /// <param name="obj">The object reference.</param>
        /// <param name="ignoreNullValues">Ignore null values.</param>
        /// <returns>The json string.</returns>
        public static string JsonObject(object obj, bool ignoreNullValues = false)
        {
            var settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = ignoreNullValues ? NullValueHandling.Ignore : NullValueHandling.Include
            };

            var stringWriter = new StringWriter();
            var serializer = JsonSerializer.Create(settings);
            using (var writer = new JsonTextWriter(stringWriter))
            {
                writer.QuoteName = false;
                serializer.Serialize(writer, obj);
            }
            var itemJson = stringWriter.ToString();
            return itemJson;
        }
    }
}
