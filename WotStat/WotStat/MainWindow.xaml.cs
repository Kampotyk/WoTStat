using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace WotStat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel tankViewModel = new ViewModel();
        ObservableCollection<KeyValuePair<long, double>> DetailItems { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            base.DataContext = tankViewModel;
            grdStats.RowDetailsVisibilityChanged += new EventHandler<DataGridRowDetailsEventArgs>(RowDetailsVisibilityChanged);
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

        private void RowDetailsVisibilityChanged(object sender, DataGridRowDetailsEventArgs e)
        {
            var mcChart = e.DetailsElement as Chart;
            var lineSeries = (LineSeries)mcChart.Series[0];
            lineSeries.ItemsSource = tankViewModel.GetChartDataForSelectedTank();
            lineSeries.Title = "Estimate";
        }

        private void DataGridMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.Source.GetType().Equals(typeof(DataGridCellsPresenter)))
            {
                DataGridRow row = sender as DataGridRow;
                object dataitem = row.DataContext;
                Visibility visibility = this.grdStats.GetDetailsVisibilityForItem(dataitem);

                if (tankViewModel.PrevSelectedTank != null && this.grdStats.GetDetailsVisibilityForItem(tankViewModel.PrevSelectedTank) == Visibility.Visible)
                {
                    this.grdStats.SetDetailsVisibilityForItem(tankViewModel.PrevSelectedTank, Visibility.Collapsed);
                }

                if (row.IsSelected && visibility == System.Windows.Visibility.Visible)
                {
                    this.grdStats.SetDetailsVisibilityForItem(dataitem, System.Windows.Visibility.Collapsed);
                }
                else
                {
                    this.grdStats.SetDetailsVisibilityForItem(dataitem, System.Windows.Visibility.Visible);
                }

                tankViewModel.PrevSelectedTank = tankViewModel.SelectedTank;
            }
        }
    }
}
