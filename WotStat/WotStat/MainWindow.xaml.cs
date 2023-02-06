using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using WotStatService.Models;

namespace WotStat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel tankViewModel = new ViewModel();
        private PrivateData userData = null;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = tankViewModel;
            grdStats.RowDetailsVisibilityChanged += StatsRowDetailsVisibilityChanged;
            grdMastery.RowDetailsVisibilityChanged += MasteryRowDetailsVisibilityChanged;
        }

        private async void OnSearch(object sender, RoutedEventArgs e)
        {
            btnSearch.IsEnabled = false;

            var originalContent = btnSearch.Content;
            btnSearch.Content = $"{originalContent}...";

            await tankViewModel.LoadPlayerTankStats(txtPlayerName.Text, userData);
            DataContext = tankViewModel;

            btnSearch.IsEnabled = true;
            btnSearch.Content = originalContent;
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

        private void StatsRowDetailsVisibilityChanged(object sender, DataGridRowDetailsEventArgs e)
        {
            if (e.DetailsElement is Chart statsChart && statsChart.IsVisible)
            {
                var lineSeries = (LineSeries)statsChart.Series[0];
                lineSeries.ItemsSource = StatService.StatsGetChartData(tankViewModel.SelectedWeakTank);
                lineSeries.Title = "Estimate";
            }
        }

        private void MasteryRowDetailsVisibilityChanged(object sender, DataGridRowDetailsEventArgs e)
        {
            if (e.DetailsElement is Chart masteryChart && masteryChart.IsVisible)
            {
                var columntSeries = (ColumnSeries)masteryChart.Series[0];
                columntSeries.ItemsSource = StatService.MasteryGetChartData(tankViewModel.SelectedNoMasterTank);
                columntSeries.Title = "Mastery";
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
            if (e.Source.GetType() == typeof(DataGridCellsPresenter))
            {
                if (sender is DataGridRow row)
                {
                    var dataitem = row.DataContext;
                    var visibility = grdMastery.GetDetailsVisibilityForItem(dataitem);

                    if (tankViewModel.PrevSelectedNoMasterTank != null && grdMastery.GetDetailsVisibilityForItem(tankViewModel.PrevSelectedNoMasterTank) == Visibility.Visible)
                    {
                        grdMastery.SetDetailsVisibilityForItem(tankViewModel.PrevSelectedNoMasterTank, Visibility.Collapsed);
                    }

                    if (row.IsSelected && visibility == Visibility.Visible)
                    {
                        grdMastery.SetDetailsVisibilityForItem(dataitem, Visibility.Collapsed);
                    }
                    else
                    {
                        grdMastery.SetDetailsVisibilityForItem(dataitem, Visibility.Visible);
                    }
                }

                tankViewModel.PrevSelectedNoMasterTank = tankViewModel.SelectedNoMasterTank;
            }
        }

        private void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            if (userData == null)
            {
                userData = new PrivateData();

                Login loginWindow = new Login(userData);
                loginWindow.ShowDialog();

                if (!String.IsNullOrEmpty(userData.Status) && userData.Status.Equals("ok"))
                {
                    btnLogin.Content = "Logout";
                    lblUsername.Content = userData.Nickname.Replace("_", "__");
                }
            }
            else
            {
                if (StatService.Logout(userData.AccessToken))
                {
                    userData = null;
                    btnLogin.Content = "Login";
                    lblUsername.Content = String.Empty;
                }
            }
        }
    }
}
