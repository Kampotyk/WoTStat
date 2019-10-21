using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WotStat.Extensions;

namespace WotStat
{
    internal class ViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<TankModel> tanks;
        private TankModel selectedTank;

        public ObservableCollection<TankModel> Tanks
        {
            get => tanks;
            set
            {
                tanks = value;
                OnPropertyChanged("Tanks");
            }
        }

        public TankModel SelectedTank
        {
            get => selectedTank;
            set
            {
                if (selectedTank != value)
                {
                    selectedTank = value;
                    OnPropertyChanged("SelectedTank");
                }
            }
        }

        public TankModel PrevSelectedTank { get; set;}

        public event PropertyChangedEventHandler PropertyChanged;

        public async Task<bool> LoadPlayerStats(string playerName)
        {
            Tanks = await Task.Factory.StartNew(() => StatService.GetPlayersTanks(
                StatService.GetAccountIdByName(playerName)
                , StatService.GetAllTanks()));
            return true;
        }

        public ObservableCollection<KeyValuePair<long, double>> GetChartDataForSelectedTank()
        {
            var chartData = new List<KeyValuePair<long, double>>();
            for (var sessionRatio = Constants.DesiredWinPercent + Constants.GraphStep;
                 sessionRatio < Constants.MaxWinPercent;
                 sessionRatio += Constants.GraphStep)
            {
                var battleCountToDesiredRatio = Computer.GetBattleCountToDesiredRatio(SelectedTank.BattleCount, SelectedTank.WinCount, Constants.DesiredWinPercent, sessionRatio);
                chartData.Add(new KeyValuePair<long, double>(battleCountToDesiredRatio, sessionRatio));
            }
            return new ObservableCollection<KeyValuePair<long,double>>(chartData.OrderByDescending(pair => pair.Value));
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
