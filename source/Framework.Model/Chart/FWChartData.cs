using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Model.Chart
{
    /// <summary>
    /// Represents the chart data.
    /// </summary>
    /// <typeparam name="TData">The chart data type.</typeparam>
    public class FWChartData<TData>
    {
        /// <summary>
        /// Gets the chart labels.
        /// </summary>
        public List<string> Labels { get; private set; } = new List<string>();

        /// <summary>
        /// Gets the chart datasets.
        /// </summary>
        public List<TData> Datasets { get; private set; } = new List<TData>();
    }
}
