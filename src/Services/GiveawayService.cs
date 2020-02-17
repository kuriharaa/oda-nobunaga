using GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Models;
using GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Parser;
using GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Services
{
    public class GiveawayService : IGiveawayService
    {
        private readonly IFeedConnector _feedConnector;
        private readonly IConfiguration _configuration;
        private readonly IGiveawayRepository _giveawayRepository;

        public GiveawayService(IFeedConnector feedConnector,
                               IConfiguration configuration,
                               IGiveawayRepository giveawayRepository)
        {
            _feedConnector = feedConnector;
            _configuration = configuration;
            _giveawayRepository = giveawayRepository;
        }

        public async Task<List<Giveaway>> GetGiveaways()
        {
            var giveaways = await _feedConnector.ParseFeed(_configuration["url:feed"]);
            foreach (Giveaway giveaway in giveaways)
            {
                await _giveawayRepository.AddOrSkip(giveaway);
            }
            return await _giveawayRepository.GetActive();
        }
    }
}
