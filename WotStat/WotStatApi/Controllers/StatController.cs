using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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