﻿using System;
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

        public MainWindow()
        {
            InitializeComponent();
            DataContext = tankViewModel;
            grdStats.RowDetailsVisibilityChanged += RowDetailsVisibilityChanged;
        }

        private async void OnSearch(object sender, RoutedEventArgs e)
        {
            btnSearch.IsEnabled = false;
            btnSearch.Content = "Search...";

            await tankViewModel.LoadPlayerTankStats(txtPlayerName.Text);
            DataContext = tankViewModel;

            btnSearch.IsEnabled = true;
            btnSearch.Content = "Search";
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

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox box)
            {
                btnSearch.IsEnabled = box.Text.Length >= 3;
            }
        }

        private static bool IsTextAllowed(string text)
        {
            var regex = new Regex("[^0-9a-zA-Z_]+");
            return !regex.IsMatch(text);
        }

        private void RowDetailsVisibilityChanged(object sender, DataGridRowDetailsEventArgs e)
        {
            if (e.DetailsElement is Chart mcChart && mcChart.IsVisible)
            {
                var lineSeries = (LineSeries)mcChart.Series[0];
                lineSeries.ItemsSource = StatService.GetChartData(tankViewModel.SelectedWeakTank);
                lineSeries.Title = "Estimate";
            }
        }

        private void WinTanksDataGridMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (e.Source.GetType() == typeof(DataGridCellsPresenter))
            {
                if (sender is DataGridRow row)
                {
                    var dataitem = row.DataContext;
                    var visibility = grdStats.GetDetailsVisibilityForItem(dataitem);

                    if (tankViewModel.PrevSelectedWeakTank != null && grdStats.GetDetailsVisibilityForItem(tankViewModel.PrevSelectedWeakTank) == Visibility.Visible)
                    {
                        grdStats.SetDetailsVisibilityForItem(tankViewModel.PrevSelectedWeakTank, Visibility.Collapsed);
                    }

                    if (row.IsSelected && visibility == Visibility.Visible)
                    {
                        grdStats.SetDetailsVisibilityForItem(dataitem, Visibility.Collapsed);
                    }
                    else
                    {
                        grdStats.SetDetailsVisibilityForItem(dataitem, Visibility.Visible);
                    }
                }

                tankViewModel.PrevSelectedWeakTank = tankViewModel.SelectedWeakTank;
            }
        }

        private void MasteryTanksDataGridMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //if (e.Source.GetType() == typeof(DataGridCellsPresenter))
            //{
            //    if (sender is DataGridRow row)
            //    {
            //        var dataitem = row.DataContext;
            //        var visibility = grdStats.GetDetailsVisibilityForItem(dataitem);

            //        if (tankViewModel.PrevSelectedWeakTank != null && grdStats.GetDetailsVisibilityForItem(tankViewModel.PrevSelectedWeakTank) == Visibility.Visible)
            //        {
            //            grdStats.SetDetailsVisibilityForItem(tankViewModel.PrevSelectedWeakTank, Visibility.Collapsed);
            //        }

            //        if (row.IsSelected && visibility == Visibility.Visible)
            //        {
            //            grdStats.SetDetailsVisibilityForItem(dataitem, Visibility.Collapsed);
            //        }
            //        else
            //        {
            //            grdStats.SetDetailsVisibilityForItem(dataitem, Visibility.Visible);
            //        }
            //    }

            //    tankViewModel.PrevSelectedWeakTank = tankViewModel.SelectedWeakTank;
            //}
        }
    }
}
