using GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Models;
using GiveawayFreeSteamBot.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveawayFreeSteamBot.Repositories
{
    public class DiscordRepository : IDiscordRepository
    {
        public IMongoCollection<Channel> GetChannelCollection()
        {
            MongoDbContext<Channel> _context = new MongoDbContext<Channel>(MongoConfig.connectionString, MongoConfig.dbName);
            return _context.GetCollectionEntries(MongoConfig.channels);
        }

        public IFindFluent<Channel, Channel> GetChannelEntry(IMongoCollection<Channel> collection, Channel channel)
        {
            FilterDefinition<Channel> filter;
            filter = Builders<Channel>.Filter.Eq(s => s.value, channel.value);

            return collection.Find(filter);
        }

        public async Task<List<Channel>> GetChannels()
        {
            IMongoCollection<Channel> collection = GetChannelCollection();
            return await(await collection.FindAsync(_ => true)).ToListAsync();
        }
    }
}
