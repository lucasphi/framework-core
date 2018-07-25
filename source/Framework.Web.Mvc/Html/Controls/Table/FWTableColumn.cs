using Framework.Core;
using Framework.Web.Mvc.DataAnnotations;
using Framework.Web.Mvc.Exceptions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Reflection;

namespace Framework.Web.Mvc.Html.Controls.Table
{
    /// <summary>
    /// Represents a table column.
    /// </summary>
    public class FWTableColumn
    {
        /// <summary>
        /// Gets the column text value.
        /// </summary>
        /// <param name="requestContext">The request helper.</param>
        /// <param name="item">The column object item.</param>
        /// <param name="index">The line index.</param>
        /// <returns>The column text value.</returns>
        public virtual string GetValue(FWRequestContext requestContext, object item, object index)
        {
            var model = Property.GetValue(item);

            if (model != null)
            {
                try
                {
                    var label = new FWDisplayControl(requestContext, model, Metadata);
                    // Clears the sizes of the internal controls.
                    label.Sizes.Clear();
                    label.Attributes.Add("data-type", "fw-cell-text");
                    return label.ToString();
                }
                catch (FWMissingDatasourceException)
                {
                    return model.ToString();
                }
                
            }
            return string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the Framework.Web.Mvc.Html.Controls.Table.FWTableColumn class.
        /// </summary>
        protected FWTableColumn()
        { }
        /// <summary>
        /// Initializes a new instance of the Framework.Web.Mvc.Html.Controls.Table.FWTableColumn class.
        /// </summary>
        /// <param name="metadata">The model metadata.</param>
        public FWTableColumn(ModelMetadata metadata)
        {
            Metadata = metadata;

            //Checks to see if the property is hidden
            IsHidden = (metadata.AdditionalValues.ContainsKey(nameof(FWHiddenAttribute)));

            //Checks if the column is sortable
            if (metadata.AdditionalValues.ContainsKey(nameof(FWSortableAttribute)))
            {
                var attr = metadata.AdditionalValues[nameof(FWSortableAttribute)] as FWSortableAttribute;

                IsSortable = attr.AllowReorder;

                //Tries to get the sort name. If is it not set, use the property name.
                SortName = attr.Name ?? metadata.PropertyName;
                SortDirection = attr.SortDirection;
            }
        }

        /// <summary>
        /// Gets or sets the column header text.
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Gets or sets the model property for the column.
        /// </summary>
        public PropertyInfo Property { get; set; }
        
        /// <summary>
        /// Gets or sets the column visibility.
        /// </summary>
        public bool IsHidden { get; set; }

        /// <summary>
        /// Gets or sets the column sortable property.
        /// </summary>
        public bool IsSortable { get; set; }

        /// <summary>
        /// Gets or sets the column sorting name.
        /// </summary>
        public string SortName { get; set; }

        /// <summary>
        /// Gets or sets the column initial sorting direction.
        /// </summary>
        public FWSortDirection SortDirection { get; set; }

        /// <summary>
        /// Gets or sets the column custom CSS.
        /// </summary>
        public string Css { get; set; }

        /// <summary>
        /// Gets or sets the column order.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Gets or sets the column custom attributes;
        /// </summary>
        public AttributeDictionary Attributes { get; private set; } = new AttributeDictionary();

        /// <summary>
        /// Gets the model metadata.
        /// </summary>
        public ModelMetadata Metadata { get; private set; }
    }
}
