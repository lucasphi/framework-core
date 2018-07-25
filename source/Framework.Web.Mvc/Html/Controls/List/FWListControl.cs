using Framework.Core;
using Framework.Model;
using Framework.Web.Mvc.Exceptions;
using Framework.Web.Mvc.Html.Controls.List;
using Framework.Web.Mvc.Html.Elements;
using Framework.Web.Mvc.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SmartFormat;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Web.Mvc.Html.Controls
{
    /// <summary>
    /// Represents a list control.
    /// </summary>
    public class FWListControl : FWControl
    {
        private static Type _paginationInterface = typeof(IFWPagination);

        /// <summary>
        /// Creates the control main element.
        /// </summary>
        /// <returns>The IFWHtmlElement object representation of the control.</returns>
        protected override IFWHtmlElement CreateControl()
        {
            //If the request is a child action, creates the list.
            if (_fullRender)
            {
                return CreateList();
            }
            else
            {
                return CreateListItems();
            }
        }

        private IFWHtmlElement CreateList()
        {
            FWPortletControl list = new FWPortletControl(Id, FWStringLocalizer.GetViewResource(Id))
            {
                Id = Id
            };
            list.DataType("fw-list");
            list.Attributes.Add("data-key", _cacheKey);

            list.Attributes.AddRange(Attributes);
            if (!string.IsNullOrWhiteSpace(CustomCss))
                list.AddCssClass(CustomCss);

            list.Attributes.Add("data-url", RequestContext.Url.Action("Load", "FWList", new { area = string.Empty }));

            // Adds the list body to the control.
            FWDivElement detailListBody = CreateListItems();
            list.Add(detailListBody);

            return list;
        }

        private FWDivElement CreateListItems()
        {
            var listBody = new FWDivElement
            {
                DataType = "fw-data-body"
            };
            listBody.AddCssClass("row");

            if (Data != null)
            {
                // If the control is attemping to render its contents but a template has not been defined, throw an exception.
                if (_template == null)
                {
                    throw new FWMissingTemplateException(Id);
                }

                foreach (var item in Data)
                {
                    var listItem = new FWDivElement();
                    listItem.AddCssClass(_columnCssClasses);
                    var html = Smart.Format(_template, item);
                    listItem.Add(html);

                    listBody.Add(listItem);
                }
            }

            if (_paginator != null)
            {
                var paginatorWrapper = new FWDivElement();
                paginatorWrapper.AddCssClass("full-width");
                paginatorWrapper.Add(CreateListFooter());
                listBody.Add(paginatorWrapper);
            }

            return listBody;
        }

        private FWDivElement CreateListFooter()
        {
            var rowFooter = new FWDivElement();
            rowFooter.AddCssClass("fw-pager m-datatable--paging-loaded clearfix");

            _paginator.CreatePagination(rowFooter);

            var paginationInfo = new FWDivElement();
            paginationInfo.AddCssClass("fw-pager-info");

            if (_paginator.Total > 0)
            {
                var paginationSize = CreateDisplayResultsSelect();
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

        private FWDivElement CreateDisplayResultsSelect()
        {
            var divSize = new FWDivElement();
            divSize.AddCssClass("btn-group bootstrap-select fw-pager-size");

            var select = new FWSelectElement("select_display");
            select.AddCssClass("form-control input-sm input-xsmall input-inline");
            select.Add("10", "10", _paginator.Display == 10);
            select.Add("20", "20", _paginator.Display == 20);
            select.Add("30", "30", _paginator.Display == 30);
            select.Add("50", "50", _paginator.Display == 50);
            select.Add("100", "100", _paginator.Display == 100);
            divSize.Add(select);

            return divSize;
        }

        /// <summary>
        /// Sets the default template for the list.
        /// </summary>
        /// <param name="template">The template Html string.</param>
        /// <returns>The fluent configurator object.</returns>
        public FWListControl Template(string template)
        {
            _template = template;
            return this;
        }

        /// <summary>
        /// Sets the column css classes.
        /// </summary>
        /// <param name="css">The column css classes.</param>
        /// <returns>The fluent configurator object.</returns>
        public FWListControl ColumnClass(string css)
        {
            _columnCssClasses = css;
            return this;
        }

        /// <summary>
        /// Creates a new List control.
        /// </summary>
        /// <param name="requestContext">The request helper.</param>
        /// <param name="metadata">The model metadata.</param>
        /// <param name="options">Initialization options for the list.</param>
        public FWListControl(FWRequestContext requestContext, ModelMetadata metadata, FWListOptions options)
            : base(options.Id)
        {
            _fullRender = true;
            _cacheKey = options.CacheKey;
            RequestContext = requestContext;

            ListType = metadata.ModelType.GetGenericArguments().First();
            ParentType = metadata.ContainerType ?? metadata.ModelType;

            if (_paginationInterface.IsAssignableFrom(options.FilterType))
            {
                _paginator = new FWPaginator(Activator.CreateInstance(options.FilterType) as IFWPagination);
            }
        }

        /// <summary>
        /// Creates a new List control.
        /// </summary>
        /// <param name="requestContext">The request helper.</param>
        /// <param name="metadata">The model metadata.</param>
        /// <param name="model">The current model.</param>
        public FWListControl(FWRequestContext requestContext, ModelMetadata metadata, IEnumerable model)
        {
            RequestContext = requestContext;
            Data = model;

            if (model is IFWPagination pagination)
            {
                _paginator = new FWPaginator(pagination);
            }
        }

        private Type ListType { get; set; }
        private Type ParentType { get; set; }

        /// <summary>
        /// Gets or sets the control model.
        /// </summary>
        private IEnumerable Data { get; set; }

        private FWRequestContext RequestContext { get; set; }

        // Stores if the control should render its body or just the content.
        private bool _fullRender;
        
        private FWPaginator _paginator;
        private string _cacheKey;

        private string _columnCssClasses;
        private string _template;
    }
}

