using System;

namespace Framework.Web.Mvc.DataAnnotations
{
    /// <summary>
    /// Marks the property as a MVVM model property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class FWDataBindAttribute : FWDataAnnotationAttribute
    {
        /// <summary>
        /// Gets or sets the input data-bind value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the input data-bind expression.
        /// </summary>
        public string Expression { get; set; }
    }
}
