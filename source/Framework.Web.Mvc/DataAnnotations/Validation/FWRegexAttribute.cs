using Framework.Web.Mvc.Metadata;
using Framework.Web.Mvc.Resources;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace Framework.Web.Mvc.DataAnnotations
{
    /// <summary>
    /// Specifies the regex pattern of a given input.
    /// </summary>
    public class FWRegexAttribute : ValidationAttribute, IFWMetadataAware
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FWRegexAttribute" />.
        /// </summary>
        /// <param name="pattern">The regex pattern.</param>
        public FWRegexAttribute(string pattern)
        {
            Pattern = pattern;
        }

        /// <summary>
        /// Validates the specified value with respect to the current validation attribute.
        /// </summary>
        /// <param name="value">The value of the object to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>true if the specified value is valid; otherwise, false.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var str = value?.ToString();

            // This validator does not handles null inputs.
            // If the input string is null, the field is either valid or should be required.
            if (str != null)
            {
                Regex regex = new Regex(Pattern);
                if (!regex.IsMatch(str))
                {
                    var errorMesasage = ErrorMessage ??
                                        string.Format(ViewResources.Validation_Regex, validationContext.DisplayName);
                    return new ValidationResult(errorMesasage);
                }
            }

            return ValidationResult.Success;
        }

        /// <summary>
        /// Gets the regex pattern.
        /// </summary>
        public string Pattern { get; private set; }

        /// <summary>
        /// Provides metadata to the model metadata creation process.
        /// </summary>
        /// <param name="metadata">The model metadata.</param>
        public void OnMetadataCreated(DisplayMetadata metadata)
        {
            metadata.AdditionalValues.Add(nameof(FWRegexAttribute), this);
        }
    }
}
