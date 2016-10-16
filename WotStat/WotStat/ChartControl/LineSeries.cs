using System;
using System.Collections.ObjectModel;

namespace WpfSampleBasicChart
{
    public class LineSeries : NotifierBase
    {
        private ObservableCollection<DataPoint> chartData = new ObservableCollection<DataPoint>();
        public ObservableCollection<DataPoint> ChartData
        {
            get { return chartData; }
            set
            {
                SetProperty(ref chartData, value);
            }
        }

        private string name = String.Empty;
        public string Name
        {
            get { return name; }
            set
            {
                SetProperty(ref name, value);
            }
        }
    }
}
