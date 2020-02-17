using GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Repositories
{
    public interface IGiveawayRepository
    {
        public Task<List<Giveaway>> GetActive();
        public MongoGiveaway GetMongoGiveaway(object val);
        public Task AddOrSkip(Giveaway giveaway);
        public Task Add(IMongoCollection<Giveaway> collection, Giveaway giveaway);
        public Task UpdateStatus(Giveaway feed);
    }
}
