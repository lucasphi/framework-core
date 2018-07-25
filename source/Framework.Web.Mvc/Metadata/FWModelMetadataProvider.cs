using Framework.Web.Mvc.Resources;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Web.Mvc.Metadata
{
    /// <summary>
    /// Framework implementation of the Microsoft.AspNetCore.Mvc.ModelBinding.Metadata.IDisplayMetadataProvider interface.
    /// </summary>
    public class FWModelMetadataProvider : IDisplayMetadataProvider
    {
        /// <summary>
        /// Sets the values for properties of Microsoft.AspNetCore.Mvc.ModelBinding.Metadata.DisplayMetadataProviderContext.DisplayMetadata.
        /// </summary>
        /// <param name="context">The Microsoft.AspNetCore.Mvc.ModelBinding.Metadata.DisplayMetadataProviderContext.</param>
        public void CreateDisplayMetadata(DisplayMetadataProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException();
            }
            
            context.DisplayMetadata.DisplayName = () =>
            {
                try
                {
                    if (context.Key.Name != null)
                    {
                        return FWStringLocalizer.GetModelResource(context.Key.Name, context.Key.ContainerType.Name);
                    }
                }
                catch (Exception)
                {
                    return string.Empty;
                }
                return string.Empty;
            };

            var attributes = context.Attributes.Where(f => (f as IFWMetadataAware) != null)
                                                .Select(f => f as IFWMetadataAware);

            foreach (var attribute in attributes)
            {
                attribute.OnMetadataCreated(context.DisplayMetadata);
            }
        }
    }
}
