using GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Repositories
{
    interface IGiveawayRepository
    {
        public Task AddOrSkip(Giveaway giveaway);
        public Task Add(IMongoCollection<Giveaway> collection, Giveaway giveaway);
        public Task UpdateStatus(IMongoCollection<Giveaway> collection, Giveaway feed, FilterDefinition<Giveaway> filter);
    }
}
