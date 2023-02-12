using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using WotStat.Extensions;
using WotStat.Models;
using WotStatService.Models;
using static System.Net.Mime.MediaTypeNames;
using static WotStat.Constants;

namespace WotStat
{
    public static class StatService
    {
        static readonly string applicationId = "f28611677e24bcfeb2b1c92bc689076e";
        static readonly Region defaultRegion = new Region { Name = "Europe", UrlSuffix = "eu" };

        public static string OpenIdLogin(Region region = null)
        {
            string address = null;

            var requestParams = new NameValueCollection
            {
                { "application_id", applicationId },
                { "expires_at", "3600" },
                { "nofollow", "1"}
            };

            var jsonResult = Request.PostRequest(UrlResolver.OpenIdLogin(region ?? defaultRegion), requestParams);

            if (String.IsNullOrEmpty(jsonResult))
            {
                return address;
            }

            dynamic result = JsonConvert.DeserializeObject<dynamic>(jsonResult);
            if (result.status.Value.Equals("ok"))
            {
                address = result.data.location;
            }

            return address;
        }

        public static bool Logout(string accessToken, Region region = null)
        {
            var requestParams = new NameValueCollection
            {
                { "application_id", applicationId },
                { "access_token", accessToken }
            };

            var jsonResult = Request.PostRequest(UrlResolver.LogoutUrl(region ?? defaultRegion), requestParams);

            if (String.IsNullOrEmpty(jsonResult))
            {
                return false;
            }

            dynamic result = JsonConvert.DeserializeObject<dynamic>(jsonResult);
            if (result.status.Value.Equals("ok"))
            {
                return true;
            }

            return false;
        }

        public static string GetAccountIdByName(string name, Region region = null)
        {
            string accountId = null;

            var requestParams = new NameValueCollection
            {
                { "application_id", applicationId },
                { "search", name },
                { "limit", "1" }
            };

            var jsonResult = Request.PostRequest(UrlResolver.AccountListUrl(region ?? defaultRegion), requestParams);

            if (String.IsNullOrEmpty(jsonResult))
            {
                return accountId;
            }

            dynamic result = JsonConvert.DeserializeObject<dynamic>(jsonResult);
            if (result.status.Value.Equals("ok") && result.data.Count != 0)
            {
                accountId = result.data.Last.account_id;
            }

            return accountId;
        }

        public static Dictionary<string, bool> GetPlayerTanks(string accountId, string accessToken, Region region = null)
        {
            var tanks = new Dictionary<string, bool>();

            var requestParams = new NameValueCollection
            {
                { "application_id", applicationId },
                { "account_id", accountId },
                { "access_token", accessToken },
                { "extra", "private.garage, private.rented" },
                { "fields", "private.garage, private.rented" },
                { "language", "en" }
            };

            var jsonResult = Request.PostRequest(UrlResolver.AccountInfo(region ?? defaultRegion), requestParams);
            if (String.IsNullOrEmpty(jsonResult))
            {
                return tanks;
            }

            dynamic result = JsonConvert.DeserializeObject<dynamic>(jsonResult);
            if (result.status.Value.Equals("ok"))
            {
                foreach (dynamic player in result.data)
                {
                    foreach (dynamic privateRecord in player.Value)
                    {
                        foreach (dynamic rentedTank in privateRecord.Value.rented)
                        {
                            tanks.Add(rentedTank.Name, true);
                        }
                        foreach (dynamic garageTank in privateRecord.Value.garage)
                        {
                            if (!tanks.ContainsKey(garageTank.Value.ToString()))
                            {
                                tanks.Add(garageTank.Value.ToString(), false);
                            }
                        }
                    }
                }
            }

            return tanks;
        }

        public static Dictionary<string, string> GetAllTanks(Region region = null)
        {
            var tanks = new Dictionary<string, string>();

            var requestParams = new NameValueCollection
            {
                { "application_id", applicationId },
                { "fields", "short_name, tank_id" },
                { "language", "en" }
            };

            var jsonResult = Request.PostRequest(UrlResolver.TanksListUrl(region ?? defaultRegion), requestParams);
            if (String.IsNullOrEmpty(jsonResult))
            {
                return tanks;
            }

            dynamic result = JsonConvert.DeserializeObject<dynamic>(jsonResult);
            if (result.status.Value.Equals("ok"))
            {
                foreach (dynamic tank in result.data)
                {
                    dynamic value = tank.Value;
                    tanks.Add(value.tank_id.ToString(), value.short_name.ToString());
                }
            }
            return tanks;
        }

