using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Models;
using GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Parser;
using GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Services;
using GiveawayFreeSteamBot.Helpers;
using GiveawayFreeSteamBot.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GiveawayFreeSteamBot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GiveawayController : ControllerBase
    {
        private IHttpContextAccessor _accessor;
        private readonly IGiveawayService _giveawayService;
        private readonly IDiscordService _discordService;

        public GiveawayController(IHttpContextAccessor accessor, IGiveawayService giveawayService, IDiscordService discordService)
        {
            _giveawayService = giveawayService;
            _discordService = discordService;
            _accessor = accessor;
        }

        [HttpPost("a")]
        public async Task<ActionResult> AddChannel(string url)
        {
            if (!string.IsNullOrEmpty(url) && url.StartsWith(MongoConfig.webhook) && WebhookChecker.IsWebhook(url))
            {
                var ip = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
                await _discordService.Add(new Channel() { name = ip, value = Regex.Replace(url, @"\s+", "") });

                return StatusCode(201);
            }
            return NotFound("wrong webhook url");
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Giveaway>), StatusCodes.Status200OK)]
        public async Task<List<Giveaway>> Get()
        {
            var channels = await _discordService.GetChannels();
            var giveaways = await _giveawayService.GetGiveaways();

            foreach (var giveaway in giveaways)
            {
                foreach (var channel in channels)
                {
                    await _discordService.Send(giveaway, channel.value);
                }
            }

            return giveaways;
        }
    }
}
