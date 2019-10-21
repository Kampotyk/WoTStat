using System.Collections.Generic;
using System.Threading.Tasks;
using WotStat;

namespace WotStatApi.Services
{
    public interface IStatService
    {
        Task<IEnumerable<TankModel>> GetTanksAsync(string userName);
    }
}
