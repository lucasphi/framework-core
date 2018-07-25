using Framework.Core;
using Framework.Web.Mvc.DataAnnotations;
using Framework.Web.Mvc.Html.Elements;
using Framework.Web.Mvc.Resources;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace Framework.Web.Mvc.Html.Controls
{
    /// <summary>
    /// Represents the base control class for form elements.
    /// </summary>
    public abstract class FWInputControl : FWMetadataControl
    {
        /// <summary>
        /// Configures the control based on the model metadata.
        /// </summary>
        /// <param name="metadata">The model metadata.</param>
        protected override void ReadMetadata(ModelMetadata metadata)
        {
            if (metadata.AdditionalValues.ContainsKey(nameof(FWDataBindAttribute)))
            {
                var bind = metadata.AdditionalValues[nameof(FWDataBindAttribute)] as FWDataBindAttribute;

                // Checks if the user has defined a custom name for the bind name.
                var bindName = bind.Value ?? metadata.PropertyName;

                // Configures the databind configuration object.
                DataBind = new FWBindConfiguration(bindName, bind.Expression);
            }
        }

        /// <summary>
        /// Gets the model resource for the given id.
        /// </summary>
        /// <returns>The resource value.</returns>
        protected virtual string GetModelResource()
        {
            return FWStringLocalizer.GetModelResource(OriginalId, ModelType.Name);
        }

        /// <summary>
        /// Gets the model resource for the given id.
        /// </summary>
        /// <param name="suffix">The resource suffix.</param>
        /// <returns>The resource value.</returns>
        protected virtual string GetModelResource(string suffix)
        {
            return GetModelResource(suffix, out bool resourceNotFound);
        }

        /// <summary>
        /// Gets the model resource for the given id.
        /// </summary>
        /// <param name="suffix">The resource suffix.</param>
        /// <param name="resourceNotFound">Whether the string was found in a resource. If true, an alternate string value was used.</param>
        /// <returns>The resource value.</returns>
        protected virtual string GetModelResource(string suffix, out bool resourceNotFound)
        {
            return FWStringLocalizer.GetModelResource($"{OriginalId}_{suffix}", (ContainerType != null) ? ContainerType.Name : ModelType.Name, out resourceNotFound);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWInputControl" />.
        /// </summary>
        /// <param name="requestContext">The request helper.</param>
        /// <param name="model">The current model.</param>
        /// <param name="metadata">The model metadata.</param>
        protected FWInputControl(FWRequestContext requestContext, object model, ModelMetadata metadata)
            : base(metadata)
        {
            RequestContext = requestContext;            
            Name = metadata.PropertyName;
            
            Model = model;
            DisplayName = metadata.DisplayName;
            ModelType = FWReflectionHelper.GetUnderlyingType(metadata.ModelType);
            ContainerType = metadata.ContainerType;
            IsRequired = metadata.IsRequired();
            IsReadOnly = metadata.IsReadOnly;   
        }

        /// <summary>
        /// Gets or sets the property name of the control.
        /// </summary>
        internal string Name { get; set; }        

        /// <summary>
        /// Gets the current request helper.
        /// </summary>
        protected FWRequestContext RequestContext { get; private set; }

        /// <summary>
        /// Gets the current model.
        /// </summary>
        protected object Model { get; private set; }

        /// <summary>
        /// Gets the model type.
        /// </summary>
        protected Type ModelType { get; private set; }

        /// <summary>
        /// Gets the container model type.
        /// </summary>
        protected Type ContainerType { get; private set; }

        /// <summary>
        /// Gets the display name of the model.
        /// </summary>
        protected string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets if the control will display its label.
        /// </summary>
        public bool DisplayLabel { get; set; } = true;

        /// <summary>
        /// Gets a value indicating wheather or not the model value is required.
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// Gets a value indicating wheather the control is readonly.
        /// </summary>
        public bool IsReadOnly { get; set; }

        /// <summary>
        /// Gets or sets if the control is disabled or not.
        /// </summary>
        public bool IsDisabled { get; set; }

        /// <summary>
        /// Gets or sets the input tooltip.
        /// </summary>
        public string Tooltip { get; set; }

        /// <summary>
        /// Gets or sets the input a data-bind configuration.
        /// </summary>
        internal FWBindConfiguration DataBind { get; private set; } = new FWBindConfiguration();
    }
}
