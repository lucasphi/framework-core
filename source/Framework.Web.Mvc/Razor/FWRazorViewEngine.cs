using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Framework.Web.Mvc.Razor
{
    /// <summary>
    /// Framework implementation of the <see cref="IRazorViewEngine"/>.
    /// </summary>
    public class FWRazorViewEngine : IRazorViewEngine
    {
        private RazorViewEngine _engine;

        /// <summary>
        /// Initializes a new instance of the <see cref="FWRazorViewEngine" />.
        /// </summary>
        public FWRazorViewEngine(IRazorPageFactoryProvider pageFactory, IRazorPageActivator pageActivator,
                                 HtmlEncoder htmlEncoder, IOptions<RazorViewEngineOptions> optionsAccessor, RazorProject razorProject, ILoggerFactory loggerFactory, DiagnosticSource diagnosticSource)
        {
            _engine = new RazorViewEngine(pageFactory, pageActivator, htmlEncoder, optionsAccessor, razorProject, loggerFactory, diagnosticSource);
        }
        
        /// <inheritdoc />
        public ViewEngineResult FindView(ActionContext context, string viewName, bool isMainPage)
        {
            var viewEngineResult = _engine.FindView(context, viewName, isMainPage);
            context.HttpContext.SetView(viewEngineResult.View.Path);
            if (isMainPage)
            {
                context.HttpContext.SetMainPageName(viewEngineResult.View.Path);
            }
            return viewEngineResult;
        }

        /// <inheritdoc />
        public ViewEngineResult GetView(string executingFilePath, string viewPath, bool isMainPage)
        {
            return _engine.GetView(executingFilePath, viewPath, isMainPage);
        }

        /// <inheritdoc />
        public RazorPageResult FindPage(ActionContext context, string pageName)
        {
            return _engine.FindPage(context, pageName);
        }

        /// <inheritdoc />
        public RazorPageResult GetPage(string executingFilePath, string pagePath)
        {
            return _engine.GetPage(executingFilePath, pagePath);
        }

        /// <inheritdoc />
        public string GetAbsolutePath(string executingFilePath, string pagePath)
        {
            return _engine.GetAbsolutePath(executingFilePath, pagePath);
        }
    }
}
