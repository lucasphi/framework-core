using Framework.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Model.Chart
{
    /// <summary>
    /// Represents a combo chart.
    /// </summary>
    class FWComboChart : IFWChart, IFWComboChart
    {
        public string Type => "bar";

        public FWChartData<IFWComboSerie> Data { get; private set; } = new FWChartData<IFWComboSerie>();

        public FWChartOptions Options { get; private set; } = new FWChartOptions();

        public string CreateChart()
        {
            return FWJsonHelper.Serialize(this, true);
        }

        public void AddLabel(string label)
        {
            Data.Labels.Add(label);
        }

        public void AddLabelRange(IEnumerable<string> labels)
        {
            foreach (var item in labels)
            {
                AddLabel(item);
            }
        }

        public void AddSeries(IFWComboSerie series)
        {
            var baseSerie = series as FWBaseSerie;
            baseSerie.DefaultBackgroundColor(Data.Datasets.Count);

            Data.Datasets.Add(series);
        }

        public void AddSeriesRange(IEnumerable<IFWComboSerie> series)
        {
            foreach (var item in series)
            {
                AddSeries(item);
            }
        }
    }
}
