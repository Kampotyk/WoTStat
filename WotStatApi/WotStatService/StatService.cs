using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using WotStat.Extensions;
using WotStat.Models;
using WotStatService.Models;

namespace WotStat
{
    public static class StatService
    {
        public static string GetAccountIdByName(string name, Region region)
        {
            var accountId = String.Empty;
            var requestParams = new NameValueCollection
            {
                { "application_id", "12845e99af9d4a7b3c734c0cbbb5ee12" },
                { "search", name },
                { "limit", "1" }
            };

            var jsonResult = Request.PostRequest(UrlResolver.AccountListUrl(region), requestParams);

            if (String.IsNullOrEmpty(jsonResult))
            {
                return accountId;
            }

            dynamic result = JsonConvert.DeserializeObject<dynamic>(jsonResult);
            accountId = result.data.Last.account_id;

            return accountId;
        }

        public static Dictionary<string, string> GetAllTanks(Region region)
        {
            var tanks = new Dictionary<string, string>();

            var requestParams = new NameValueCollection
            {
                { "application_id", "12845e99af9d4a7b3c734c0cbbb5ee12" },
                { "language", "en" },
                { "fields", "short_name, tank_id" }
            };

            var jsonResult = Request.PostRequest(UrlResolver.TanksListUrl(region), requestParams);
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

        public static List<TankModel> GetPlayerTankStats(string accountId,
            Dictionary<string, string> tanks, Dictionary<string, List<string>> tanksMastery, Region region)
        {
            var playerTanks = new List<TankModel>();

            var requestParams = new NameValueCollection
            {
                { "application_id", "12845e99af9d4a7b3c734c0cbbb5ee12" },
                { "language", "en" },
                { "account_id", accountId }
            };

            var jsonResult = Request.PostRequest(UrlResolver.PlayersTanksUrl(region), requestParams);

            if (String.IsNullOrEmpty(jsonResult))
            {
                return playerTanks;
            }

            dynamic result = JsonConvert.DeserializeObject<dynamic>(jsonResult);
            foreach (dynamic player in result.data)
            {
                foreach (dynamic tank in player.Value)
                {
                    if (tanks.TryGetValue(tank.tank_id.ToString(), out string tankName))
                    {
                        var battles = tank.statistics.battles.Value;
                        var wins = tank.statistics.wins.Value;
                        var badge = (Constants.Badge)tank.mark_of_mastery.Value;
                        tanksMastery.TryGetValue(tank.tank_id.ToString(), out List<string> mastery);

                        var tankModel = TankExtensions.Create(tankName, battles, wins, badge, mastery);
                        playerTanks.Add(tankModel);
                    }
                }
            }
            return playerTanks;
        }

        public static Dictionary<string, List<string>> GetAllTanksMastery(Region region)
        {
            var tanks = new Dictionary<string, List<string>>();

            var jsonResult = Request.GetRequest(UrlResolver.TanksMasteryListUrl(region));
            if (String.IsNullOrEmpty(jsonResult))
            {
                return tanks;
            }

            dynamic result = JsonConvert.DeserializeObject<dynamic>(jsonResult);
            foreach (dynamic tank in result.data)
            {
                var mastery = new List<string>();
                foreach (dynamic value in tank.mastery)
                {
                    mastery.Add(value.ToString());
                }
                tanks.Add(tank.id.ToString(), mastery);
            }
            return tanks;
        }

        public static ObservableCollection<KeyValuePair<long, double>> StatsGetChartData(TankModel tank)
            => StatsGetChartData(tank.BattleCount, tank.WinCount);

        public static ObservableCollection<KeyValuePair<long, double>> StatsGetChartData(long battleCount, long winCount)
        {
            var chartData = new List<KeyValuePair<long, double>>();
            for (var sessionRatio = Constants.DesiredWinPercent + Constants.GraphStep;
                 sessionRatio < Constants.MaxWinPercent;
                 sessionRatio += Constants.GraphStep)
            {
                var battleCountToDesiredRatio = Computer.GetBattleCountToDesiredRatio(battleCount
                    , winCount, Constants.DesiredWinPercent, sessionRatio);
                chartData.Add(new KeyValuePair<long, double>(battleCountToDesiredRatio, sessionRatio));
            }
            return new ObservableCollection<KeyValuePair<long, double>>(chartData.OrderByDescending(pair => pair.Value));
        }

        public static ObservableCollection<KeyValuePair<string, int>> MasteryGetChartData(TankModel tank)
             => MasteryGetChartData(tank.Mastery);

        public static ObservableCollection<KeyValuePair<string, int>> MasteryGetChartData(List<string> mastery)
        {
            var chartData = new List<KeyValuePair<string, int>>();

            List<int> sortedList = mastery.Select(int.Parse).OrderBy(value => value).ToList();

            int i = 0;
            for (var badge = Constants.Badge.Third; badge <= Constants.Badge.Master; badge++, i++)
            {
                chartData.Add(new KeyValuePair<string, int>(Enum.GetName(typeof(Constants.Badge), badge), sortedList[i]));
            }
            return new ObservableCollection<KeyValuePair<string, int>>(chartData);
        }
    }
}
