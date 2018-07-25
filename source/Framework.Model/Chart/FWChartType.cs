using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Framework.Model.Chart
{
    /// <summary>
    /// Enumerate the chart types.
    /// </summary>
    public enum FWChartType
    {
        /// <summary>
        /// Line chart.
        /// </summary>
        [Description("line")]
        Line = 1,

        /// <summary>
        /// Bar chart.
        /// </summary>
        [Description("bar")]
        Bar,

        /// <summary>
        /// Radar chart.
        /// </summary>
        [Description("radar")]
        Radar,

        /// <summary>
        /// Pie chart.
        /// </summary>
        [Description("pie")]
        Pie,

        /// <summary>
        /// Doughnut chart.
        /// </summary>
        [Description("doughnut")]
        Doughnut,

        /// <summary>
        /// PolarArea chart.
        /// </summary>
        [Description("polarArea")]
        PolarArea
    }
}
