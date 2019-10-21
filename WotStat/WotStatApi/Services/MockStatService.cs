﻿using System.Collections.Generic;
using System.Threading.Tasks;
using WotStat;
using WotStat.Extensions;
using static WotStat.Constants;

namespace WotStatApi.Services
{
    public class MockStatService : IStatService
    {
        public async Task<IEnumerable<TankModel>> GetTanksAsync(string userName)
        {
            return await Task.Run(() => {
                return new List<TankModel>
                {
                    TankExtensions.Create("Set Chill", 1377, 420, Badge.Third),
                    TankExtensions.Create("KB69", 134, 69, Badge.Second),
                    TankExtensions.Create("Matilda", 666, 333, Badge.Master),
                };
            });
        }
    }
}
