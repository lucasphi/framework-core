using Framework.Core;
using Framework.Core.Helpers;
using Framework.Web.Mvc.DataAnnotations;
using Framework.Web.Mvc.Helper;
using Framework.Web.Mvc.Html.Elements;
using Framework.Web.Mvc.Models;
using Framework.Web.Mvc.Resources;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Web.Mvc.Html.Controls
{
    /// <summary>
    /// Represents a Select control.
    /// </summary>
    public class FWSelectControl : FWInputControl
    {
        /// <summary>
        /// Configures the control based on the model metadata.
        /// </summary>
        /// <param name="metadata">The model metadata.</param>
        protected override void ReadMetadata(ModelMetadata metadata)
        {
            base.ReadMetadata(metadata);

            if (metadata.AdditionalValues.ContainsKey(nameof(FWSelectAttribute)))
            {
                var selectAttr = (metadata.AdditionalValues[nameof(FWSelectAttribute)] as FWSelectAttribute);

                // If the control already has a datasource, that means its a boolean or an enum. Do not let user change the datasource.
                if (_datasourceName == null)
                    _datasourceName = selectAttr.DataSource;
            }
        }

        /// <summary>
        /// Creates the control main element.
        /// </summary>
        /// <returns></returns>
        protected override IFWHtmlElement CreateControl()
        {
            FWDivElement element = new FWDivElement();
            element.MergeAttributes(Attributes);
            if (!string.IsNullOrWhiteSpace(CustomCss))
                element.AddCssClass(CustomCss);

            element.Id = Id;
            element.DataType = "fw-select";
            element.AddCssClass("m-form__group form-group");
            element.Attributes.Add("data-language", FWGlobalizationHelper.CurrentLocaleName);

            if (DisplayLabel)
            {
                FWLabelControl label = new FWLabelControl(Name, DisplayName, IsRequired, Tooltip);
                label.AddCssClass("control-label");
                element.Add(label.ToString());
            }

            FWSelectElement select = CreateSelect();
            element.Add(select);

            return element;
        }

        private FWSelectElement CreateSelect()
        {
            var select = new FWSelectElement(Name);
            select.AddCssClass("m-select2 form-control");

            select.Attributes.Add("data-rule-required", IsRequired.ToString().ToLower());
            select.Attributes.Add("data-msg-required", string.Format(ViewResources.Validation_Required, DisplayName));
            select.Attributes.Add("data-minimumresultsforsearch", _minimumResultsForSearch.ToString());

            //Adds a width 100% to the select to make select2 pluging use all available space
            select.Attributes.Add("style", "width:100%");

            if (IsReadOnly)
            {
                select.Attributes.Add("readonly", "readonly");
            }

            if (_multiple)
            {
                select.Attributes.Add("multiple", "multiple");
            }

            if (!IsRequired)
            {
                string placeholder = GetModelResource("Placeholder", out bool resourceNotFound);
                select.Attributes.Add("data-placeholder", resourceNotFound ? ViewResources.Select_SelectOption : placeholder);
            }

            if (DataBind)
            {
                DataBind.Add("options", $"ds{_datasourceName}", true);
                DataBind.Add("optionsValue", "'id'");
                DataBind.Add("optionsText", "'value'");
                DataBind.AddMainBind(!_multiple ? FWBindConfiguration.VALUE : FWBindConfiguration.SELECTED_OPTIONS);
                select.DataBind = DataBind.CreateBind();
            }
            else if (_datasource != null)
            {
                foreach (var item in _datasource)
                {
                    var isSelected = _selected != null && item.Id == _selected.ToString();
                    select.Add(item, isSelected);
                }
            }

            select.Attributes.Add("data-allowclear", _allowClear.ToString().ToLower());
            return select;
        }

        /// <summary>
        /// Sets the number of results required to display the search field. Negative numbers will hide it.
        /// </summary>
        /// <param name="results">The required results.</param>
        /// <returns>The fluent configurator object.</returns>
        public FWSelectControl MinimumResultsForSearch(sbyte results)
        {
            _minimumResultsForSearch = results;
            return this;
        }

        /// <summary>
        /// Hides the search field.
        /// </summary>
        /// <param name="hideSearch">Should hide the search or not.</param>
        /// <returns>The fluent configurator object.</returns>
        public FWSelectControl HideSearch(bool hideSearch = true)
        {
            _minimumResultsForSearch = -1;
            return this;
        }

        /// <summary>
        /// Configures if the control label should be displayed or not.
        /// </summary>
        /// <param name="hideLabel">True to hide the label.</param>
        /// <returns>The fluent configurator object.</returns>
        public FWSelectControl HideLabel(bool hideLabel = true)
        {
            DisplayLabel = !hideLabel;
            return this;
        }

        /// <summary>
        /// Allows multiple selections.
        /// </summary>
        /// <returns>The fluent configurator object.</returns>
        public FWSelectControl Multiple(bool multiple)
        {
            _multiple = multiple;
            _allowClear = !multiple;
            return this;
        }

        /// <summary>
        /// Creates a new Select control.
        /// </summary>
        /// <param name="requestContext">The request helper.</param>
        /// <param name="model">The current model.</param>
        /// <param name="metadata">The model metadata.</param>
        /// <param name="container">The model container object.</param>
        public FWSelectControl(FWRequestContext requestContext, object model, ModelMetadata metadata, object container = null)
            : base(requestContext, model, metadata)
        {
            if (metadata.UnderlyingOrModelType == FWKnownTypes.Bool)
            {
                _datasource = new List<IFWDatasourceItem>
                {
                    new FWDatasourceItem() { Id = "true", Value = GetModelResource("TRUE") },
                    new FWDatasourceItem() { Id = "false", Value = GetModelResource("FALSE") }
                };

                if (model != null)
                    _selected = model.ToString();                
            }
            else if (FWReflectionHelper.IsEnum(metadata.ModelType))
            {
                var enumDictionary = FWEnumHelper.GetEnumAsDictonary(metadata.ModelType);
                var datasource = new List<IFWDatasourceItem>();
                // Gets the resource values for the enum items.
                foreach (var item in enumDictionary)
                {
                    datasource.Add(new FWDatasourceItem() { Id = item.Key, Value = GetModelResource(item.Value) });
                }
                _datasource = datasource;
                
                _selected = FWEnumHelper.GetValues(model as Enum).FirstOrDefault();                
            }
            else
            {
                // Looks for the datasource inside the view model.
                _datasource = FWDatasourceHelper.GetDatasourceFromModel(_datasourceName, requestContext.Model);

                // If the datasource is not found, try to look for it inside the container object.
                if (_datasource == null)
                    _datasource = FWDatasourceHelper.GetDatasourceFromModel(_datasourceName, container);

                if (model != null)
                    _selected = model.ToString();
            }

            if (DataBind)
            {
                requestContext.HttpContext.AddDatasource(metadata.PropertyName, metadata.ContainerMetadata.ModelType, _datasource);
                _datasourceName = metadata.PropertyName;
            }

            _allowClear = !IsRequired;
        }
        
        private sbyte _minimumResultsForSearch = 7;
        private bool _allowClear;
        private bool _multiple;

        private object _selected;
        private IEnumerable<IFWDatasourceItem> _datasource;
        private string _datasourceName;
    }
}
