using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using WpfSampleBasicChart;

namespace WotStat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel tankViewModel = new ViewModel();
        ObservableCollection<LineSeries> chartData = new ObservableCollection<LineSeries>();

        public MainWindow()
        {
            InitializeComponent();
            base.DataContext = tankViewModel;
            TankChart.ItemsSource = chartData;
        }

        private void OnSearch(object sender, RoutedEventArgs e)
        {
            tankViewModel.LoadPlayerStats(txtPlayerName.Text);
            base.DataContext = tankViewModel;
        }

        private void TextBoxValidation(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void TextBoxPasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void OnTextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            TextBox box = sender as TextBox;
            this.btnSearch.IsEnabled = box.Text.Length >= 3;
        }

        private bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9a-zA-Z_]+");
            return !regex.IsMatch(text);
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (chartData.Count == 1)
            {
                chartData.RemoveAt(0);
            }
            chartData.Add(tankViewModel.GetChartDataForSelectedTank());
        }
    }
}
