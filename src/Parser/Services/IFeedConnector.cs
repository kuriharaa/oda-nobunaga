using GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Parser
{
    public interface IFeedConnector
    {
        Task<List<Giveaway>> ParseFeed(string resource);
    }
}
