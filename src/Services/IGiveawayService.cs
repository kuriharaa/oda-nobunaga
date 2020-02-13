using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Models;

namespace GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Services
{
    public interface IGiveawayService
    {
        public Task<List<Giveaway>> GetGiveaways();
    }
}
