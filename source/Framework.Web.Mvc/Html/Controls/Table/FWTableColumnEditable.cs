using Framework.Core;
using Framework.Web.Mvc.Html.Elements;
using Framework.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Framework.Web.Mvc.Html.Controls.Table
{
    /// <summary>
    /// Represents an editable table column.
    /// </summary>
    public class FWTableColumnEditable : FWTableColumn
    {
        /// <summary>
        /// Gets the column text value.
        /// </summary>
        /// <param name="requestContext">The request helper.</param>
        /// <param name="item">The column object item.</param>
        /// <param name="index">The line index.</param>
        /// <returns>The column text value.</returns>
        public override string GetValue(FWRequestContext requestContext, object item, object index)
        {
            var model = Property.GetValue(item);

            var div = new FWDivElement
            {
                DataType = "fw-cell-model"
            };

            var factory = new FWInputControlFactory(requestContext, model, Metadata)
            {
                Container = item
            };
            var control = factory.CreateControl();
            // Clears the sizes of the internal controls.
            control.Sizes.Clear();            
            control.Name = $"{ModelPrefix}[{index}].{Metadata.PropertyName}";
            control.Id = $"{ModelPrefix}_{Metadata.PropertyName}{index}";

            if (control.DataBind)
            {
                // Do not set a prefix for templates
                if (int.TryParse(index.ToString(), out int result))
                {
                    // Sets the bind prefix.
                    control.DataBind.BindPrefix = $"{ModelPrefix}()[{index}].";
                }
            }

            control.DisplayLabel = false;

            div.Add(control.ToString());

            return div.ToString();
        }

        /// <summary>
        /// Initializes a new instance of the Framework.Web.Mvc.Html.Controls.Table.FWTableColumnEditable class.
        /// </summary>
        /// <param name="metadata">The model metadata.</param>
        public FWTableColumnEditable(ModelMetadata metadata)
            : base(metadata)
        { }

        /// <summary>
        /// Gets or sets the model prefix.
        /// </summary>
        public string ModelPrefix { get; set; }
    }
}
