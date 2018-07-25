using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Reflection;

namespace Framework.Web.Mvc.Resources
{
    /// <summary>
    /// Framework implementation of the <see cref="IStringLocalizer"/>.
    /// </summary>
    public class FWStringLocalizer : IStringLocalizer
    {
        private CultureInfo _localizerCulture;

        /// <summary>
        /// Initializes a new instance of the <see cref="FWStringLocalizer" />.
        /// </summary>
        public FWStringLocalizer()
            : this(CultureInfo.CurrentUICulture)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWStringLocalizer" />.
        /// </summary>
        /// <param name="culture">The System.Globalization.CultureInfo to use.</param>
        public FWStringLocalizer(CultureInfo culture)
        {
            _localizerCulture = culture;
        }

        /// <summary>
        /// Gets the string resource with the given name.
        /// </summary>
        /// <param name="name">The name of the string resource.</param>
        /// <returns>The string resource as a Microsoft.Extensions.Localization.LocalizedString.</returns>
        public LocalizedString this[string name]
        {
            get
            {
                if (name == null)
                {
                    throw new ArgumentNullException(nameof(name));
                }

                var value = GetViewResource(name, FWHttpContext.Current.GetViewName(), out bool resourceNotFound, _localizerCulture);

                // If the resource was not found, use the key as value to maintain default mvc localization behavior.
                if (resourceNotFound)
                    value = name;
                
                return new LocalizedString(name, value, resourceNotFound);
            }
        }

        /// <summary>
        /// Gets the string resource with the given name and formatted with the supplied arguments.
        /// </summary>
        /// <param name="name">The name of the string resource.</param>
        /// <param name="arguments">The values to format the string with.</param>
        /// <returns>The formatted string resource as a Microsoft.Extensions.Localization.LocalizedString.</returns>
        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                if (name == null)
                {
                    throw new ArgumentNullException(nameof(name));
                }

                var value = GetViewResource(name, FWHttpContext.Current.GetViewName(), out bool resourceNotFound, _localizerCulture);

                // Formats the value.
                value = string.Format(value, arguments);

                // If the resource was not found, use the key as value to maintain default mvc localization behavior.
                if (resourceNotFound)
                    value = name;

