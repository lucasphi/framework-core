using Framework.Collections;
using Framework.Core;
using Framework.Model;
using Framework.Web.Mvc.Helper;
using Framework.Web.Mvc.Html.Controls.Grid;
using Framework.Web.Mvc.Html.Controls.Table;
using Framework.Web.Mvc.Html.Elements;
using Framework.Web.Mvc.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mvc.Html.Controls
{
    /// <summary>
    /// Represents a Grid control.
    /// </summary>
    public class FWGridControl : FWTableControl
    {
        private static Type _pagination = typeof(IFWPagination);

        /// <summary>
        /// Creates the control main element.
        /// </summary>
        /// <returns>The IFWHtmlElement object representation of the control.</returns>
        protected override IFWHtmlElement CreateControl()
        {
            //If the request is a child action, creates the grid.
            if (_fullRender)
            {
                return CreateGrid();
            }
            else
            {
                return CreateGridBody();
            }
        }

        private IFWHtmlElement CreateGrid()
        {
            FWPortletControl grid = new FWPortletControl(Id, FWStringLocalizer.GetViewResource(Id));
            grid.DataType("fw-grid");
            grid.Attributes.Add("data-key", _cacheKey);

            grid.Attributes.AddRange(Attributes);
            if (!string.IsNullOrWhiteSpace(CustomCss))
                grid.AddCssClass(CustomCss);
            // Clears custom attributes and css to prevent adding to the base table control.
            Attributes.Clear();
            ClearCss();

            grid.Attributes.Add("data-url", RequestContext.Url.Action("Load", "FWGrid", new { area = string.Empty }));

            //Adds the grid body to the control.
            FWDivElement detailGridBody = CreateGridBody();
            grid.Add(detailGridBody);

            if (_allowSelect)
            {
                CreateGridSelection(grid);
            }

            return grid;
        }

        private FWDivElement CreateGridBody()
        {
            FWDivElement detailGridBody = new FWDivElement();
            {
                detailGridBody.DataType = "fw-data-body";

                var grid = base.CreateControl() as FWTagBuilder;
                grid.Attributes.Add("role", "grid");
                detailGridBody.Add(grid);

                //If the model has pagination, adds the footer
                if (_paginator != null)
                {
                    detailGridBody.Add(CreateGridFooter());
                }
            }

            return detailGridBody;
        }

        private FWDivElement CreateGridFooter()
        {
            var rowFooter = new FWDivElement();
            rowFooter.AddCssClass("fw-pager clearfix");

            _paginator.CreatePagination(rowFooter);

            var paginationInfo = new FWDivElement();
            paginationInfo.AddCssClass("fw-pager-info");

            if (_paginator.Total > 0)
            {
                var paginationSize = CreateGridDisplayResultsSelect();
                paginationInfo.Add(paginationSize);

                //Creates the items displayed information.
                var elements = new FWSpanElement();
                elements.AddCssClass("fw-pager-detail");
                elements.Add(string.Format(ViewResources.Grid_Label_Displayed, _paginator.CurrentMin, _paginator.CurrentMax, _paginator.Total));
                paginationInfo.Add(elements);
            }
            else
            {
                var elements = new FWSpanElement();
                elements.AddCssClass("fw-pager-detail");
                elements.Add(ViewResources.Grid_No_Results);
                paginationInfo.Add(elements);
            }

            rowFooter.Add(paginationInfo);

            return rowFooter;
        }

        private FWDivElement CreateGridDisplayResultsSelect()
        {
            var divSize = new FWDivElement();
            divSize.AddCssClass("btn-group bootstrap-select fw-pager-size");

            var select = new FWSelectElement("select_display");
            select.AddCssClass("form-control input-sm input-xsmall input-inline");
            select.Add("10", "10", _paginator.Display == 10);
            select.Add("20", "20", _paginator.Display == 20);
            select.Add("50", "50", _paginator.Display == 50);
            select.Add("30", "30", _paginator.Display == 30);
            select.Add("100", "100", _paginator.Display == 100);
            divSize.Add(select);

            return divSize;
        }

        private void CreateGridSelection(FWPortletControl detailGrid)
        {
            detailGrid.Attributes.Add("data-allowselect", "true");

            //Adds the selection legend
            var legend = new FWElementGroup();

            var legendIcon = new FWSpanElement();
            legendIcon.AddCssClass("text-warning");
            legendIcon.Add("<i class=\"fa fa-square\"></i>");
            legend.Add(legendIcon);
            var legendText = new FWSpanElement();
            legendText.Add(ViewResources.Selected);
            legend.Add(legendText);
            detailGrid.AddAction(legend.ToString());

            //Adds the hidden post input
            var postInputHolder = new FWDivElement()
            {
                DataType = "fw-grid-selection",
                Id = _selectionName ?? Id
            };
            if (_selection != null)
            {
                StringBuilder sb = new StringBuilder("[");
                //Adds the selected items to the grid
                foreach (var item in _selection)
                {
                    if (sb.Length > 1)
                        sb.Append(",");
                    sb.Append(item.ToString());
                }
                sb.Append("]");
                postInputHolder.Attributes.Add("data-selected", sb.ToString());
            }

            detailGrid.Add(postInputHolder);
        }

        /// <summary>
        /// Overrides the default CreateLine to add Grid behavior.
        /// </summary>
        /// <param name="lineobj">The object reference for the current line.</param>
        /// <returns>A FWTableLineElement object.</returns>
        protected override FWTableLineElement CreateLine(object lineobj)
        {
            var line = base.CreateLine(lineobj);

            if (_selection != null)
            {
                var lineKey = FWModelHelper.GetKeyPropertyValue(lineobj).ToString();
                //Changes the css of selected cells
                foreach (var selectedItem in _selection)
                {
                    if (selectedItem.ToString() == lineKey)
                        line.AddCssClass("warning");
                }
            }

            return line;
        }

        /// <summary>
        /// Overrides the default CreateHeaderCell to add Grid behavior.
        /// </summary>
        /// <param name="column">The column object.</param>
        /// <returns>A FWTableCellElement object.</returns>
        protected override FWTableCellElement CreateHeaderCell(FWTableColumn column)
        {
            var cell = base.CreateHeaderCell(column);

            if (column.IsSortable)
            {
                cell.AddCssClass("sorting");
                cell.Attributes.Add("data-sorting", column.SortName);

                if (_sortInfo.ContainsKey(column.SortName))
                {
                    cell.Attributes.Add("data-sorting-direction", _sortInfo[column.SortName].ToString());

                    var iconDirection = _sortInfo[column.SortName] == FWSortDirection.Descending ? "down" : "up";
                    var icon = cell.Add(new FWTagBuilder("i"));
                    icon.AddCssClass($"fa fa-angle-double-{iconDirection}");
                }
                else
                {
                    // If the cell is not beeing sorted, set its direction to 'ascending'
                    cell.Attributes.Add("data-sorting-direction", column.SortDirection.ToString());
                }
            }

            if (_allowSelect && _keyPropertyName == column.Property.Name)
            {
                //Marks the key column to allow selection
                cell.Attributes.Add("data-key", "true");
            }

            return cell;
        }

        /// <summary>
        /// Verify if the table has reached its maximum amount of items.
        /// </summary>
        /// <returns>True if the table should not be rendered anymore. Otherwise returs false.</returns>
        protected override bool CheckEndOfTable()
        {
            if (_paginator != null)
                return LineIndex >= _paginator.Display;
            return false;
        }

        /// <summary>
        /// Adds a new column to the Grid.
        /// </summary>
        /// <param name="templates">The columns to be added.</param>
        /// <returns>The fluent configurator object.</returns>
        public FWGridControl AddColumn(params string[] templates)
        {
            return AddColumn(null, int.MaxValue, templates);
        }

        /// <summary>
        /// Adds a new column to the Grid.
        /// </summary>
        /// <param name="css">The column custom CSS.</param>
        /// <param name="order">The order of the column.</param>
        /// <param name="templates">The columns to be added.</param>
        /// <returns>The fluent configurator object.</returns>
        public FWGridControl AddColumn(string css, int order, params string[] templates)
        {
            var column = new FWTableColumnCustom(templates)
            {
                Css = css,
                Order = order
            };
            AddColumn(column);
            return this;
        }

        /// <summary>
        /// Allows the selection of Grid lines.
        /// </summary>
        /// <param name="allowSelect">Informs if the Grid allows selection.</param>
        /// <param name="selectionName">The name of the post field.</param>
        /// <returns>The fluent configurator object.</returns>
        public FWGridControl AllowSelect(bool allowSelect = true, string selectionName = null)
        {
            _allowSelect = allowSelect;
            _selectionName = selectionName;
            if (allowSelect)
            {
                //Stores the key property
                _keyPropertyName = FWModelHelper.GetKeyProperty(base.ListType).Name;
            }
            return this;
        }

        /// <summary>
        /// Creates a new Grid control.
        /// </summary>
        /// <param name="requestContext">The request helper.</param>
        /// <param name="metadata">The model metadata.</param>
        /// <param name="options">Initialization options for the grid.</param>
        public FWGridControl(FWRequestContext requestContext, ModelMetadata metadata, FWGridOptions options)
            : base(requestContext, null, metadata, options.Id)
        {
            _fullRender = true;
            _cacheKey = options.CacheKey;
            
            // Checks if the filter has a pagination.
            if (_pagination.IsAssignableFrom(options.FilterType))
            {
                var dataOptions = Activator.CreateInstance(options.FilterType);
                InitializeDataFiltering(dataOptions as IFWPagination);
            }
        }

        /// <summary>
        /// Creates a new Grid control.
        /// </summary>
        /// <param name="requestContext">The request helper.</param>
        /// <param name="metadata">The model metadata.</param>
        /// <param name="model">The current model.</param>
        public FWGridControl(FWRequestContext requestContext, ModelMetadata metadata, IEnumerable model)
            : base(requestContext, model, metadata)
        {
            if (model is IFWPagination pagination)
            {
                InitializeDataFiltering(pagination);
            }

            if (model is IFWSelectable selectable)
            {
                _selection = selectable.Selected;
            }
        }

        private void InitializeDataFiltering(IFWPagination pagination)
        {
            _paginator = new FWPaginator(pagination);

            _sortInfo = new Dictionary<string, FWSortDirection>();
            var sortInfo = (pagination as IFWDataOptions).SortInfo;
            if (sortInfo != null)
            {
                foreach (var info in sortInfo)
                {
                    _sortInfo.Add(info.SortName, info.SortDirection);
                }
            }
        }

        // Stores if the control should render its body or just the content.
        private bool _fullRender;

        private bool _allowSelect;
        private string _selectionName;
        private IEnumerable _selection;
        private string _keyPropertyName;
        private string _cacheKey;
        
        private FWPaginator _paginator;        
        private Dictionary<string, FWSortDirection> _sortInfo { get; set; }
    }
}
