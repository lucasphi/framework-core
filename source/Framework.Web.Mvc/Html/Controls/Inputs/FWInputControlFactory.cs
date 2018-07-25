using Framework.Core;
using Framework.Web.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Reflection;

namespace Framework.Web.Mvc.Html.Controls
{
    internal class FWInputControlFactory
    {
        /// <summary>
        /// Creates the control main element.
        /// </summary>
        public FWInputControl CreateControl()
        {
            FWInputControl control = null;

            control = AttemptCreateControlByAttribute() ?? AttemptCreateControlByModelType();

            return control;
        }

        private FWInputControl AttemptCreateControlByAttribute()
        {
            // Checks if the model is decorated as a select.
            if (_metadata.AdditionalValues.ContainsKey(nameof(FWSelectAttribute)))
                return CreateSelect();

            // Checks if the model is decorated as an hidden input.
            if (_metadata.AdditionalValues.ContainsKey(nameof(FWHiddenAttribute)))
                return CreateHiddenInput();

            return null;
        }

        private FWInputControl AttemptCreateControlByModelType()
        {
            if (_modelType == FWKnownTypes.Bool)
                return CreateCheckbox();

            if (_modelType == FWKnownTypes.DateTime)
                return CreateDatepicker();
            
            // Checks if the model is an enum.
            if (FWReflectionHelper.IsEnum(_modelType))
                return CreateSelect();

            return CreateTextbox();
        }

        private FWInputControl CreateSelect()
        {
            FWSelectControl control = new FWSelectControl(_requestContext, _model, _metadata, Container);
            control.Attributes.Add("data-control", "select");
            return control;
        }

        private FWInputControl CreateTextbox()
        {
            var control = new FWTextboxControl(_requestContext, _model, _metadata);
            control.Attributes.Add("data-control", "textbox");
            return control;
        }

        private FWInputControl CreateCheckbox()
        {
            var control = new FWCheckboxControl(_requestContext, _model, _metadata);
            control.Attributes.Add("data-control", "checkbox");
            return control;
        }

        private FWInputControl CreateDatepicker()
        {
            var control = new FWDatepickerControl(_requestContext, _model, _metadata);
            control.Attributes.Add("data-control", "datepicker");
            return control;
        }

        private FWInputControl CreateHiddenInput()
        {
            var control = new FWHiddenControl(_requestContext, _model, _metadata);
            control.Attributes.Add("data-control", "hidden");
            return control;
        }

        /// <summary>
        /// Creates a new control.
        /// </summary>
        /// <param name="requestContext">The request helper.</param>
        /// <param name="model">The current model.</param>
        /// <param name="metadata">The model metadata.</param>
        public FWInputControlFactory(FWRequestContext requestContext, object model, ModelMetadata metadata)
        {
            _requestContext = requestContext;
            _modelType = FWReflectionHelper.GetUnderlyingType(metadata.ModelType);
            _model = model;
            _metadata = metadata;
        }

        /// <summary>
        /// Gets or sets the model container object.
        /// </summary>
        internal object Container { get; set; }

        private FWRequestContext _requestContext;
        private Type _modelType;
        private object _model;
        private ModelMetadata _metadata;
    }
}
