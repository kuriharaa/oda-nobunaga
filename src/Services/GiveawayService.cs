using GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Models;
using GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Parser;
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

        public GiveawayService(IFeedConnector feedConnector, IConfiguration configuration)
        {
            _feedConnector = feedConnector;
            _configuration = configuration;
        }

        public async Task<List<Giveaway>> GetGiveaways()
        {
            return await _feedConnector.ParseFeed(_configuration["urls:feed"]);
        }
    }
}
