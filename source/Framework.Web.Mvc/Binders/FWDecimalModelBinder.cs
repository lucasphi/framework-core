using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Framework.Web.Mvc.Binders
{
    /// <summary>
    /// Implements a binder for decimal values.
    /// </summary>
    public class FWDecimalModelBinder : IModelBinder
    {
        /// <summary>
        /// Attempts to bind a model.
        /// </summary>
        /// <param name="bindingContext">The Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingContext.</param>
        /// <returns>A System.Threading.Tasks.Task which will complete when the model binding process completes.</returns>
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null) throw new ArgumentNullException(nameof(bindingContext));

            // Check the value sent in
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult != ValueProviderResult.None)
            {
                bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

                // Attempt to parse the input value
                var valueAsString = valueProviderResult.FirstValue;

                var success = decimal.TryParse(RemoveCurrency(valueAsString), out decimal result);
                if (success)
                {
                    bindingContext.Result = ModelBindingResult.Success(result);                    
                }
                else
                {
                    bindingContext.ModelState[bindingContext.ModelName].Errors.Add("Could not parse decimal value.");
                    bindingContext.ModelState[bindingContext.ModelName].ValidationState = ModelValidationState.Invalid;
                    bindingContext.Result = ModelBindingResult.Failed();
                }
            }
            
            return Task.CompletedTask;
        }

        private string RemoveCurrency(string value)
        {
            var regex = new Regex(@"([\d,.]+)");

            var match = regex.Match(value);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            return value;
        }
    }
}
