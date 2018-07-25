using Framework.Web.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace Framework.Web.Mvc.Html.Controls
{
    /// <summary>
    /// Represents controls that uses attributes for configuration.
    /// </summary>
    public abstract class FWMetadataControl : FWControl
    {
        internal void SetSize(params (FWColSize size, FWDevice device)[] sizes)
        {
            Sizes.Clear();
            foreach (var item in sizes)
            {
                Sizes.Add(new FWControlSize(item.size, item.device));
            }
        }

        /// <summary>
        /// Configures the control based on the model metadata.
        /// </summary>
        /// <param name="metadata">The model metadata.</param>
        protected virtual void ReadMetadata(ModelMetadata metadata)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWMetadataControl" />.
        /// </summary>
        /// <param name="metadata">The model metadata.</param>
        public FWMetadataControl(ModelMetadata metadata)
            : base(metadata.PropertyName)
        {
            ReadMetadata(metadata);

            if (metadata.AdditionalValues.ContainsKey(nameof(FWSizeAttribute)))
            {
                var sizes = metadata.AdditionalValues[nameof(FWSizeAttribute)] as List<FWSizeAttribute>;
                foreach (var item in sizes)
                {
                    Sizes.Add(new FWControlSize(item.Size, item.Device));
                }
            }
        }
    }
}
