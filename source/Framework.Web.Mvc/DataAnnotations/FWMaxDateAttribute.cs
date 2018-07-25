using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using System;

namespace Framework.Web.Mvc.DataAnnotations
{
    /// <summary>
    /// Sets the maximum date for a datepicker control.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class FWMaxDateAttribute : FWDataAnnotationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FWMaxDateAttribute"/> class.
        /// </summary>
        public FWMaxDateAttribute()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWMaxDateAttribute"/> class.
        /// </summary>
        /// <param name="year">The absolute maximum year.</param>
        /// <param name="month">The absolute maximum month.</param>
        /// <param name="day">The absolute maximum day.</param>
        public FWMaxDateAttribute(int year, int month, int day)
            : this(null, year, month, day)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWMaxDateAttribute"/> class.
        /// </summary>
        /// <param name="datepicker">The maximum value DatePicker name.</param>
        public FWMaxDateAttribute(string datepicker)
            : this(datepicker, 0, 0, 0)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWMaxDateAttribute"/> class.
        /// </summary>
        /// <param name="datepicker">The maximum value DatePicker name.</param>
        /// <param name="year">The absolute maximum year.</param>
        /// <param name="month">The absolute maximum month.</param>
        /// <param name="day">The absolute maximum day.</param>
        public FWMaxDateAttribute(string datepicker, int year, int month, int day)
        {
            DatePicker = datepicker;
            AbsoluteDate = (year, month, day);
        }

        /// <summary>
        /// Gets the maximum value DatePicker name.
        /// </summary>
        public string DatePicker { get; private set; }

        /// <summary>
        /// Gets or sets the absolute max date.
        /// </summary>
        internal (int Year, int Month, int Day) AbsoluteDate { get; private set; }

        /// <inheritdoc />
        public sealed override void OnMetadataCreated(DisplayMetadata metadata)
        {
            metadata.AdditionalValues.Add(nameof(FWMaxDateAttribute), this);
        }
    }
}