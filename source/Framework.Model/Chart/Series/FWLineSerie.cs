using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Model.Chart
{
    /// <summary>
    /// Represents a line chart series.
    /// </summary>
    public class FWLineSerie : FWBaseSerie, IFWComboSerie
    {
        /// <summary>
        /// Gets the chart type.
        /// </summary>
        protected override FWChartType ChartType => FWChartType.Line;

        /// <summary>
        /// Gets or sets if the area under the line is filled.
        /// </summary>
        public bool Fill { get; set; }

        /// <summary>
        /// Gets or sets the color of the line.
        /// </summary>
        public string BorderColor => BackgroundColor;
    }
}
