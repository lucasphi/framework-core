using Framework.Core;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Framework.Web.Mvc
{
    static class FWModelMetadataExtensions
    {
        /// <summary>
        /// Checks if the model is required or is a non nullable value.
        /// </summary>
        /// <param name="metadata">The model metadata.</param>
        /// <returns>true if the model is required, false otherwise.</returns>
        public static bool IsRequired(this ModelMetadata metadata)
        {
            return metadata.IsRequired | !FWReflectionHelper.AllowsNullValue(metadata.ModelType);
        }
    }
}
