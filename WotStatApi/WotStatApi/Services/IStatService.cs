using System.Collections.Generic;
using System.Threading.Tasks;
using WotStat.Models;
using WotStatApi.Models;
using WotStatService.Models;

namespace WotStatApi.Services
{
    public interface IStatService
    {
        Task<IEnumerable<TankModel>> GetTanksAsync(string userName, Region region);
        Task<IEnumerable<EstimationGraphPoint>> GetEstimationGraphData(long battleCount, long winCount);
    }
}
