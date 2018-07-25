using Framework.Core;
using Framework.Web.Mvc.Html;
using Framework.Web.Mvc.Html.Controls;
using Framework.Web.Mvc.Html.Menu;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Web.Mvc
{
    /// <summary>
    /// Extension methods for razor.
    /// </summary>
    public class FWRazorInjections : IFWRazorInjections
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FWRazorInjections" />.
        /// </summary>
        /// <param name="httpContext">The application http context accessor.</param>
        /// <param name="tempDataFactory">The tempdata factory.</param>
        /// <param name="applicationMenu">The application menu.</param>
        /// <param name="appMenuBuilder">The custom application menu builder.</param>
        public FWRazorInjections(IHttpContextAccessor httpContext, ITempDataDictionaryFactory tempDataFactory, IFWMenuComponent applicationMenu = null, IFWMenuBuilder appMenuBuilder = null)
        {
            _httpContextAccessor = httpContext;
            _tmpDataFactory = tempDataFactory;
            _applicationMenu = applicationMenu;
            _appMenuBuilder = appMenuBuilder;
        }

        /// <summary>
        /// In a Razor layout page, renders the main menu.
        /// </summary>
        /// <param name="urlHelper">The current UrlHelper.</param>
        /// <returns>The main menu html.</returns>
        public IHtmlContent RenderMenu(IUrlHelper urlHelper)
        {
            if (_applicationMenu == null)
                return new HtmlString(string.Empty);

            IFWMenuBuilder builder = _appMenuBuilder ?? new FWMainMenu(urlHelper);

            _applicationMenu.Build(builder);
            return new HtmlString(builder.ToHtml());
        }

        /// <summary>
        /// In a Razor layout page, renders the user menu.
        /// </summary>
        /// <param name="urlHelper">The current UrlHelper.</param>
        /// <returns>The user menu html.</returns>
        public IHtmlContent RenderTopMenu(IUrlHelper urlHelper)
        {
            var menus = FWServiceCollectionExtensions.GetRegisteredTopMenus<IFWMenuComponent>();
            if (!menus.Any())
                return new HtmlString(string.Empty);

            StringBuilder menuStringBuilder = new StringBuilder();
            foreach (var menu in menus)
            {
                var menuBuilder = new FWTopMenu(urlHelper);
                menu.Build(menuBuilder);
                menuStringBuilder.Append(menuBuilder.ToHtml());
            }
            return new HtmlString(menuStringBuilder.ToString());
        }

        /// <summary>
        /// In a Razor layoutpage, renders the last request alerts.
        /// </summary>
        /// <returns>The alerts.</returns>
        public IHtmlContent RenderAlerts()
        {
            var tempData = _tmpDataFactory.GetTempData(_httpContextAccessor.HttpContext);
            if (!tempData.ContainsKey(FWBaseController.ALERT_QUEUE))
                return new HtmlString(string.Empty);

            var alerts = FWJsonHelper.Deserialize<Queue<FWMessage>>(tempData[FWBaseController.ALERT_QUEUE].ToString());

            StringBuilder sb = new StringBuilder();
            if (alerts != null)
            {
                foreach (var alert in alerts)
                {
                    var alertControl = new FWMessageControl(alert.Message, alert.Type);
                    sb.Append(alertControl.ToString());
                }
            }

            return new HtmlString(sb.ToString());
        }        

        private IHttpContextAccessor _httpContextAccessor;
        private ITempDataDictionaryFactory _tmpDataFactory;
        private IFWMenuComponent _applicationMenu;
        private IFWMenuBuilder _appMenuBuilder;        
    }    
}
