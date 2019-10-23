using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WotStatApi.Services;

namespace WotStatApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        private readonly IStatService _statService;

        public StatController(IStatService statService)
        {
            _statService = statService;
        }

        [HttpGet]
        [Route("Tanks")]
        public async Task<ActionResult> GetTanksAsync(string userName)
        {
            try
            {
                var data = await _statService.GetTanksAsync(userName);
                return new JsonResult(data);
            }
            catch
            {
                return BadRequest($"Error retrieving data for '{userName}'");
            }            
        }

        [HttpGet]
        [Route("Estimation-Graph")]
        public async Task<ActionResult> GetEstimationGraphAsync(long battleCount, long winCount)
        {
            return new JsonResult(await _statService.GetEstimationGraphData(battleCount, winCount));
        }
    }
}