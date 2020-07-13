using GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Models;
using GiveawayFreeSteamBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Services
{
    public interface IDiscordService
    {
        Task Send(Giveaway giveaway, string webhook);
        Task Add(Channel channel);
        Task<List<Channel>> GetChannels();
    }
}
