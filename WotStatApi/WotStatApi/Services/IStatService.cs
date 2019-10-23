using System.Collections.Generic;
using System.Threading.Tasks;
using WotStat;
using WotStatApi.Models;

namespace WotStatApi.Services
{
    public interface IStatService
    {
        Task<IEnumerable<TankModel>> GetTanksAsync(string userName);
        Task<IEnumerable<EstimationGraphPoint>> GetEstimationGraphData(long battleCount, long winCount);
    }
}
