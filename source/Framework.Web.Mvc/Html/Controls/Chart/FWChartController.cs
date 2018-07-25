using Framework.Core;
using Framework.Model.Chart;
using Framework.Web.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mvc.Html.Controls.Chart
{
    /// <summary>
    /// Framework chart controller.
    /// </summary>
    public class FWChartController : FWBaseController
    {
        /// <summary>
        /// Action called to load the chart data from an ajax request.
        /// </summary>
        /// <param name="id">The chart id.</param>
        /// <returns>The chart json.</returns>
        [FWAuthentication]
        public IActionResult Load(string id)
        {
            string chartKey = $"chartKey{id}";
            var chart = HttpContext.Session.GetString(chartKey);
            HttpContext.Session.Remove(chartKey);
            return Content(chart);
        }
    }
}
