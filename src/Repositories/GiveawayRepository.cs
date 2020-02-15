using GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Repositories
{
    public class GiveawayRepository : IGiveawayRepository
    {
        MongoDbContext _context = new MongoDbContext();
        public async Task AddOrSkip(Giveaway giveaway)
        {
            IMongoCollection<Giveaway> collection = _context.GetCollectionEntries("collection name");
            FilterDefinition<Giveaway> filter = Builders<Giveaway>.Filter.Eq(s => s.ExternalId, giveaway.ExternalId);
            var entry = collection.Find(filter);
            if (!entry.Any())
                await Add(collection, giveaway);
        }

        public async Task Add(IMongoCollection<Giveaway> collection, Giveaway giveaway)
        {
            try
            {
                await collection.InsertOneAsync(giveaway);
            }
            catch (Exception e)
            {
                throw new MongoStoreException($"fatal error happened when adding data to mongodb {giveaway.ExternalId}", e);
            }
        }

        public async Task UpdateStatus(IMongoCollection<Giveaway> collection, Giveaway giveaway, FilterDefinition<Giveaway> filter)
        {
            UpdateDefinition<Giveaway> update = Builders<Giveaway>.Update
                .Set(s => s.Url, giveaway.Url)
                .Set(s => s.Status, giveaway.Status);

            try
            {
                await collection.UpdateOneAsync(filter, update);
            }
            catch (Exception e)
            {
                throw new MongoStoreException($"Fatal error happened when updating data to mongodb {giveaway.ExternalId}", e);
            }
        }
    }
}
