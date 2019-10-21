using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WotStatApi.Services;

namespace WotStatApi.Controllers
{
    [Route("api/[controller]")]
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
            return new JsonResult(await _statService.GetTanksAsync(userName));
        }
    }
}