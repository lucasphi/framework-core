using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Model.Chart
{
    /// <summary>
    /// Represents a doughnut chart series.
    /// </summary>
    public class FWDoughnutSerie : FWBaseSerie
    {
        /// <summary>
        /// Gets the chart type.
        /// </summary>
        protected override FWChartType ChartType => FWChartType.Doughnut;

        /// <summary>
        /// Gets or sets the series background color.
        /// </summary>
        public new string[] BackgroundColor { get; set; }

        internal override void DefaultBackgroundColor(int index)
        {
            if (BackgroundColor == null)
            {
                var length = Data.Count();
                BackgroundColor = new string[length];
                for (int i = 0; i < length; i++)
                {
                    BackgroundColor[i] = $"rgba({BackgroundColors[i, 0]}, {BackgroundColors[i, 1]}, {BackgroundColors[i, 2]})";
                }
            }
        }
    }
}
