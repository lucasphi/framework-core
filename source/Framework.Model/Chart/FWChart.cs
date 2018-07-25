using Framework.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Framework.Model.Chart
{
    /// <summary>
    /// Represents a chart.
    /// </summary>
    class FWChart<TData> : IFWChart<TData>
        where TData : FWBaseSerie
    {
        [JsonIgnore]
        internal FWChartType ChartType { get; set; }

        public string Type => ChartType.GetDescription();

        public FWChartData<TData> Data { get; private set; } = new FWChartData<TData>();

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

        public void AddSeries(TData series)
        {
            series.DefaultBackgroundColor(Data.Datasets.Count);
            Data.Datasets.Add(series);
        }

        public void AddSeriesRange(IEnumerable<TData> series)
        {
            foreach (var item in series)
            {
                AddSeries(item);
            }
        }        
    }
}
