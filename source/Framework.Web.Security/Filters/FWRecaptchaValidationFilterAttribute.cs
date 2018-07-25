using Framework.Core;
using Framework.Web.Security.Util;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Web.Security.Filters
{
    /// <summary>
    /// Action filter to handle recaptcha validations.
    /// </summary>
    public class FWRecaptchaValidationFilterAttribute : ActionFilterAttribute
    {
        /// <inheritdoc />
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheKey = $"LoginAttempt_{context.HttpContext.Connection.RemoteIpAddress}";
            var attempt = _memoryCache.GetOrCreate(cacheKey, entry => 0);

            // Only validates the recatpcha if the number of attempts is equal or greater then the required attempts.
            if (attempt >= _options.RequiredAttempts)
            {
                await DoReCaptchaValidation(context);
            }

            _memoryCache.Set(cacheKey, attempt + 1, new TimeSpan(1, 0, 0));

            await base.OnActionExecutionAsync(context, next);
        }

        private async Task DoReCaptchaValidation(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.HasFormContentType)
            {
                // Get request? 
                AddModelError(context, Resources.Resources.FWRecaptcha_NoToken);
                return;
            }
            string token = context.HttpContext.Request.Form["g-recaptcha-response"];
            if (string.IsNullOrWhiteSpace(token))
            {
                AddModelError(context, Resources.Resources.FWRecaptcha_Invalid);
            }
            else
            {
                await ValidateRecaptcha(context, token);
            }
        }

        private async Task ValidateRecaptcha(ActionExecutingContext context, string token)
        {
            using (var webClient = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new[]
                {
                        new KeyValuePair<string, string>("secret", _options.PrivateKey),
                        new KeyValuePair<string, string>("response", token)
                });
                HttpResponseMessage response = await webClient.PostAsync(_options.ApiEndpoint, content);
                string json = await response.Content.ReadAsStringAsync();
                var reCaptchaResponse = FWJsonHelper.Deserialize<ReCaptchaResponse>(json);
                if (reCaptchaResponse == null)
                {
                    AddModelError(context, Resources.Resources.FWRecaptcha_NoResponse);
                }
                else if (!reCaptchaResponse.Success)
                {
                    AddModelError(context, Resources.Resources.FWRecaptcha_Invalid);
                }
            }
        }

        private static void AddModelError(ActionExecutingContext context, string error)
        {
            context.ModelState.AddModelError("ReCaptcha", error.ToString());
        }
        
        /// <summary>
        /// Initializes a new instance of <see cref="FWRecaptchaValidationFilterAttribute"/>.
        /// </summary>
        /// <param name="configuration">The configuration object.</param>
        /// <param name="memoryCache">The memory cache object.</param>
        public FWRecaptchaValidationFilterAttribute(IConfiguration configuration, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;

            var options = new FWRecaptchaOptions();
            configuration.Bind("Recaptcha", options);
            if (options.SiteKey == null)
                throw new FWSettingsException("Recaptcha:SiteKey");

            _options = options;
        }

        private FWRecaptchaOptions _options;
        private IMemoryCache _memoryCache;
    }

    class ReCaptchaResponse
    {
        public bool Success { get; set; }

        public string Challenge_ts { get; set; }

        public string Hostname { get; set; }

        public string[] Errorcodes { get; set; }
    }
}
