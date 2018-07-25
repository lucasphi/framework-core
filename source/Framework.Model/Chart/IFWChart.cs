using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Model.Chart
{
    /// <summary>
    /// Common interface for a chart.
    /// </summary>
    public interface IFWChart
    {
        /// <summary>
        /// Creates the chart Json object.
        /// </summary>
        /// <returns>The chart json object.</returns>
        string CreateChart();

        /// <summary>
        /// Adds a new label.
        /// </summary>
        /// <param name="label">The label.</param>
        void AddLabel(string label);

        /// <summary>
        /// Adds a list of labels.
        /// </summary>
        /// <param name="labels">The list of labels.</param>
        void AddLabelRange(IEnumerable<string> labels);
    }

    /// <summary>
    /// Common interface for a chart.
    /// </summary>
    /// <typeparam name="TData">The chart data type.</typeparam>
    public interface IFWChart<TData> : IFWChart
        where TData : FWBaseSerie
    {
        /// <summary>
        /// Adds a series.
        /// </summary>
        /// <param name="series">The series.</param>
        void AddSeries(TData series);

        /// <summary>
        /// Adds a list of series.
        /// </summary>
        /// <param name="series">The list of series.</param>
        void AddSeriesRange(IEnumerable<TData> series);
    }
}
