using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Model.Chart
{
    /// <summary>
    /// Common interface for a combo chart.
    /// </summary>
    public interface IFWComboChart : IFWChart
    {
        /// <summary>
        /// Adds a series.
        /// </summary>
        /// <param name="series">The series.</param>
        void AddSeries(IFWComboSerie series);

        /// <summary>
        /// Adds a list of series.
        /// </summary>
        /// <param name="series">The list of series.</param>
        void AddSeriesRange(IEnumerable<IFWComboSerie> series);
    }
}
