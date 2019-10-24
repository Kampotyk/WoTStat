﻿using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WotStat;
using WotStat.Models;
using WotStatApi.Models;
using WotStatService.Models;

namespace WotStatApi.Services
{
    public class StatServiceWrapper : IStatService
    {
        private IMemoryCache _cache;

        public StatServiceWrapper(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<IEnumerable<EstimationGraphPoint>> GetEstimationGraphData(long battleCount, long winCount)
        {
            return await Task.Run(() =>
            {
                return StatService.GetChartData(battleCount, winCount)
                    .Select(pair => new EstimationGraphPoint { WinsLeft = pair.Key, WinRatio = pair.Value });
            });
        }

        public async Task<IEnumerable<TankModel>> GetTanksAsync(string userName, Region region)
        {
            return await Task.Run(async () => {

                var accountId = StatService.GetAccountIdByName(userName, region);

                var tanks = await _cache.GetOrCreateAsync("tanks", entry => 
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1);
                    return Task.Run(() => { return StatService.GetAllTanks(region); });
                });

                return StatService.GetPlayersTanks(accountId, tanks, region);
            });
        }
    }
}
