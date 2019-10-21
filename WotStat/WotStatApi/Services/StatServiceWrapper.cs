using System.Collections.Generic;
using System.Threading.Tasks;
using WotStat;

namespace WotStatApi.Services
{
    public class StatServiceWrapper : IStatService
    {
        public async Task<IEnumerable<TankModel>> GetTanksAsync(string userName)
        {
            return await Task.Run(() => {

                var accountId = WotStat.StatService.GetAccountIdByName(userName);
                // todo: cash this
                var tanks = WotStat.StatService.GetAllTanks();

                return WotStat.StatService.GetPlayersTanks(accountId, tanks);
            });
        }
    }
}
