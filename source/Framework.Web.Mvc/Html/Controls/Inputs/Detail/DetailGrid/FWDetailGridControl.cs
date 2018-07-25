using Framework.Core;
using Framework.Web.Mvc.DataAnnotations;
using Framework.Web.Mvc.Exceptions;
using Framework.Web.Mvc.Helper;
using Framework.Web.Mvc.Html.Controls.Inputs.Detail;
using Framework.Web.Mvc.Html.Elements;
using Framework.Web.Mvc.Resources;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Framework.Web.Mvc.Html.Controls
{
    /// <summary>
    /// Represents a DetailGrid control.
    /// </summary>
    public class FWDetailGridControl : FWInputControl
    {
        /// <summary>
        /// Gets the model resource for the given id.
        /// </summary>
        /// <returns>The resource value.</returns>
        protected override string GetModelResource()
        {
            return FWStringLocalizer.GetModelResource(OriginalId, _parentType.Name);
        }

        /// <summary>
        /// Creates the control main element.
        /// </summary>
        /// <returns>The IFWHtmlElement object representation of the control.</returns>
        protected override IFWHtmlElement CreateControl()
        {
            var detailControl = new FWPortletControl(Id, GetModelResource());
            detailControl.AddCssClass("fw-detail-grid");
            detailControl.DataType("fw-detailcontrol");

            detailControl.Attributes.Add("data-model", _listType.Name);

            detailControl.AddAction(FWDetailHelper.CreateUndoButton());

            detailControl.Required(IsRequired);

            detailControl.Footer(FWDetailHelper.CreateAddButton().ToString());

            detailControl.Attributes.AddRange(Attributes);
            if (!string.IsNullOrWhiteSpace(CustomCss))
                detailControl.AddCssClass(CustomCss);

            var body = new FWDivElement()
            {
                DataType = "fw-detailcontrol-body"
            };
            if (Model != null)
            {
                int i = 0;
                var list = Model as IEnumerable;
                foreach (var item in list)
                {
                    body.Add(CreateBody(item, i.ToString()));
                    i++;
                }
            }

            detailControl.Add(body);

            if (IsRequired)
            {
                var validationDiv = new FWDivElement();
                {
                    validationDiv.DataType = "fw-detailvalidation";
                    validationDiv.AddCssClass("m-form__group form-group");

                    //Creates the fileupload validation input
                    var validationInput = new FWInputElement("DetailValidation", FWInputType.Hidden);
                    validationInput.Attributes.Add("data-rule-requireddetail", "true");
                    validationInput.Attributes.Add("data-msg-requireddetail", string.Format(ViewResources.Validation_Required_Detail, GetModelResource()));
                    validationInput.Attributes.Add("data-skipvalidation", "false");

                    validationDiv.Add(validationInput);

                    detailControl.Add(validationDiv);
                }
            }

            detailControl.Add(RegisterTemplate());

            return detailControl;
        }

        private FWDivElement CreateBody(object item, string index)
        {
            var body = new FWDivElement();
            body.Attributes.Add("data-control-child", "grid");
            body.AddCssClass("row");

            // Gets the metadata for the generic list type.
            ModelMetadata listMetadata = RequestContext.MetadataProvider.GetMetadataForType(_listType);

            // Loop through all properties and creates the inputs.
            foreach (var property in _properties)
            {
                body.Add(CreateBodyControl(item, index, listMetadata, property));
            }

            // Adds the action buttons to the body
            body.Add(CreateActionButtons());

            return body;
        }

        private FWDivElement CreateBodyControl(object item, string index, ModelMetadata listMetadata, PropertyInfo property)
        {
            // Gets the metadata for the current property
            var itemMetadata = listMetadata.Properties.Where(f => f.PropertyName == property.Name).First();

            // Get the property value.
            var itemModel = property.GetValue(item);

            FWDivElement detailControlItem = CreateItemElement(itemMetadata, item, itemModel, index);

            // Creates the input part of the body
            var div = new FWDivElement
            {
                DataType = "fw-cell-model"
            };

            var factory = new FWInputControlFactory(RequestContext, itemModel, itemMetadata)
            {
                Container = item
            };
            var control = factory.CreateControl();
            // Clears the sizes of the internal controls.
            control.Sizes.Clear();            
            control.Name = string.Format("{0}[{1}].{2}", _modelPrefix, index, itemMetadata.PropertyName);
            control.Id = string.Format("{0}_{1}{2}", _modelPrefix, itemMetadata.PropertyName, index);

            if (control.DataBind)
            {
                // Do not set a prefix for templates
                if (int.TryParse(index.ToString(), out int result))
                {
                    // Sets the bind prefix.
                    control.DataBind.BindPrefix = $"{_modelPrefix}()[{index}].";
                }
            }

            div.Add(control.ToString());

            detailControlItem.Add(div);

            return detailControlItem;
        }

        private FWDivElement CreateItemElement(ModelMetadata itemMetadata, object item, object itemModel, string index)
        {
            var detailControlItem = new FWDivElement();
            detailControlItem.Attributes.Add("data-type", "fw-detailcontrol-item");
            
            //Checks to see if the property is hidden
            if (!itemMetadata.AdditionalValues.ContainsKey(nameof(FWHiddenAttribute)))
            {
                // Checks if the property has a size.
                if (itemMetadata.AdditionalValues.ContainsKey(nameof(FWSizeAttribute)))
                {
                    // Sets the size of the line and then, marks all inner controls as full width.
                    var sizeAttr = itemMetadata.AdditionalValues[nameof(FWSizeAttribute)] as List<FWSizeAttribute>;
                    foreach (var size in sizeAttr)
                    {
                        detailControlItem.AddCssClass(FWGridSizeHelper.GetCss(size.Size, size.Device));
                        if (!_totalColumnSize.ContainsKey(size.Device))
                            _totalColumnSize[size.Device] = 0;
                        _totalColumnSize[size.Device] += (int)size.Size;
                    }
                }              
            }
            else
            {
                detailControlItem.AddCssClass("hidden");
            }

            return detailControlItem;
        }

        private FWDivElement CreateActionButtons()
        {
            var actions = new FWDivElement();
            actions.AddCssClass("fw-actions");
            foreach (var actionSizes in _totalColumnSize)
            {
                // Calculates the remaining grid size in the last row.
                var colSize = FWGridSizeHelper.GRID_COLUMNS - (actionSizes.Value % FWGridSizeHelper.GRID_COLUMNS);
                actions.AddCssClass($"col-{FWGridSizeHelper.GetDeviceName(actionSizes.Key)}-{colSize}");
            }

            actions.Add(FWDetailHelper.CreateRemoveButton().ToString());

            return actions;
        }

        private string RegisterTemplate()
        {
            var templateObj = Activator.CreateInstance(_listType);
            var template = CreateBody(templateObj, "{{:index}}");

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<script id=\"template-{0}\" type=\"text/x-jsrender\">", Id);
            sb.Append(template.ToString());
            sb.Append("</script>");

            return sb.ToString();
        }

        /// <summary>
        /// Creates a new DetailGrid control.
        /// </summary>
        /// <param name="requestContext">The request helper.</param>
        /// <param name="model">The current model.</param>
        /// <param name="metadata">The model metadata.</param>
        public FWDetailGridControl(FWRequestContext requestContext, IEnumerable model, ModelMetadata metadata)
            : base(requestContext, model, metadata)
        {
            _listType = metadata.ModelType.GenericTypeArguments.FirstOrDefault();
            if (_listType == null)
                throw new FWInvalidModelException("Detail Grid model must be a generic list.");

            _properties = FWReflectionHelper.GetPublicProperties(_listType).Where(f => !f.IsDefined(typeof(FWDatasourceAttribute)));
            _modelPrefix = metadata.PropertyName;

            _parentType = metadata.ContainerType ?? metadata.ModelType;
        }
        
        private Type _listType;
        private Type _parentType;
        private IEnumerable<PropertyInfo> _properties;
        private string _modelPrefix;

        // Used to calculate the size of the button column.
        private Dictionary<FWDevice, int> _totalColumnSize = new Dictionary<FWDevice, int>();
    }
}
