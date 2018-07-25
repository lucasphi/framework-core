using Framework.Core;
using Framework.Model;
using Framework.Web.Mvc.Exceptions;
using Framework.Web.Mvc.Html.Elements;
using Framework.Web.Mvc.Models;
using Framework.Web.Mvc.Resources;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Framework.Web.Mvc.Html.Controls
{
    /// <summary>
    /// Represents a Checkbox control.
    /// </summary>
    public class FWCheckboxControl : FWInputControl
    {
        /// <summary>
        /// Creates the control main element.
        /// </summary>
        /// <returns>The control IFWHtmlElement interface.</returns>
        protected override IFWHtmlElement CreateControl()
        {
            var element = new FWDivElement();
            element.MergeAttributes(Attributes);
            if (!string.IsNullOrWhiteSpace(CustomCss))
                element.AddCssClass(CustomCss);

            element.Id = Id;
            element.DataType = "fw-checkbox";
            element.AddCssClass("m-form__group form-group");

            if (_isBoolean)
            {
                element.Attributes.Add("data-text-true", ViewResources.Yes);
                element.Attributes.Add("data-text-false", ViewResources.No);
            }

            if (DisplayLabel)
            {
                FWLabelControl label = new FWLabelControl(Name, DisplayName, IsRequired, Tooltip);
                element.Add(label.ToString());
            }

            var innerDiv = new FWDivElement();
            innerDiv.AddCssClass((_orientation == FWOrientation.Vertical) ? "m-checkbox-list" : "m-checkbox-inline");

            for(int i = 0; i < _values.Count; i++)
            {   
                innerDiv.Add(CreateCheckbox(_values.ElementAt(i).Key, _values.ElementAt(i).Value, $"{Name}_{i}"));
            }

            element.Add(innerDiv);

            return element;
        }

        private FWLabelElement CreateCheckbox(string key, string value, string id)
        {
            var label = new FWLabelElement();
            label.AddCssClass("m-checkbox m-checkbox--square");

            var checkbox = new FWCheckboxElement(Name)
            {
                Id = TagBuilder.CreateSanitizedId(id, "_"),
                IsChecked = _selected.Contains(key),
                Value = key
            };
            checkbox.Attributes.Add("data-rule-required", IsRequired.ToString().ToLower());
            checkbox.Attributes.Add("data-rule-minlength", _minimumOptions.ToString());
            checkbox.Attributes.Add("data-msg-required", string.Format(ViewResources.Validation_Required_Min, _minimumOptions));

            if (IsDisabled)
            {
                label.AddCssClass("m-checkbox--disabled");
                checkbox.Attributes.Add("disabled", "disabled");
            }

            if (DataBind)
            {
                DataBind.AddMainBind(FWBindConfiguration.CHECKED);
                checkbox.DataBind = DataBind.CreateBind();
            }

            label.Add(checkbox);
            label.Add(value);
            label.Add(new FWSpanElement());

            return label;
        }

        /// <summary>
        /// Sets the checkbox orientation.
        /// </summary>
        /// <param name="orientation">The orientation option.</param>
        /// <returns>The fluent configurator object.</returns>
        public FWCheckboxControl Orientation(FWOrientation orientation)
        {
            _orientation = orientation;
            return this;
        }

        /// <summary>
        /// Configures if the control label should be displayed or not.
        /// </summary>
        /// <param name="hideLabel">True to hide the label.</param>
        /// <returns>The fluent configurator object.</returns>
        public FWCheckboxControl HideLabel(bool hideLabel = true)
        {
            DisplayLabel = !hideLabel;
            return this;
        }

        /// <summary>
        /// Creates a new Checkbox control.
        /// </summary>
        /// <param name="requestContext">The request helper.</param>
        /// <param name="model">The current model.</param>
        /// <param name="metadata">The model metadata.</param>
        public FWCheckboxControl(FWRequestContext requestContext, object model, ModelMetadata metadata)
            : base(requestContext, model, metadata)
        {
            if (FWReflectionHelper.CheckType<bool>(metadata.ModelType))
            {
                _isBoolean = true;
                _values = new Dictionary<string, string>
                {
                    { "true", DisplayName }
                };
                if (model != null)
                    _selected.Add(model.ToString());

                //If the property is boolean, there is no sense in being required.
                if (metadata.ModelType == FWKnownTypes.Bool)
                    IsRequired = false;

                HideLabel();
            }
            else if (FWReflectionHelper.IsEnum(metadata.ModelType))
            {
                var enumDictionary = FWEnumHelper.GetEnumAsDictonary(metadata.ModelType);
                _values = new Dictionary<string, string>();
                // Gets the resource values for the enum items.
                foreach (var item in enumDictionary)
                {
                    _values.Add(item.Key, GetModelResource(item.Value));
                }
                _selected = FWEnumHelper.GetValues(model as Enum).ToList();
            }
            else
            {
                throw new FWMissingDatasourceException(Id);
            }
        }

        /// <summary>
        /// Creates a new Checkbox control.
        /// </summary>
        /// <param name="requestContext">The request helper.</param>
        /// <param name="model">The current model.</param>
        /// <param name="metadata">The model metadata.</param>
        /// <param name="datasource">The checkbox datasource.</param>
        public FWCheckboxControl(FWRequestContext requestContext, object model, ModelMetadata metadata, IEnumerable<FWDatasourceItem> datasource)
            : base(requestContext, model, metadata)
        {
            _values = new Dictionary<string, string>();
            foreach (var item in datasource)
            {
                _values.Add(item.Id, item.Value);
            }

            if (FWReflectionHelper.IsCollection(metadata.ModelType))
            {
                var list = model as IEnumerable;
                foreach (var item in list)
                {
                    _selected.Add(item.ToString());
                }
            }
            else
            {
                if (model != null)
                    _selected.Add(model.ToString());
            }
        }

        private List<string> _selected = new List<string>();
        private Dictionary<string, string> _values;

        private FWOrientation _orientation;

        private int _minimumOptions = 1;
        
        private bool _isBoolean;
    }
}
