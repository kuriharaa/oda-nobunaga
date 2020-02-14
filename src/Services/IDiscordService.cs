using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Services
{
    public interface IDiscordService
    {
        Task Send();
    }
}
