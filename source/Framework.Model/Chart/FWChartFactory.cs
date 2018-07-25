using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Model.Chart
{
    /// <summary>
    /// Encapsuletes the chart factory methods.
    /// </summary>
    public static class FWChartFactory
    {
        /// <summary>
        /// Creates a new Line chart.
        /// </summary>
        /// <returns>The chart configuration object.</returns>
        public static IFWChart<FWLineSerie> CreateLineChart()
        {
            var chart = new FWChart<FWLineSerie>()
            {
                ChartType = FWChartType.Line
            };
            chart.Options.Scales = new FWChartScale()
            {
                YAxes = new List<FWCartesianAxe>()
                {
                    new FWCartesianAxe()
                    {
                        Ticks = new FWChartTick()
                    }
                }
            };
            return chart;
        }

        /// <summary>
        /// Creates a new Bar chart.
        /// </summary>
        /// <returns>The chart configuration object.</returns>
        public static IFWChart<FWBarSerie> CreateBarChart()
        {
            var chart = new FWChart<FWBarSerie>()
            {
                ChartType = FWChartType.Bar
            };
            chart.Options.Scales = new FWChartScale()
            {
                YAxes = new List<FWCartesianAxe>()
                {
                    new FWCartesianAxe()
                    {
                        Ticks = new FWChartTick()
                    }
                }
            };
            return chart;
        }

        /// <summary>
        /// Creates a new Pie chart.
        /// </summary>
        /// <returns>The chart configuration object.</returns>
        public static IFWChart<FWPieSerie> CreatePieChart()
        {
            return new FWChart<FWPieSerie>()
            {
                ChartType = FWChartType.Pie
            };
        }

        /// <summary>
        /// Creates a new Doughnut chart.
        /// </summary>
        /// <returns>The chart configuration object.</returns>
        public static IFWChart<FWDoughnutSerie> CreateDoughnutChart()
        {
            return new FWChart<FWDoughnutSerie>()
            {
                ChartType = FWChartType.Doughnut
            };
        }

        /// <summary>
        /// Creates a new Combo chart.
        /// </summary>
        /// <returns>The chart configuration object.</returns>
        public static IFWComboChart CreateComboChart()
        {
            var chart = new FWComboChart();
            chart.Options.Scales = new FWChartScale()
            {
                YAxes = new List<FWCartesianAxe>()
                {
                    new FWCartesianAxe()
                    {
                        Ticks = new FWChartTick()
                    }
                }
            };
            return chart;
        }
    }
}
