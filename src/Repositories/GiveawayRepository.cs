using GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Models;
using GiveawayFreeSteamBot.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Repositories
{
    public class GiveawayRepository : IGiveawayRepository
    {
        //private readonly IOptions<MongoConfig> _mongoConfig;
        //public GiveawayRepository(IOptions<MongoConfig> mongoConfig)
        //{
        //    _mongoConfig = mongoConfig;
        //}

        public async Task AddOrSkip(Giveaway giveaway)
        {
            MongoGiveaway mongoGiveaway = GetMongoGiveaway(giveaway.ExternalId);

            if (!mongoGiveaway.entry.Any())
            {
                await Add(mongoGiveaway.collection, giveaway);
            }
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

        public async Task UpdateStatus(Giveaway giveaway)
        {
            MongoGiveaway mongoGiveaway = GetMongoGiveaway(giveaway.ExternalId);

            UpdateDefinition<Giveaway> update = Builders<Giveaway>.Update
                .Set(s => s.Url, giveaway.Url)
                .Set(s => s.Sent, true);

            try
            {
                await mongoGiveaway.collection.UpdateOneAsync(mongoGiveaway.filter, update);
            }
            catch (Exception e)
            {
                throw new MongoStoreException($"Fatal error happened when updating data to mongodb {giveaway.ExternalId}", e);
            }
        }

        public async Task<List<Giveaway>> GetActive()
        {
            return await GetMongoGiveaway(false)
                         .entry
                         .ToListAsync();
        }

        public MongoGiveaway GetMongoGiveaway(object val)
        {
            FilterDefinition<Giveaway> filter;
            MongoDbContext<Giveaway> _context = new MongoDbContext<Giveaway>(MongoConfig.connectionString, MongoConfig.dbName);
            IMongoCollection<Giveaway> collection = _context.GetCollectionEntries(MongoConfig.collection);
            if (val is bool)
                filter = Builders<Giveaway>.Filter.Eq(s => s.Sent, (bool)val);
            else
                filter = Builders<Giveaway>.Filter.Eq(s => s.ExternalId, (string)val);
            IFindFluent<Giveaway, Giveaway> entry = collection.Find(filter);
            return new MongoGiveaway
            {
                collection = collection,
                entry = entry,
                filter = filter
            };
        }
    }
}
