using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Web.Mvc.DataAnnotations
{
    /// <summary>
    /// Sets the size of a property when rendered to the screen.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class FWSizeAttribute : FWDataAnnotationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the Framework.Web.DataAnnotations.FWSizeAttribute class.
        /// </summary>
        /// <param name="size">The size enumerator.</param>
        public FWSizeAttribute(FWColSize size)
        {
            Size = size;
        }

        /// <summary>
        /// Gets the defined size for the property.
        /// </summary>
        public FWColSize Size { get; private set; }

        /// <summary>
        /// Sets the device size to which this attribute applies.
        /// </summary>
        public FWDevice Device { get; set; } = FWDevice.Tablet;

        /// <summary>
        /// Provides metadata to the model metadata creation process.
        /// </summary>
        /// <param name="metadata">The model metadata.</param>
        public override void OnMetadataCreated(DisplayMetadata metadata)
        {
            List<FWSizeAttribute> sizes;

            if (!metadata.AdditionalValues.ContainsKey(nameof(FWSizeAttribute)))
            {
                sizes = new List<FWSizeAttribute>();
                metadata.AdditionalValues.Add(nameof(FWSizeAttribute), sizes);
            }
            else
            {
                sizes = metadata.AdditionalValues[nameof(FWSizeAttribute)] as List<FWSizeAttribute>;
            }

            sizes.Add(this);
        }
    }
}