        public static List<TankModel> GetPlayerTanksStats(string accountId,
                                                          Dictionary<string, string> tanks,
                                                          Dictionary<string, List<string>> tanksMasteryStats,
                                                          Dictionary<string, bool> playerGarageTanks,
                                                          Region region = null)
        {
            var playerTanks = new List<TankModel>();

            var requestParams = new NameValueCollection
            {
                { "application_id", applicationId },
                { "account_id", accountId },
                { "language", "en" }
            };

            var jsonResult = Request.PostRequest(UrlResolver.PlayersTanksUrl(region ?? defaultRegion), requestParams);

            if (String.IsNullOrEmpty(jsonResult))
            {
                return playerTanks;
            }

            dynamic result = JsonConvert.DeserializeObject<dynamic>(jsonResult);
            if (result.status.Value.Equals("ok"))
            {
                foreach (dynamic player in result.data)
                {
                    foreach (dynamic tank in player.Value)
                    {
                        if (tanks.TryGetValue(tank.tank_id.ToString(), out string tankName))
                        {
                            var battles = tank.statistics.battles.Value;
                            var wins = tank.statistics.wins.Value;
                            var badge = (Constants.Badge)tank.mark_of_mastery.Value;
                            tanksMasteryStats.TryGetValue(tank.tank_id.ToString(), out List<string> masteryStats);

                            var tankModel = TankExtensions.Create(tankName, battles, wins, badge);
                            tankModel.MasteryStats = masteryStats;
                            playerTanks.Add(tankModel);

                            if (playerGarageTanks != null && playerGarageTanks.ContainsKey(tank.tank_id.ToString()))
                            {
                                playerGarageTanks.Remove(tank.tank_id.ToString());
                            }
                        }
                    }
                }

                if (playerGarageTanks != null)
                {
                    foreach (var tank in playerGarageTanks)
                    {
                        if (tanks.TryGetValue(tank.Key, out string tankName))
                        {
                            if (tank.Value)
                            {
                                tankName = $"{tankName} (Rent)";
                            }
                            tanksMasteryStats.TryGetValue(tank.Key, out List<string> masteryStats);

                            var tankModel = TankExtensions.Create(tankName, 0, 0, Constants.Badge.None);
                            tankModel.MasteryStats = masteryStats;
                            playerTanks.Add(tankModel);
                        }
                    }
                }
            }
            return playerTanks;
        }

        public static List<TankModel> GetPlayerTanksAchievements(string accountId,
                                                                 Dictionary<string, string> tanks,
                                                                 Dictionary<string, List<KeyValuePair<string, string>>> tanksGunMarksStats,
                                                                 Region region = null)
        {
            var playerTankAchievements = new List<TankModel>();

            var requestParams = new NameValueCollection
            {
                { "application_id", applicationId },
                { "account_id", accountId },
                { "fields", "achievements, tank_id" },
                { "language", "en" }
            };

            var jsonResult = Request.PostRequest(UrlResolver.PlayerTanksAchievementsUrl(region ?? defaultRegion), requestParams);

            if (String.IsNullOrEmpty(jsonResult))
            {
                return playerTankAchievements;
            }

            dynamic result = JsonConvert.DeserializeObject<dynamic>(jsonResult);
            if (result.status.Value.Equals("ok"))
            {
                foreach (dynamic player in result.data)
                {
                    foreach (dynamic tank in player.Value)
                    {
                        if (tanks.TryGetValue(tank.tank_id.ToString(), out string tankName))
                        {
                            var badge = tank.achievements?.markOfMastery?.Value ?? 0;
                            var gunMarks = tank.achievements?.marksOnGun?.Value ?? 0;

                            tanksGunMarksStats.TryGetValue(tank.tank_id.ToString(), out List<KeyValuePair<string, string>> tankGunMarksStats);

                            var tankModel = TankExtensions.Create(tankName, 0, 0, (Constants.Badge)badge);
                            tankModel.GunMarks = (int)gunMarks;
                            tankModel.GunMarksStats = tankGunMarksStats;
                            playerTankAchievements.Add(tankModel);
                        }
                    }
                }
            }
            return playerTankAchievements;
        }

        public static Dictionary<string, List<string>> GetAllTanksMasteryStats(Region region = null)
        {
            var tanks = new Dictionary<string, List<string>>();

            var jsonResult = Request.GetRequest(UrlResolver.TanksMasteryListUrl(region ?? defaultRegion));
            if (String.IsNullOrEmpty(jsonResult))
            {
                return tanks;
            }

            dynamic result = JsonConvert.DeserializeObject<dynamic>(jsonResult);
            if (result.status.Value.Equals("ok"))
            {
                foreach (dynamic tank in result.data)
                {
                    var mastery = new List<string>();
                    foreach (dynamic value in tank.mastery)
                    {
                        mastery.Add(value.ToString());
                    }
                    tanks.Add(tank.id.ToString(), mastery);
                }
            }
            return tanks;
        }

        public static Dictionary<string, List<KeyValuePair<string, string>>> GetAllTanksGunMarksStats(Region region = null)
        {
            var tanks = new Dictionary<string, List<KeyValuePair<string, string>>>();

            var jsonResult = Request.GetRequest(UrlResolver.TanksGunMarksListUrl(region ?? defaultRegion));
            if (String.IsNullOrEmpty(jsonResult))
            {
                return tanks;
            }

            dynamic result = JsonConvert.DeserializeObject<dynamic>(jsonResult);
            if (result.status.Value.Equals("ok"))
            {
                foreach (dynamic tank in result.data)
                {
                    var marks = new List<KeyValuePair<string, string>>();
                    foreach (dynamic mark in tank.marks)
                    {
                        marks.Add(new KeyValuePair<string, string>(mark.Name.ToString(), mark.Value.ToString()));
                    }
                    tanks.Add(tank.id.ToString(), marks);
                }
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
             => MasteryGetChartData(tank.MasteryStats);

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

        public static ObservableCollection<KeyValuePair<string, int>> GunMarksGetChartData(TankModel tank)
            => GunMarksGetChartData(tank.GunMarksStats);

        public static ObservableCollection<KeyValuePair<string, int>> GunMarksGetChartData(List<KeyValuePair<string, string>> gunMarks)
        {
            var chartData = new List<KeyValuePair<string, int>>();

            foreach(var gunMark in gunMarks)
            {
                chartData.Add(new KeyValuePair<string, int>(gunMark.Key, int.Parse(gunMark.Value)));
            }
            return new ObservableCollection<KeyValuePair<string, int>>(chartData);
        }
    }
}
