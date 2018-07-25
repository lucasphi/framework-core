using System;

namespace Framework.Web.Mvc.DataAnnotations
{
    /// <summary>
    /// Marks the property as a MVVM datasource.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class FWDatasourceAttribute : FWDataAnnotationAttribute
    { }
}
