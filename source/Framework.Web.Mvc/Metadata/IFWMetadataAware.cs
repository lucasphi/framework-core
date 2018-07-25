using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Web.Mvc.Metadata
{
    /// <summary>
    /// Provides an interface for exposing attributes to the ModelMetadata provider.
    /// </summary>
    public interface IFWMetadataAware
    {
        /// <summary>
        /// When implemented in a class, provides metadata to the model metadata creation process.
        /// </summary>
        /// <param name="metadata">The metadata context.</param>
        void OnMetadataCreated(DisplayMetadata metadata);
    }
}
