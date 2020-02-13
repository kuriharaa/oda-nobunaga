using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GiveawayFreeSteamBot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GiveawayController : ControllerBase
    {
        private readonly ILogger<GiveawayController> _logger;

        public GiveawayController(ILogger<GiveawayController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public OkResult Get()
        {
            return Ok();
        }
    }
}
