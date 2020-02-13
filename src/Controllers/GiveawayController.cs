using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Models;
using GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Parser;
using GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GiveawayFreeSteamBot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GiveawayController : ControllerBase
    {
        private readonly IGiveawayService _giveawayService;

        public GiveawayController(IGiveawayService giveawayService)
        {
            _giveawayService = giveawayService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Giveaway>), StatusCodes.Status200OK)]
        public async Task<List<Giveaway>> Get()
        {
            var giveaways = await _giveawayService.GetGiveaways();
            return giveaways;
        }
    }
}
