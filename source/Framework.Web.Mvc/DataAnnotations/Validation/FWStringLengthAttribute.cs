using Framework.Web.Mvc.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Framework.Web.Mvc.Resources;
using System.IO;
using System.Linq;

namespace Framework.Web.Mvc.DataAnnotations
{
    /// <summary>
    /// Specifies the length of a given input.
    /// </summary>
    public class FWStringLengthAttribute : ValidationAttribute, IFWMetadataAware
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FWStringLengthAttribute" />.
        /// </summary>
        /// <param name="maximumLength">The maximum length.</param>
        /// <param name="minimumLength">The minimum length</param>
        public FWStringLengthAttribute(int maximumLength = 0, int minimumLength = 0)
        {
            MaximumLength = maximumLength;
            MinimumLength = minimumLength;
        }

        /// <summary>
        /// Validates the specified value with respect to the current validation attribute.
        /// </summary>
        /// <param name="value">The value of the object to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>true if the specified value is valid; otherwise, false.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string str = value?.ToString();

            // This validator does not handles null inputs.
            // If the input string is null, the field is either valid or should be required.
            if (str != null)
            {
                // The validation message is not user friendly and should be properly validated by the page script.
                // If the value is invalid, something went wrong. Either there is a script error that allowed to post invalid fields or
                // someone is trying to force an invalid value.
                if (MinimumLength > 0 && str.Length < MinimumLength)
                {
                    var minLengthErrorMessage = ErrorMessage ??
                                                string.Format(ViewResources.Validation_MinLength, validationContext.DisplayName, MinimumLength);
                    return new ValidationResult(minLengthErrorMessage);
                }

                if (MaximumLength > 0 && str.Length > MaximumLength)
                {
                    var maxLengthErrorMessage = ErrorMessage ??
                                                string.Format(ViewResources.Validation_MaxLength, validationContext.DisplayName, MaximumLength);

                    return new ValidationResult(maxLengthErrorMessage);
                }
            }

            return ValidationResult.Success;
        }

        /// <summary>
        /// Gets the maximum length.
        /// </summary>
        public int MaximumLength { get; private set; }

        /// <summary>
        /// Gets the minimum length.
        /// </summary>
        public int MinimumLength { get; private set; }

        /// <summary>
        /// Provides metadata to the model metadata creation process.
        /// </summary>
        /// <param name="metadata">The model metadata.</param>
        public void OnMetadataCreated(DisplayMetadata metadata)
        {
            metadata.AdditionalValues.Add(nameof(FWStringLengthAttribute), this);
        }
    }
}
