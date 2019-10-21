using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WotStat;
using WotStatApi.Models;

namespace WotStatApi.Services
{
    public class StatServiceWrapper : IStatService
    {
        public async Task<IEnumerable<EstimationGraphPoint>> GetEstimationGraphData(long battleCount, long winCount)
        {
            return await Task.Run(() =>
            {
                return StatService.GetChartData(battleCount, winCount)
                    .Select(pair => new EstimationGraphPoint { WinsLeft = pair.Key, WinRatio = pair.Value });
            });
        }

        public async Task<IEnumerable<TankModel>> GetTanksAsync(string userName)
        {
            return await Task.Run(() => {

                var accountId = StatService.GetAccountIdByName(userName);
                // todo: cash this
                var tanks = StatService.GetAllTanks();

                return StatService.GetPlayersTanks(accountId, tanks);
            });
        }
    }
}
