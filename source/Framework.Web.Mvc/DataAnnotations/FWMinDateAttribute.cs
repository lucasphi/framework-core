using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using System;

namespace Framework.Web.Mvc.DataAnnotations
{
    /// <summary>
    /// Sets the minimum date for a datepicker control.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class FWMinDateAttribute : FWDataAnnotationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FWMinDateAttribute"/> class.
        /// </summary>
        public FWMinDateAttribute()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWMinDateAttribute"/> class.
        /// </summary>
        /// <param name="year">The absolute minimum year.</param>
        /// <param name="month">The absolute minimum month.</param>
        /// <param name="day">The absolute minimum day.</param>
        public FWMinDateAttribute(int year, int month, int day)
            : this(null, year, month, day)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWMinDateAttribute"/> class.
        /// </summary>
        /// <param name="datepicker">The minimum value DatePicker name.</param>
        public FWMinDateAttribute(string datepicker)
            : this(datepicker, 0, 0, 0)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWMinDateAttribute"/> class.
        /// </summary>
        /// <param name="datepicker">The minimum value DatePicker name.</param>
        /// <param name="year">The absolute minimum year.</param>
        /// <param name="month">The absolute minimum month.</param>
        /// <param name="day">The absolute minimum day.</param>
        public FWMinDateAttribute(string datepicker, int year, int month, int day)
        {
            DatePicker = datepicker;
            AbsoluteDate = (year, month, day);
        }

        /// <summary>
        /// Gets the minimum value DatePicker name.
        /// </summary>
        public string DatePicker { get; private set; }

        /// <summary>
        /// Gets or sets the absolute min date.
        /// </summary>
        internal (int Year, int Month, int Day) AbsoluteDate { get; private set; }

        /// <inheritdoc />
        public sealed override void OnMetadataCreated(DisplayMetadata metadata)
        {
            metadata.AdditionalValues.Add(nameof(FWMinDateAttribute), this);
        }
    }
}