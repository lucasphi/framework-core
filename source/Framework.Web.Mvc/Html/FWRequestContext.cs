using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Framework.Web.Mvc.Html
{
    /// <summary>
    /// Encapsulates the html helpers.
    /// </summary>
    public class FWRequestContext
    {
        /// <summary>
        /// Gets the url helper instance.
        /// </summary>
        internal IUrlHelper Url
        {
            get
            {
                if (_urlHelper == null)
                    _urlHelper = _urlHelperFactory.GetUrlHelper(_actionAccessor.ActionContext);
                return _urlHelper;
            }
        }

        /// <summary>
        /// Gets the view model.
        /// </summary>
        internal object Model { get; set; }

        /// <summary>
        /// Gets the HttpContext for the current request.
        /// </summary>
        internal HttpContext HttpContext { get => _actionAccessor.ActionContext.HttpContext; }

        /// <summary>
        /// Gets the model metadata provider.
        /// </summary>
        internal IModelMetadataProvider MetadataProvider { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWRequestContext" />.
        /// </summary>
        /// <param name="urlHelperFactory">The url helper factory.</param>
        /// <param name="actionAccessor">The action accessor.</param>
        public FWRequestContext(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionAccessor)
        {
            _urlHelperFactory = urlHelperFactory;
            _actionAccessor = actionAccessor;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWRequestContext" />.
        /// </summary>
        /// <param name="urlHelperFactory">The url helper factory.</param>
        /// <param name="actionAccessor">The action accessor.</param>
        /// <param name="provider">The model metadata provider.</param>
        public FWRequestContext(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionAccessor,
                                    IModelMetadataProvider provider)
        {
            _urlHelperFactory = urlHelperFactory;
            _actionAccessor = actionAccessor;
            MetadataProvider = provider;
        }

        private IUrlHelper _urlHelper;

        private IUrlHelperFactory _urlHelperFactory;
        private IActionContextAccessor _actionAccessor;
    }
}
