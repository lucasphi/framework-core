using Framework.Web.Mvc.DataAnnotations;
using Framework.Web.Mvc.Html.Controls.Forms.Textbox;
using Framework.Web.Mvc.Html.Elements;
using Framework.Web.Mvc.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Globalization;

namespace Framework.Web.Mvc.Html.Controls
{
    /// <summary>
    /// Represents a Datepicker control.
    /// </summary>
    public class FWDatepickerControl : FWInputControl
    {
        private static readonly string _iconName = "far fa-calendar-alt";

        /// <summary>
        /// Configures the control based on the model metadata.
        /// </summary>
        /// <param name="metadata">The model metadata.</param>
        protected override void ReadMetadata(ModelMetadata metadata)
        {
            base.ReadMetadata(metadata);

            if (metadata.AdditionalValues.ContainsKey(nameof(FWMinDateAttribute)))
            {
                var annotation = (FWMinDateAttribute)metadata.AdditionalValues[nameof(FWMinDateAttribute)];
                _minDateControl = annotation.DatePicker;

                if (annotation.AbsoluteDate.Year > 0 && annotation.AbsoluteDate.Month > 0 && annotation.AbsoluteDate.Day > 0)
                {
                    _minDate = new DateTime(annotation.AbsoluteDate.Year, annotation.AbsoluteDate.Month, annotation.AbsoluteDate.Day);
                }

                // If the property has a MinDate attribute with no target or specific date, use datetime.today
                if (_minDateControl == null && !_minDate.HasValue)
                {
                    _minDate = DateTime.Today;
                }
            }

            if (metadata.AdditionalValues.ContainsKey(nameof(FWMaxDateAttribute)))
            {
                var annotation = (FWMaxDateAttribute)metadata.AdditionalValues[nameof(FWMaxDateAttribute)];
                _maxDateControl = annotation.DatePicker;

                if (annotation.AbsoluteDate.Year > 0 && annotation.AbsoluteDate.Month > 0 && annotation.AbsoluteDate.Day > 0)
                {
                    _maxDate = new DateTime(annotation.AbsoluteDate.Year, annotation.AbsoluteDate.Month, annotation.AbsoluteDate.Day);
                }

                // If the property has a MaxDate attribute with no target or specific date, use datetime.today
                if (_maxDateControl == null && !_maxDate.HasValue)
                {
                    _maxDate = DateTime.Today;
                }
            }
        }

        /// <summary>
        /// Creates the control main element.
        /// </summary>
        /// <returns></returns>
        protected override IFWHtmlElement CreateControl()
        {
            var element = new FWDivElement();
            element.MergeAttributes(Attributes);
            if (!string.IsNullOrWhiteSpace(CustomCss))
                element.AddCssClass(CustomCss);

            element.Id = Id;
            element.DataType = "fw-datepicker";            

            element.AddCssClass("m-form__group form-group date date-picker");

            if (DisplayLabel)
            {
                var label = new FWLabelControl(Name, DisplayName, IsRequired, Tooltip);
                label.AddCssClass("control-label");
                element.Add(label.ToString());
            }

            var input = CreateDatepickerInput(element);
            //Adds the control icon
            var icon = new FWTextboxIcon(_iconName);
            var iconPlaceholder = icon.Create(input);
            element.Add(iconPlaceholder.ToString());

            if (_minDate.HasValue)
                element.Attributes.Add("data-mindate", _minDate.Value.ToString("yyyy-MM-dd"));

            if (_maxDate.HasValue)
                element.Attributes.Add("data-maxdate", _maxDate.Value.ToString("yyyy-MM-dd"));

            if (_minDateControl != null)
                element.Attributes.Add("data-mindate-target", _minDateControl);

            if (_maxDateControl != null)
                element.Attributes.Add("data-maxdate-target", _maxDateControl);

            return element;
        }

        private FWInputElement CreateDatepickerInput(FWDivElement element)
        {
            FWInputElement input = CreateInput(element);            
            input.AddCssClass("form-control datetimepicker-input");

            if (!string.IsNullOrWhiteSpace(_placeholder))
                input.Attributes.Add("placeholder", _placeholder);

            input.Attributes.Add("data-rule-required", IsRequired.ToString().ToLower());
            input.Attributes.Add("data-msg-required", string.Format(ViewResources.Validation_Required, DisplayName));

            if (DataBind)
            {
                DataBind.AddMainBind(FWBindConfiguration.DATEPICKER);
                input.DataBind = DataBind.CreateBind();
            }
            else if (Model != null)
            {                
                input.Value = (!_hasTimePicker) ?
                                    ((DateTime)Model).ToString("yyyy-MM-dd") :
                                    ((DateTime)Model).ToString("yyyy-MM-ddTHH:mm:ss");
            }

            if (IsReadOnly)
            {
                input.Attributes.Add("readonly", "readonly");
            }

            return input;
        }

        private FWInputElement CreateInput(FWDivElement element)
        {
            var isMobile = RequestContext.HttpContext.Request.IsMobileDevice();

            FWInputElement input;
            if (!isMobile)
            {
                // Format dates for Moment.js
                element.Attributes.Add("data-datelanguage", CultureInfo.CurrentUICulture.Name);                
                element.Attributes.Add("data-date-format", (_hasTimePicker) ? "L LT" : "L");

                input = new FWInputElement(Name, FWInputType.Textbox)
                {
                    Id = $"{Id}_input"
                };
                input.Attributes.Add("data-toggle", "datetimepicker");
                input.Attributes.Add("data-target", $"#{input.Id}");
            }
            else if (_hasTimePicker)
            {
                input = new FWInputElement(Name, FWInputType.Datetime)
                {
                    Id = $"{Id}_input"
                };
            }
            else
            {
                input = new FWInputElement(Name, FWInputType.Date)
                {
                    Id = $"{Id}_input"
                };
            }                

            return input;
        }


        /// <summary>
        /// Defines if the datepicker has a timepicker.
        /// </summary>
        /// <param name="hasTime">True to create the time picker.</param>
        /// <returns>The fluent configurator object.</returns>
        public FWDatepickerControl TimePicker(bool hasTime = true)
        {
            _hasTimePicker = hasTime;
            return this;
        }

        /// <summary>
        /// Sets the control placeholder.
        /// </summary>
        /// <param name="placeholder">The placeholder text.</param>
        /// <returns>The fluent configurator object.</returns>
        public FWDatepickerControl PlaceHolder(string placeholder = null)
        {
            if (placeholder != null)
            {
                _placeholder = placeholder;
            }
            else
            {
                _placeholder = GetModelResource("Placeholder");
            }
            return this;
        }

        /// <summary>
        /// Configures if the control label should be displayed or not.
        /// </summary>
        /// <param name="hideLabel">True to hide the label.</param>
        /// <returns>The fluent configurator object.</returns>
        public FWDatepickerControl HideLabel(bool hideLabel = true)
        {
            DisplayLabel = !hideLabel;
            return this;
        }

        /// <summary>
        /// Creates a new DatePicker control.
        /// </summary>
        /// <param name="requestContext">The request helper.</param>
        /// <param name="model">The current model.</param>
        /// <param name="metadata">The model metadata.</param>
        public FWDatepickerControl(FWRequestContext requestContext, object model, ModelMetadata metadata)
            : base(requestContext, model, metadata)
        { }

        private bool _hasTimePicker = false;
        private string _placeholder;

        private DateTime? _minDate;
        private DateTime? _maxDate;
        private string _minDateControl;
        private string _maxDateControl;
    }
}
