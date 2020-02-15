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
        private readonly IDiscordService _discordService;

        public GiveawayController(IGiveawayService giveawayService, IDiscordService discordService)
        {
            _giveawayService = giveawayService;
            _discordService = discordService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Giveaway>), StatusCodes.Status200OK)]
        public async Task<List<Giveaway>> Get()
        {
            var giveaways = await _giveawayService.GetGiveaways();
            await _discordService.Send("");

            return giveaways;
        }
    }
}
