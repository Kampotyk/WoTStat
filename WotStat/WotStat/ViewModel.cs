using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

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

        public void LoadPlayerStats(string playerName)
        {
            Tanks = GetPlayersTanks(GetAccountIdByName(playerName), GetAllTanks());
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

        private static string GetAccountIdByName(string name)
        {
            var accountId = String.Empty;
            var requestParams = new NameValueCollection
            {
                { "application_id", "12845e99af9d4a7b3c734c0cbbb5ee12" },
                { "search", name },
                { "limit", "1" }
            };

            var jsonResult = Request.PostRequest(Constants.AccountListUrl, requestParams);

            if (String.IsNullOrEmpty(jsonResult))
            {
                return accountId;
            }

            dynamic result = JsonConvert.DeserializeObject<dynamic>(jsonResult);
            accountId = result.data.Last.account_id;

            return accountId;
        }

        private static Dictionary<string, string> GetAllTanks()
        {
            var tanks = new Dictionary<string, string>();

            var requestParams = new NameValueCollection
            {
                { "application_id", "12845e99af9d4a7b3c734c0cbbb5ee12" },
                { "language", "en" },
                { "fields", "short_name, tank_id" }
            };

            var jsonResult = Request.PostRequest(Constants.TanksListUrl, requestParams);
            if (String.IsNullOrEmpty(jsonResult))
            {
                return tanks;
            }

            dynamic result = JsonConvert.DeserializeObject<dynamic>(jsonResult);
            foreach (dynamic tank in result.data)
            {
                dynamic value = tank.Value;
                tanks.Add(value.tank_id.ToString(), value.short_name.ToString());
            }
            return tanks;
        }

        private static ObservableCollection<TankModel> GetPlayersTanks(string accountId, Dictionary<string, string> tanks)
        {
            var playerTanks = new ObservableCollection<TankModel>();

            var requestParams = new NameValueCollection
            {
                { "application_id", "12845e99af9d4a7b3c734c0cbbb5ee12" },
                { "language", "en" },
                { "account_id", accountId }
            };

            var jsonResult = Request.PostRequest(Constants.PlayersTanksUrl, requestParams);

            if (String.IsNullOrEmpty(jsonResult))
            {
                return playerTanks;
            }

            dynamic result = JsonConvert.DeserializeObject<dynamic>(jsonResult);
            foreach (dynamic player in result.data)
            {
                foreach (dynamic tank in player.Value)
                {
                    string tankName;
                    if (tanks.TryGetValue(tank.tank_id.ToString(), out tankName))
                    {
                        var battles = tank.statistics.battles.Value;
                        var wins = tank.statistics.wins.Value;
                        var badge = (Constants.Badge)tank.mark_of_mastery.Value;
                        var tankModel = new TankModel(tankName, battles, wins, badge);
                        if (tankModel.WinsToDesiredPercent > 0)
                        {
                            playerTanks.Add(tankModel);
                        }
                    }
                }
            }
            return playerTanks;
        }
    }
}
