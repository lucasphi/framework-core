using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Model.Chart
{
    /// <summary>
    /// Represents the chart options.
    /// </summary>
    public class FWChartOptions
    {
        /// <summary>
        /// Gets or sets if the chart is responsive.
        /// </summary>
        public bool Responsive { get; set; } = true;

        /// <summary>
        /// Gets or sets the linear scale is use to chart numerical data.
        /// </summary>
        public FWChartScale Scales { get; set; }
    }

    /// <summary>
    /// Represents the chart scales.
    /// </summary>
    public class FWChartScale
    {
        /// <summary>
        /// Gets or sets the yAxes.
        /// </summary>
        public List<FWCartesianAxe> YAxes { get; set; }
    }

    /// <summary>
    /// Represents a chart cartesian axe.
    /// </summary>
    public class FWCartesianAxe
    {
        /// <summary>
        /// Gets or sets the ticks sub options.
        /// </summary>
        public FWChartTick Ticks { get; set; }
    }

    /// <summary>
    /// Represents a chart tick sub option.
    /// </summary>
    public class FWChartTick
    {
        /// <summary>
        /// If true, scale will include 0 if it is not already included.
        /// </summary>
        public bool BeginAtZero { get; set; } = true;
    }
}
