using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Model.Chart
{
    /// <summary>
    /// Base chart series.
    /// </summary>
    public abstract class FWBaseSerie
    {
        /// <summary>
        /// Gets the chart type.
        /// </summary>
        protected abstract FWChartType ChartType { get; }

        /// <summary>
        /// Gets the chart type as a string.
        /// </summary>
        public string Type => ChartType.GetDescription();

        /// <summary>
        /// Gets or sets the series label.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the series data.
        /// </summary>
        public IEnumerable<double> Data { get; set; }

        /// <summary>
        /// Gets or sets the series background color.
        /// </summary>
        public string BackgroundColor { get; set; }

        /// <summary>
        /// Sets the default background color.
        /// </summary>
        /// <param name="index">The series index.</param>
        internal virtual void DefaultBackgroundColor(int index)
        {
            if (BackgroundColor == null)
            {
                BackgroundColor = $"rgba({BackgroundColors[index, 0]}, {BackgroundColors[index, 1]}, {BackgroundColors[index, 2]})";
            }
        }

        /// <summary>
        /// Default background colors.
        /// </summary>
        public static double[,] BackgroundColors = new double[9, 3]
        {
            { 37, 119, 181 },
            { 124, 180, 124 },
            { 167, 179, 188 },
            { 255, 99, 132 },
            { 54, 162, 235 },
            { 255, 206, 86 },
            { 75, 192, 192 },
            { 153, 102, 255 },
            { 255, 159, 64 }
        };
    }
}
