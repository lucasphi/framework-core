using Framework.Core;
using Framework.Web.Mvc.Html.Controls.Inputs.Detail;
using Framework.Web.Mvc.Html.Controls.Table;
using Framework.Web.Mvc.Html.Elements;
using Framework.Web.Mvc.Resources;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Framework.Web.Mvc.Html.Controls
{
    /// <summary>
    /// Represents a DetailGrid control.
    /// </summary>
    public class FWDetailTableControl : FWTableControl
    {
        /// <summary>
        /// Creates the control main element.
        /// </summary>
        /// <returns>The IFWHtmlElement object representation of the control.</returns>
        protected override IFWHtmlElement CreateControl()
        {
            CreateLineActions();

            var detailControl = new FWPortletControl(Id, GetModelResource(OriginalId));
            detailControl.AddCssClass("fw-detail-table");
            detailControl.DataType("fw-detailcontrol");

            detailControl.Attributes.Add("data-model", ListType.Name);

            detailControl.AddAction(FWDetailHelper.CreateUndoButton());

            detailControl.Required(Metadata.IsRequired);

            detailControl.Footer(FWDetailHelper.CreateAddButton().ToString());

            detailControl.Attributes.AddRange(Attributes);
            if (!string.IsNullOrWhiteSpace(CustomCss))
                detailControl.AddCssClass(CustomCss);
            // Clears custom attributes and css to prevent adding to the base table control.
            Attributes.Clear();
            ClearCss();
            
            var grid = base.CreateControl() as FWTagBuilder;
            grid.DataType = "fw-detailcontrol-body";
            grid.Attributes.Add("role", "grid");

            detailControl.Add(grid);

            if (Metadata.IsRequired)
            {
                var validationDiv = new FWDivElement();
                {
                    validationDiv.DataType = "fw-detailvalidation";
                    validationDiv.AddCssClass("m-form__group form-group");

                    //Creates the validation input
                    var validationInput = new FWInputElement("DetailValidation", FWInputType.Hidden);
                    validationInput.Attributes.Add("data-rule-requireddetail", Metadata.IsRequired.ToString().ToLower());
                    validationInput.Attributes.Add("data-msg-requireddetail", string.Format(ViewResources.Validation_Required_Detail, GetModelResource(OriginalId)));
                    validationInput.Attributes.Add("data-skipvalidation", "false");

                    validationDiv.Add(validationInput);

                    detailControl.Add(validationDiv);
                }
            }

            detailControl.Add(RegisterTemplate());

            return detailControl;
        }

        private string RegisterTemplate()
        {
            var line = new FWTableLineElement();
            line.Attributes.Add("data-control-child", "table");
            line.AddCssClass("row");

            //Sets an invalid index for the model line.
            base.Index = "{{:index}}";

            foreach (var column in Columns)
            {
                var cell = base.CreateLineCell(_model, column);
                line.Add(cell);
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<script id=\"template-{0}\" type=\"text/x-jsrender\">", Id);
            sb.Append(line.ToString());
            sb.Append("</script>");

            return sb.ToString();
        }

        private void CreateLineActions()
        {
            var column = new FWTableColumnCustom(new List<string> { FWDetailHelper.CreateRemoveButton().ToString() })
            {
                Css = "col-sm-1 m--align-right",
                Order = int.MaxValue
            };
            AddColumn(column);
        }

        /// <summary>
        /// Creates a new DetailTable control.
        /// </summary>
        /// <param name="requestContext">The request helper.</param>
        /// <param name="model">The current model.</param>
        /// <param name="metadata">The model metadata.</param>
        public FWDetailTableControl(FWRequestContext requestContext, IEnumerable model, ModelMetadata metadata)
            : base(requestContext, model, metadata)
        {
            // Sets the sizes of columns of the table to be defined by the user.
            AutoSizeColumns = false;

            if (_model == null)
                _model = Activator.CreateInstance(metadata.ModelType.GetGenericArguments()[0]);
        }

        /// <summary>
        /// Creates a table editable column.
        /// </summary>
        /// <param name="metadata">The model metadata.</param>
        /// <param name="property">The proeprty info.</param>
        /// <returns>A FWTableColumn object.</returns>
        protected override FWTableColumn CreateColumn(ModelMetadata metadata, PropertyInfo property)
        {
            var column = new FWTableColumnEditable(metadata)
            {
                Header = FWStringLocalizer.GetModelResource(property.Name, ListType.Name),
                ModelPrefix = base.Metadata.PropertyName //(metadata.AdditionalValues["parentMetadata"] as ModelMetadata).PropertyName;
            };
            column.Attributes.Add("data-type", "fw-detailcontrol-item");

            if (metadata.IsRequired())
                column.Header = $"<label class=\"control-label\"><span class=\"required\">*</span>{column.Header}</label>";

            return column;
        }

        private object _model;
    }
}
