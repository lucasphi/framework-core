using Framework.Core;
using Framework.Web.Mvc.DataAnnotations;
using Framework.Web.Mvc.Exceptions;
using Framework.Web.Mvc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Framework.Web.Mvc.Helper
{
    class FWDataModelResolverHelper
    {
        public static FWDataModelResolverHelper Serialize(ViewContext viewContext)
        {
            // Gets the view model type.
            var modelType = viewContext.ViewData.ModelMetadata.ModelType;
            // Gets the view model or creates an instance.
            var model = viewContext.ViewData.Model ?? Activator.CreateInstance(modelType);

            var obj = Serialize(model, modelType, viewContext.HttpContext, false);

            // Checks if the user has defined some custom bindings.
            obj._customScript = GetCustomBindings(viewContext.HttpContext);

            return obj;
        }

        public string ToScript(string modelName)
        {
            StringBuilder sb = new StringBuilder();            

            sb.Append(_modelData.ToString());
            sb.Append(_datasourceData.ToString());

            var json = sb.ToString();
            if (string.IsNullOrWhiteSpace(json))
                return string.Empty;

            var data = $"{_innerProperties} fw.{modelName} = function (base) {{ var self=this;self.fn = {{ push: function(v, item, target) {{ self[v].push(item); ko.applyBindings(item, target); }} }}; {json}{_customScript} }};";

            return data;
        }

        private static FWDataModelResolverHelper Serialize(object model, Type modelType, HttpContext context, bool bindNameWhenNull)
        {
            var obj = new FWDataModelResolverHelper(context)
            {
                _bindNameNotValue = bindNameWhenNull
            };

            // Adds the model binds and datasources.
            obj.Loop(model, modelType);

            // Adds HttpContext datasources (used by select boolean and enum values).
            var ds = context.GetAllDatasources();
            if (ds != null)
            {
                foreach (var item in ds)
                {
                    // Asserts that the datasource is only added to the corrent model.
                    if (item.Value.ModelType == modelType)
                        obj._datasourceData.Append($"self.ds{item.Key}={obj.ReadDatasourceItems(item.Value.Items)};");
                }
            }

            return obj;
        }

        private static string GetCustomBindings(HttpContext context, string collectionBind = null)
        {
            string viewPath = context.GetViewPath();
            if (viewPath != null)
            {
                viewPath = viewPath.Substring(1, viewPath.Length - 1);
                var viewName = (collectionBind == null) ? 
                            context.GetViewName() : 
                            $"{context.GetViewName()}_{collectionBind}";

                var customJsPath = Path.Combine(viewPath, viewName) + ".js";
                if (File.Exists(customJsPath))
                {
                    return File.ReadAllText(customJsPath);
                }
            }
            return null;
        }

        private void Loop(object model, Type modelType)
        {
            var properties = FWReflectionHelper.GetProperties(modelType, BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in properties)
            {                
                // Checks if the property has the FWDataBind attribute.
                if (prop.IsDefined(typeof(FWDataBindAttribute)))
                {
                    Databind(prop, model);
                }
            }
        }

        /// <summary>
        /// Adds a property to the model.
        /// </summary>
        /// <param name="prop">The property PropertyInfo.</param>
        /// <param name="model">The model object reference.</param>
        private void Databind(PropertyInfo prop, object model)
        {
            if (FWReflectionHelper.IsPrimitive(prop.PropertyType))
            {
                BindPrimitive(prop, model);
            }
            else if (FWReflectionHelper.IsCollection(prop.PropertyType))
            {
                BindCollection(prop, model);
            }
        }

        private void BindPrimitive(PropertyInfo prop, object model)
        {
            var value = prop.GetValue(model);

            string formattedValue;
            if (_bindNameNotValue)
            {
                formattedValue = prop.Name;
            }
            else
            {
                formattedValue =  PropertyValue(value, prop.PropertyType);
            }
            _modelData.Append($"self.{prop.Name}=ko.observable({formattedValue});");
        }

        private void BindCollection(PropertyInfo prop, object model)
        {
            var value = prop.GetValue(model);

            var collectionGeneric = prop.PropertyType.GetGenericArguments().FirstOrDefault();
            var listProperties = FWReflectionHelper.GetProperties(collectionGeneric, BindingFlags.Public | BindingFlags.Instance).Where(f => f.IsDefined(typeof(FWDataBindAttribute)));

            // Creates the class represent             
            var collectionModel = Serialize(Activator.CreateInstance(collectionGeneric), collectionGeneric, _context, true);            
            var propertiesNames = listProperties.Select(f => f.Name);
            var customString = GetCustomBindings(_context, collectionGeneric.Name);
            _innerProperties.Append($"fw.{collectionGeneric.Name} = function(parent,{string.Join(",", propertiesNames)}) {{ var self=this; {collectionModel._modelData} {collectionModel._datasourceData} {customString} }};");

            // Adds the existing values
            var array = new List<string>();
            if (value is IEnumerable list)
            {
                foreach (var item in list)
                {
                    var itemValues = listProperties.Select(f => PropertyValue(f.GetValue(item), f.PropertyType));
                    array.Add($"new fw.{collectionGeneric.Name}(this,{string.Join(",", itemValues)})");
                }
            }

            _modelData.Append($"self.{prop.Name}=ko.observableArray([{string.Join(",", array)}]);");
        }

        private string ReadDatasourceItems(object value)
        {
            var stringWriter = new StringWriter();
            var serializer = JsonSerializer.Create(new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            using (var writer = new JsonTextWriter(stringWriter))
            {
                writer.QuoteName = false;
                serializer.Serialize(writer, value);
            }
            var itemJson = stringWriter.ToString();
            return itemJson;
        }

        private string PropertyValue(object propertyValue, Type propertyType)
        {
            var type = FWReflectionHelper.GetUnderlyingType(propertyType);

            if (propertyValue == null)
            {
                var defaultValue = FWReflectionHelper.GetDefault(propertyType);
                return defaultValue == null ? "''" : defaultValue.ToString();
            }

            if (type == FWKnownTypes.String)
            {
                return $"'{propertyValue.ToString().Replace("'", "\\'")}'";
            }
            else if (type == FWKnownTypes.Bool)
            {
                return (bool)propertyValue ? "true" : "false";
            }
            else if (type == FWKnownTypes.Decimal)
            {
                return ((decimal)propertyValue).ToString(new CultureInfo("en"));
            }
            else if (type == FWKnownTypes.DateTime)
            {
                return $"moment.utc('{((DateTime)propertyValue).ToString("yyyy-MM-ddTHH:mm:ss")}')";
            }

            return propertyValue.ToString();
        }

        private FWDataModelResolverHelper(HttpContext context)
        {
            _context = context;
        }

        private HttpContext _context;
        private StringBuilder _innerProperties = new StringBuilder();
        private StringBuilder _modelData = new StringBuilder();        
        private StringBuilder _datasourceData = new StringBuilder();
        private string _customScript;
        
        private static Type _datasourceItem = typeof(IFWDatasourceItem);

        private bool _bindNameNotValue = false;
    }
}