                return new LocalizedString(name, value, resourceNotFound);
            }
        }

        /// <summary>
        /// Gets all string resources.
        /// </summary>
        /// <param name="includeParentCultures">A System.Boolean indicating whether to include strings from parent cultures.</param>
        /// <returns>The strings.</returns>
        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a string localizer for a specific culture.
        /// </summary>
        /// <param name="culture">The System.Globalization.CultureInfo to use.</param>
        /// <returns>A culture specific FWStringLocalizer.</returns>
        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            return new FWStringLocalizer(culture);
        }

        /// <summary>
        /// Attemps to get the current page title.
        /// </summary>
        /// <returns>The current page title, if it was defined in a resource file.</returns>
        public static string GetPageTitle()
        {
            return GetViewResource("Title", FWHttpContext.Current.GetMainPageName(), out bool resourceNotFound, CultureInfo.CurrentUICulture);
        }

        /// <summary>
        /// Gets the view resource value.
        /// </summary>
        /// <param name="key">The resource key.</param>
        /// <returns>The resource value if any.</returns>
        public static string GetViewResource(string key)
        {
            return GetViewResource(key, FWHttpContext.Current.GetViewName(), out bool resourceNotFound);
        }

        /// <summary>
        /// Gets the view resource value.
        /// </summary>
        /// <param name="key">The resource key.</param>
        /// <param name="prefix">The resource prefix.</param>
        /// <returns>The resource value if any.</returns>
        public static string GetViewResource(string key, string prefix)
        {
            return GetViewResource(key, prefix, out bool resourceNotFound);
        }

        /// <summary>
        /// Gets the view resource value.
        /// </summary>
        /// <param name="key">The resource key.</param>
        /// <param name="prefix">The resource prefix.</param>
        /// <param name="resourceNotFound">Whether the string was found in a resource. If true, an alternate string value was used.</param>
        /// <param name="culture">The System.Globalization.CultureInfo to use.</param>
        /// <returns>The resource value if any.</returns>
        public static string GetViewResource(string key, string prefix, out bool resourceNotFound, CultureInfo culture = null)
        {            
            string fullkey;
            if (!string.IsNullOrWhiteSpace(prefix))
                fullkey = string.Format("{0}_{1}", prefix, key);
            else
                fullkey = key;
            var displayValue = GetResource(fullkey, "ViewResources", culture);

            // If the resource could not be found at the application, try looking for it at the UI Resource
            if (displayValue == null)
            {
                displayValue = ViewResources.ResourceManager.GetString(fullkey);
            }

            if (displayValue != null)
            {
                resourceNotFound = false;
                return displayValue.ToString();
            }
            else
            {
                resourceNotFound = true;
                return fullkey;
            }
        }

        /// <summary>
        /// Gets the model resource value.
        /// </summary>
        /// <param name="key">The resource key.</param>
        /// <returns>The resource value, if any.</returns>
        public static string GetModelResource(string key)
        {
            return GetModelResource(key, null, out bool resourceNotFound);
        }

        /// <summary>
        /// Gets the model resource value.
        /// </summary>
        /// <param name="key">The resource key.</param>
        /// <param name="prefix">The resource prefix.</param>
        /// <returns>The resource value, if any.</returns>
        public static string GetModelResource(string key, string prefix)
        {
            return GetModelResource(key, prefix, out bool resourceNotFound);
        }

        /// <summary>
        /// Gets the model resource value.
        /// </summary>
        /// <param name="key">The resource key.</param>
        /// <param name="prefix">The resource prefix.</param>
        /// <param name="resourceNotFound">Whether the string was found in a resource. If true, an alternate string value was used.</param>
        /// <param name="culture">The System.Globalization.CultureInfo to use.</param>
        /// <returns>The resource value, if any.</returns>
        public static string GetModelResource(string key, string prefix, out bool resourceNotFound, CultureInfo culture = null)
        {
            string fullkey;
            if (!string.IsNullOrWhiteSpace(prefix))
                fullkey = string.Format("{0}_{1}", prefix, key);
            else
                fullkey = key;

            var displayValue = GetResource(fullkey, "ModelResources", culture);

            // If the resource could not be found at the application, try looking for it at the framework Resource
            if (displayValue == null)
            {
                displayValue = Resources.ResourceManager.GetString(fullkey);
            }

            if (displayValue != null)
            {
                resourceNotFound = false;
                return displayValue.ToString();
            }
            else
            {
                resourceNotFound = true;
                return fullkey;
            }
        }

        private static object GetResource(string key, string resource, CultureInfo culture)
        {
            if (FWHttpContext.Current.Items["ViewVirtualPath"] == null)
                return null;

            if (culture == null)
                culture = CultureInfo.CurrentUICulture;
            
            string resourceValue = null;
            ResourceManager manager;
            
            try
            {
                // Look for the local resource
                string localResourceName = $"{FWApp.DefaultNamespace}{FWHttpContext.Current.Items["ViewVirtualPath"].ToString().Replace('\\', '.')}.Resources.{resource}";
                manager = GetResourceManager(localResourceName);
                resourceValue = manager.GetString(key, culture);
            }
            catch (MissingManifestResourceException)
            {
                // If the resource file is not found, skip it.
            }

            // If displayValue is not localy found, try the application global resource.
            if (_appHasGlobalResource && resourceValue == null)
            {
                try
                {
                    // If there is not local resource, look for the global resource
                    string globalResourceName = $"{FWApp.DefaultNamespace}.Resources.{resource}";
                    manager = GetResourceManager(globalResourceName);
                    resourceValue = manager.GetString(key, culture);
                }
                catch (MissingManifestResourceException)
                {
                    // If the resource is not found, skip it.
                    _appHasGlobalResource = false;
                }
            }

            return resourceValue;
        }

        private static ResourceManager GetResourceManager(string resourceName)
        {
            if (_resourceManagers.ContainsKey(resourceName))
                return _resourceManagers[resourceName];

            Assembly assembly = Assembly.GetEntryAssembly();
            var manager = new ResourceManager(resourceName, assembly);
            _resourceManagers.Add(resourceName, manager);
            return manager;
        }

        private static Dictionary<string, ResourceManager> _resourceManagers = new Dictionary<string, ResourceManager>();
        private static bool _appHasGlobalResource = true;
    }
}
