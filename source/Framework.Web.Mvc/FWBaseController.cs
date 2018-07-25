using Framework.Core;
using Framework.Web.Mvc.Exceptions;
using Framework.Web.Mvc.Filters;
using Framework.Web.Mvc.Html;
using Framework.Web.Mvc.Html.Controls;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Web.Mvc
{
    /// <summary>
    /// Represents the framework base controller class.
    /// </summary>
    [MiddlewareFilter(typeof(FWLocalizationPipeline))]
    [FWExceptionFilter]
    public class FWBaseController : Controller
    {
        /// <summary>
        /// The alert queue key constant.
        /// </summary>
        public const string ALERT_QUEUE = "FWAlertQueue";

        /// <summary>
        /// Adds the culture to the route.
        /// </summary>
        /// <returns>Redirects to the index page.</returns>
        public IActionResult SetLocalization()
        {
            var userLanguage = HttpContext.Request.Headers["Accept-Language"].ToString().Split(',').First();
            var cultureName = FWApp.GetCulture(userLanguage);

            return RedirectToAction("Index", new { culture = cultureName });
        }

        #region Authentication
        /// <summary>
        /// Redirects the user after a successful login.
        /// </summary>
        /// <param name="returnUrl">The url to redirect to. Defaults to 'RedirectUrl' query string.</param>
        /// <returns>A redirection result.</returns>
        protected IActionResult SignInSuccess(string returnUrl = null)
        {
            if (returnUrl == null)
                returnUrl = Request.Query["ReturnUrl"];

            if (string.IsNullOrWhiteSpace(returnUrl))
                return RedirectToAction("Index");
            else
                return Redirect(returnUrl);
        }
        #endregion

        #region Responses
        /// <summary>
        /// Creates a <see cref="JsonResult" /> for a boolean result.
        /// </summary>
        /// <param name="success">The action was successful or not.</param>
        /// <param name="message">The action result message.</param>
        /// <returns>The created <see cref="JsonResult"/> that serializes the specified parameters to JSON format for the response.</returns>
        protected JsonResult Json(bool success, string message = null)
        {
            return Json(new { success = success, message = message });
        }

        /// <summary>
        /// Creates a <see cref="JsonResult"/> object that serializes the specified <paramref name="data"/> object
        /// to JSON.
        /// </summary>
        /// <param name="data">The object to serialize.</param>
        /// <returns>The created <see cref="JsonResult"/> that serializes the specified <paramref name="data"/>
        /// to JSON format for the response.</returns>
        [NonAction]
        public override JsonResult Json(object data)
        {
            data = ReadResultMessagesForAjaxResponses(data);
            return base.Json(data);
        }

        /// <summary>
        /// Creates a <see cref="JsonResult"/> object that serializes the specified <paramref name="data"/> object
        /// to JSON.
        /// </summary>
        /// <param name="data">The object to serialize.</param>
        /// <param name="serializerSettings">The <see cref="JsonSerializerSettings"/> to be used by
        /// the formatter.</param>
        /// <returns>The created <see cref="JsonResult"/> that serializes the specified <paramref name="data"/>
        /// as JSON format for the response.</returns>
        /// <remarks>Callers should cache an instance of <see cref="JsonSerializerSettings"/> to avoid
        /// recreating cached data with each call.</remarks>
        [NonAction]
        public override JsonResult Json(object data, JsonSerializerSettings serializerSettings)
        {
            data = ReadResultMessagesForAjaxResponses(data);
            return base.Json(data, serializerSettings);
        }

        /// <summary>
        /// Redirects an ajax request to the specified action.
        /// </summary>
        /// <param name="actionName">The name of the action.</param>
        /// <returns>The created Json with the redirect command.</returns>
        protected IActionResult AjaxRedirectToAction(string actionName)
        {
            var url = Url.Action(actionName);
            return AjaxRedirect(url);
        }

        /// <summary>
        /// Redirects an ajax request to the specified action.
        /// </summary>
        /// <param name="actionName">The name of the action.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <returns>The created Json with the redirect command.</returns>
        protected IActionResult AjaxRedirectToAction(string actionName, string controllerName = null)
        {
            var url = Url.Action(actionName, controllerName);
            return AjaxRedirect(url);
        }

        /// <summary>
        /// Redirects an ajax request to the specified action.
        /// </summary>
        /// <param name="actionName">The name of the action.</param>
        /// <param name="routeValues">The route data to use for generating the URL.</param>
        /// <returns>The created Json with the redirect command.</returns>
        protected IActionResult AjaxRedirectToAction(string actionName, object routeValues = null)
        {
            var url = Url.Action(actionName, routeValues);
            return AjaxRedirect(url);
        }

        /// <summary>
        /// Redirects an ajax request to the specified action.
        /// </summary>
        /// <param name="actionName">The name of the action.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <param name="routeValues">The route data to use for generating the URL.</param>
        /// <returns>The created Json with the redirect command.</returns>
        protected IActionResult AjaxRedirectToAction(string actionName, string controllerName = null, object routeValues = null)
        {
            var url = Url.Action(actionName, controllerName, routeValues);
            return AjaxRedirect(url);
        }

        /// <summary>
        /// Redirects an ajax request to the specified URL.
        /// </summary>
        /// <param name="url">The url to redirect.</param>
        /// <returns>The created Json with the redirect command.</returns>
        protected IActionResult AjaxRedirect(string url)
        {
            return base.Json(new { redirectUrl = url});
        }
        #endregion

        #region Alerts
        /// <summary>
        /// Defines the action success message.
        /// </summary>
        /// <param name="message">The message text.</param>
        /// <param name="alertType">The alert type.</param>
        [NonAction]
        public void SetResultMessage(string message, FWMessageType alertType)
        {
            var alert = new FWMessage() { Message = message, Type = alertType };
            AddAlert(alert);
        }

        private void AddAlert(FWMessage alert)
        {
            Queue<FWMessage> alerts;

            if (TempData.ContainsKey(ALERT_QUEUE))
                alerts = FWJsonHelper.Deserialize<Queue<FWMessage>>(TempData[ALERT_QUEUE].ToString());
            else
                alerts = new Queue<FWMessage>();

            alerts.Enqueue(alert);
            TempData[ALERT_QUEUE] = FWJsonHelper.Serialize(alerts);
        }

        private object ReadResultMessagesForAjaxResponses(object data)
        {
            if (TempData.ContainsKey(ALERT_QUEUE))
            {
                var alerts = FWJsonHelper.Deserialize<Queue<FWMessage>>(TempData[ALERT_QUEUE].ToString());

                if (alerts != null)
                {
                    var settings = new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    };
                    data = new { content = data, alerts = JsonConvert.SerializeObject(alerts, settings) };
                }
            }

            return data;
        }
        #endregion

        #region Action events
        /// <summary>
        /// Handles invalid model requests.
        /// </summary>
        /// <param name="context">The action executing context.</param>
        protected virtual void OnInvalidModelState(ActionExecutingContext context)
        {
            var errors = context.ModelState.Where(f => f.Value.Errors.Count > 0)
                                .SelectMany(s => s.Value.Errors.Select(x => x.ErrorMessage));
            throw new FWRequestInterruptException(string.Join(", ", errors));
        }

        /// <summary>
        /// Called before the action method is invoked.
        /// </summary>
        /// <param name="context">The action executing context.</param>
        [NonAction]
        public sealed override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!ModelState.IsValid)
            {
                OnInvalidModelState(context);
            }

            base.OnActionExecuting(context);
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Gets a list with all modelstate error messages.
        /// </summary>
        /// <returns>The list with all error messages.</returns>
        protected IEnumerable<string> GetModelStateErrors()
        {
            return ModelState.Where(f => f.Value.Errors.Count > 0)
                                .SelectMany(s => s.Value.Errors.Select(x => x.ErrorMessage));
        }
        #endregion
    }
}
