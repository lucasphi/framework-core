using Framework.Core;
using Framework.Model;
using Framework.Web.Mvc.DataAnnotations;
using Framework.Web.Mvc.Exceptions;
using Framework.Web.Mvc.Helper;
using Framework.Web.Mvc.Html.Elements;
using Framework.Web.Mvc.Resources;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Framework.Web.Mvc.Html.Controls
{
    /// <summary>
    /// Represents a Display control.
    /// </summary>
    public class FWDisplayControl : FWMetadataControl
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
        /// Gets the formatted model value.
        /// </summary>
        /// <returns>the formatted model value.</returns>
        public string GetValue()
        {
            return GetPropertyValue();
        }

        /// <summary>
        /// Creates the control main element.
        /// </summary>
        /// <returns>The IFWHtmlElement object representation of the control.</returns>
        protected override IFWHtmlElement CreateControl()
        {
            var element = new FWDivElement()
            {
                DataType = "fw-display"
            };
            element.AddCssClass("fw-display");
            element.MergeAttributes(Attributes);
            if (!string.IsNullOrWhiteSpace(CustomCss))
                element.AddCssClass(CustomCss);

            if (DisplayLabel)
            {
                element.Add(_metadata.DisplayName);
            }

            var span = new FWSpanElement()
            {
                DataType = "fw-display-text"
            };

            if (DataBind)
            {
                DataBind.Add(FWBindConfiguration.TEXT, Name);
                span.DataBind = DataBind.CreateBind();
            }
            else if (_model != null)
            {
                string text = GetPropertyValue();
                //Adds the text value to the element. In case text is null, adds the default ToString value.
                span.Add(text);
            }

            element.Add(span);

            return element;
        }

        private string GetPropertyValue()
        {
            switch (_modelType.Name)
            {
                case nameof(String):
                    return _model.ToString();
                case nameof(Boolean):
                    return GetCheckboxText();
                case nameof(DateTime):
                    return ((DateTime)_model).ToString("d");
            }

            if (FWReflectionHelper.IsEnum(_modelType))
            {
                if (_model == null)
                    return string.Empty;
                return FWStringLocalizer.GetModelResource($"{OriginalId}_{_model.ToString()}", _metadata.ContainerType?.Name);
            }

            if (_metadata.AdditionalValues.ContainsKey(nameof(FWSelectAttribute)))
            {
                return GetSelectText();
            }

            return _model.ToString();
        }

        private string GetSelectText()
        {
            // Gets the datasource name
            string datasourceName = (_metadata.AdditionalValues[nameof(FWSelectAttribute)] as FWSelectAttribute).DataSource;
            
            // Looks for the datasource inside the view model.
            var datasource = FWDatasourceHelper.GetDatasourceFromModel(datasourceName, _requestContext.Model);

            // If the datasource is not found, try to look for it inside the container object.
            if (datasource == null)
            {
                datasource = FWDatasourceHelper.GetDatasourceFromModel(datasourceName, Container);
            }

            if (datasource == null)
                throw new FWMissingDatasourceException("DataSource");

            return datasource.Where(f => f.Value.Equals(_model.ToString())).FirstOrDefault()?.Value.ToString();
        }

        private string GetCheckboxText()
        {
            if ((bool)_model)
            {
                return ViewResources.Yes;
            }
            return ViewResources.No;
        }

        /// <summary>
        /// Creates a new Display control.
        /// </summary>
        /// <param name="requestContext">The request helper.</param>
        /// <param name="model">The current model.</param>
        /// <param name="metadata">The model metadata.</param>
        public FWDisplayControl(FWRequestContext requestContext, object model, ModelMetadata metadata)
            : base(metadata)
        {
            Name = metadata.PropertyName;

            _requestContext = requestContext;
            _modelType = FWReflectionHelper.GetUnderlyingType(metadata.ModelType);
            _model = model;
            _metadata = metadata;
        }

        /// <summary>
        /// Gets or sets the property name of the Control.
        /// </summary>
        internal string Name { get; set; }

        /// <summary>
        /// Gets or sets the model container object.
        /// </summary>
        internal object Container { get; set; }

        /// <summary>
        /// Gets or sets if the control will display its label.
        /// </summary>
        public bool DisplayLabel { get; set; }

        /// <summary>
        /// Gets or sets the input a data-bind configuration.
        /// </summary>
        internal FWBindConfiguration DataBind { get; private set; } = new FWBindConfiguration();

        private FWRequestContext _requestContext;
        private Type _modelType;
        private object _model;
        private ModelMetadata _metadata;
    }
}
