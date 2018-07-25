using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Model.Chart
{
    /// <summary>
    /// Represents a bar chart series.
    /// </summary>
    public class FWBarSerie : FWBaseSerie, IFWComboSerie
    {
        /// <summary>
        /// Gets the chart type.
        /// </summary>
        protected override FWChartType ChartType => FWChartType.Bar;

        /// <summary>
        /// Gets or sets the color of the bar border.
        /// </summary>
        public string BorderColor { get; set; }
    }
}
