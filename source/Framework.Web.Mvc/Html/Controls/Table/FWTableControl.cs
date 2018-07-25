using Framework.Core;
using Framework.Model;
using Framework.Web.Mvc.DataAnnotations;
using Framework.Web.Mvc.Helper;
using Framework.Web.Mvc.Html.Controls.Table;
using Framework.Web.Mvc.Html.Elements;
using Framework.Web.Mvc.Resources;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Framework.Web.Mvc.Html.Controls
{
    /// <summary>
    /// Represents a Grid control.
    /// </summary>
    public class FWTableControl : FWControl
    {
        /// <summary>
        /// Gets the table model resource.
        /// </summary>
        /// <param name="key">The resource key.</param>
        /// <returns>The resource string.</returns>
        protected string GetModelResource(string key)
        {
            return FWStringLocalizer.GetModelResource(key, ParentType.Name);
        }

        /// <summary>
        /// Creates the control main element.
        /// </summary>
        /// <returns>The IFWHtmlElement object representation of the control.</returns>
        protected override IFWHtmlElement CreateControl()
        {
            if (_autoGenerateColumns)
                GenerateColumnsFromMetadata();

            // Reorder the columns
            Columns = Columns.OrderBy(f => f.Order).ToList();

            var table = new FWTagBuilder("table")
            {
                DataType = "fw-table"
            };
            table.AddCssClass("table table-responsive-sm m-table m-table--head-separator-metal");

            table.Attributes.AddRange(Attributes);
            if (!string.IsNullOrWhiteSpace(CustomCss))
                table.AddCssClass(CustomCss);

            //Creates the table header
            FWTagBuilder head = new FWTagBuilder("thead");
            head.AddCssClass("thead-default");

            var tableHead = new FWTagBuilder("tr");
            if (!AutoSizeColumns)
                tableHead.AddCssClass("row");

            if (_color.HasValue)
                tableHead.AddCssClass(_color.Value.GetDescription());
            tableHead.Attributes.Add("role", "row");

            //Iterates through the columns to add the header
            foreach (var column in Columns)
            {
                //Creates a new <th> element
                FWTableCellElement headercell = CreateHeaderCell(column);
                tableHead.Add(headercell);
            }

            //Adds the header to the table
            head.Add(tableHead);
            table.Add(head);

            TableBody = new FWTagBuilder("tbody");

            //Iterates through the columns to add the values to the table
            if (Data != null)
            {
                LineIndex = 0;
                foreach (var dataitem in Data)
                {
                    if (CheckEndOfTable())
                        break;

                    var line = CreateLine(dataitem);
                    line.Attributes.Add("data-control-child", "table");
                    if (!AutoSizeColumns)
                        line.AddCssClass("row");

                    foreach (var column in Columns)
                    {
                        //Create a new <td> element
                        var cell = CreateLineCell(dataitem, column);
                        line.Add(cell);
                    }
                    TableBody.Add(line);

                    LineIndex++;
                }
            }

            //Adds the body to the table
            table.Add(TableBody);

            return table;
        }

        /// <summary>
        /// Creates a new table line.
        /// </summary>
        /// <param name="lineobj">The object reference for the current line.</param>
        /// <returns>A FWTableLineElement object.</returns>
        protected virtual FWTableLineElement CreateLine(object lineobj)
        {
            return new FWTableLineElement();
        }

        /// <summary>
        /// Creates a new header cell.
        /// </summary>
        /// <param name="column">The column object.</param>
        /// <returns>A FWTableCellElement object.</returns>
        protected virtual FWTableCellElement CreateHeaderCell(FWTableColumn column)
        {
            var cell = new FWTableCellElement(true);

            //Adds the text to the <th>
            cell.Add(column.Header);         

            AddCellProperties(column, cell);

            return cell;
        }

        /// <summary>
        /// Creates a new line cell.
        /// </summary>
        /// <param name="item">The cell value.</param>
        /// <param name="column">The column object.</param>
        /// <returns>A FWTableCellElement object.</returns>
        protected virtual FWTableCellElement CreateLineCell(object item, FWTableColumn column)
        {
            var cell = new FWTableCellElement(false);

            if (item != null)
            {
                var control = column.GetValue(RequestContext, item, Index);
                cell.Value = control.ToString();
            }

            AddCellProperties(column, cell);

            return cell;
        }

        /// <summary>
        /// Verify if the table has reached its maximum amount of items.
        /// </summary>
        /// <returns>True if the table should not be rendered anymore. Otherwise returs false.</returns>
        protected virtual bool CheckEndOfTable()
        {
            return false;
        }

        private void AddCellProperties(FWTableColumn column, FWTableCellElement cell)
        {
            if (column.Css != null)
                cell.AddCssClass(column.Css);

            //Checks to see if the property is hidden
            if (column.IsHidden)
                cell.AddCssClass("hidden");

            foreach (var attribute in column.Attributes)
            {
                cell.Attributes.Add(attribute.Key, attribute.Value);
            }

            if (column.Metadata != null)
            {
                //Sets the column size if was defined in the metadata.
                if (column.Metadata.AdditionalValues.ContainsKey(nameof(FWSizeAttribute)))
                {
                    var sizes = column.Metadata.AdditionalValues[nameof(FWSizeAttribute)] as List<FWSizeAttribute>;
                    foreach (var item in sizes)
                    {
                        cell.AddCssClass(FWGridSizeHelper.GetCss(item.Size, item.Device));
                    }
                }
            }
        }

        /// <summary>
        /// Sets the color.
        /// </summary>
        /// <param name="color">The button color.</param>
        /// <returns>The fluent configurator object.</returns>
        public FWTableControl Color(FWStateColors color)
        {
            _color = color;
            return this;
        }

        /// <summary>
        /// Sets if the control will generate its columns automatically from the metadata.
        /// </summary>
        /// <param name="autoGenerateColumns">True to auto generate. False otherwise.</param>
        /// <returns>The fluent configurator object.</returns>
        public FWTableControl AutoGenerateColumns(bool autoGenerateColumns = true)
        {
            _autoGenerateColumns = autoGenerateColumns;
            return this;
        }

        /// <summary>
        /// Adds a custom column to the table.
        /// </summary>
        /// <param name="column">The column object.</param>
        protected void AddColumn(FWTableColumn column)
        {
            Columns.Add(column);
        }

        /// <summary>
        /// Creates a new Table.
        /// </summary>
        /// <param name="requestContext">The request helper.</param>
        /// <param name="model">The current model.</param>
        /// <param name="metadata">The model metadata.</param>
        public FWTableControl(FWRequestContext requestContext, IEnumerable model, ModelMetadata metadata)
            : this(requestContext, model, metadata, metadata.PropertyName ?? metadata.ModelType.GetGenericArguments().FirstOrDefault()?.Name)
        { }

        /// <summary>
        /// Creates a new Table.
        /// </summary>
        /// <param name="requestContext">The request helper.</param>
        /// <param name="model">The current model.</param>
        /// <param name="metadata">The model metadata.</param>
        /// <param name="id">The control id.</param>
        public FWTableControl(FWRequestContext requestContext, IEnumerable model, ModelMetadata metadata, string id)
            : base(id)
        {
            //if (!FWReflectionHelper.IsCollection(metadata.ModelType))
            //    throw new FWInvalidPropertyTypeException(metadata.PropertyName, FWKnownTypes.IEnumerable);

            Metadata = metadata;
            Data = model;
            RequestContext = requestContext;

            ListType = metadata.ModelType.GetGenericArguments().First();
            ParentType = metadata.ContainerType ?? metadata.ModelType;
        }

        /// <summary>
        /// Creates a table column.
        /// </summary>
        /// <param name="metadata">The model metadata.</param>
        /// <param name="property">The proeprty info.</param>
        /// <returns>A FWTableColumn object.</returns>
        protected virtual FWTableColumn CreateColumn(ModelMetadata metadata, PropertyInfo property)
        {
            var column = new FWTableColumn(metadata)
            {
                Header = FWStringLocalizer.GetModelResource(property.Name, ListType.Name)
            };
            return column;
        }

        /// <summary>
        /// Generates the columns from the model metadata.
        /// </summary>
        protected virtual void GenerateColumnsFromMetadata()
        {
            // Gets the properties
            IEnumerable<PropertyInfo> properties = FWReflectionHelper.GetPublicProperties(ListType);

            // Finds the property metadata
            ModelMetadata listMetadata = RequestContext.MetadataProvider.GetMetadataForType(ListType);

            // Creates all data columns
            foreach (var prop in properties)
            {
                var itemMetadata = listMetadata.Properties.Where(f => f.PropertyName == prop.Name).First();

                if (!itemMetadata.AdditionalValues.ContainsKey(nameof(FWDatasourceAttribute)))
                {
                    FWTableColumn column = CreateColumn(itemMetadata, prop);
                    column.Property = prop;

                    Columns.Add(column);
                }
            }
        }

        /// <summary>
        /// Gets the model metadata.
        /// </summary>
        protected ModelMetadata Metadata { get; private set; }

        /// <summary>
        /// Gets the table columns.
        /// </summary>
        protected List<FWTableColumn> Columns { get; private set; } = new List<FWTableColumn>();

        /// <summary>
        /// Gets the table &lt;body&gt; tag.
        /// </summary>
        protected FWTagBuilder TableBody { get; private set; }

        /// <summary>
        /// Gets the type of the list.
        /// </summary>
        protected Type ListType { get; private set; }

        /// <summary>
        /// Gets the type of the parent model.
        /// </summary>
        protected Type ParentType { get; private set; }

        /// <summary>
        /// Gets or sets the current line index.
        /// </summary>
        protected object Index { get; set; }

        /// <summary>
        /// Gets or sets if the table columns have sizes defined.
        /// </summary>
        internal bool AutoSizeColumns { get; set; } = true;

        /// <summary>
        /// Gets the current request context.
        /// </summary>
        protected FWRequestContext RequestContext { get; private set; }

        /// <summary>
        /// Gets the current line index been rendered.
        /// </summary>
        protected int LineIndex
        {
            get { return (int)Index; }
            private set { Index = value; }
        }

        /// <summary>
        /// Gets or sets the control model.
        /// </summary>
        protected IEnumerable Data { get; private set; }

        private FWStateColors? _color;
        private bool _autoGenerateColumns { get; set; } = true;
    }
}
