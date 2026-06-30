using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Digitamentos.Models;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;

namespace Digitamentos.ViewModels
{
    public class ResultsViewModel : BaseViewModel
    {
        private readonly MainViewModel _mainViewModel;

        public TypingMetrics Metrics { get; }

        public string AccuracyFormatted => $"{Metrics.Accuracy:F1}%";
        public string KpmFormatted => $"{Metrics.Kpm:F0} TPM";
        public string RawFormatted => $"{Metrics.TotalKeystrokes}";

        public PlotModel SpeedChartModel { get; }

        public ICommand GoHomeCommand { get; }

        public ResultsViewModel(MainViewModel mainViewModel, TypingMetrics metrics)
        {
            _mainViewModel = mainViewModel;
            Metrics = metrics;
            GoHomeCommand = new RelayCommand(_mainViewModel.NavigateToHome);

            SpeedChartModel = new PlotModel
            {
                Title = "Evolução da Velocidade",
                TextColor = OxyColor.Parse("#888888"),
                PlotAreaBorderColor = OxyColors.Transparent
            };

            var xAxis = new LinearAxis { Position = AxisPosition.Bottom, Title = "Tempo (s)", MajorGridlineStyle = LineStyle.Solid, MajorGridlineColor = OxyColor.Parse("#22888888") };
            var yAxis = new LinearAxis { Position = AxisPosition.Left, Title = "Velocidade (TPM)", MajorGridlineStyle = LineStyle.Solid, MajorGridlineColor = OxyColor.Parse("#22888888") };
            SpeedChartModel.Axes.Add(xAxis);
            SpeedChartModel.Axes.Add(yAxis);

            var series = new LineSeries
            {
                Color = OxyColor.Parse("#0078D7"),
                StrokeThickness = 3,
                MarkerType = MarkerType.None
            };

            foreach (var point in metrics.WpmHistory)
            {
                series.Points.Add(new DataPoint(point.Item1, point.Item2));
            }

            var errorSeries = new ScatterSeries
            {
                MarkerType = MarkerType.Cross,
                MarkerStroke = OxyColors.Red,
                MarkerStrokeThickness = 2,
                MarkerSize = 5
            };

            foreach (var point in metrics.ErrorPoints)
            {
                errorSeries.Points.Add(new ScatterPoint(point.Item1, point.Item2));
            }

            SpeedChartModel.Series.Add(series);
            SpeedChartModel.Series.Add(errorSeries);
        }
    }
}
