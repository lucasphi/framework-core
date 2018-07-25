﻿using Framework.Model;
using Framework.Web.Mvc.Filters;
using Framework.Web.Mvc.Helper;
using Framework.Web.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mvc.Html.Controls.Grid
{
    /// <summary>
    /// Framework grid controller.
    /// </summary>
    public class FWGridController : FWBaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FWGridController" />.
        /// </summary>
        /// <param name="memoryCache">The memory cache service reference.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="urlHelperFactory">The url helper factory.</param>
        /// <param name="actionAccessor">The action accessor.</param>
        /// <param name="metadataProvider">The model metadata provider.</param>
        public FWGridController(IMemoryCache memoryCache, IServiceProvider serviceProvider,
                                IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionAccessor, IModelMetadataProvider metadataProvider)
        {
            _memoryCache = memoryCache;
            _serviceProvider = serviceProvider;
            _requestContext = new FWRequestContext(urlHelperFactory, actionAccessor, metadataProvider);
        }

        /// <summary>
        /// Action called to paginate the grid.
        /// </summary>
        /// <param name="bindModel">The temporary binding model.</param>
        /// <returns>The grid html.</returns>
        [FWAuthentication]
        public IActionResult Load(FWDataOptionsViewModel bindModel)
        {
            var options = _memoryCache.Get<FWGridOptions>(bindModel.CId);

            if (options == null)
            {
                return StatusCode(500, "bad model");
            }

            var filter = FWMapperHelper.Transform(options.FilterType, bindModel);            
            FWModelHelper.BindFilterFromQueryString(filter, HttpContext.Request.Query);

            HttpContext.SetView(options.ViewPath);

            var service = _serviceProvider.GetService(options.ServiceType);
            var model = options.ServiceMethod.Invoke(service, new object[] { filter }) as IEnumerable;

            // Gets the metadata from the return type of the service method.
            var metadata = _requestContext.MetadataProvider.GetMetadataForType(options.ServiceMethod.ReturnParameter.ParameterType);
            var control = new FWGridControl(_requestContext, metadata, model)
            {
                AutoSizeColumns = options.AutoSizeColumns
            };
            foreach (var config in options.FluentConfiguration)
            {
                config.Invoke(control);
            }

            return Content(control.ToString());
        }

        private FWRequestContext _requestContext;

        private IMemoryCache _memoryCache;

        private IServiceProvider _serviceProvider;
    }
}
