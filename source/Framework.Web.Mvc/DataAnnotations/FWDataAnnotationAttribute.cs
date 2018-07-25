using Framework.Web.Mvc.Metadata;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Web.Mvc.DataAnnotations
{
    /// <summary>
    /// Absctract class that represents data annotation attributes for MVC models.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class FWDataAnnotationAttribute : Attribute, IFWMetadataAware
    {
        /// <summary>
        /// Provides metadata to the model metadata creation process.
        /// </summary>
        /// <param name="metadata">The model metadata.</param>
        public virtual void OnMetadataCreated(DisplayMetadata metadata)
        {
            metadata.AdditionalValues.Add(this.GetType().Name, this);
        }
    }
}
