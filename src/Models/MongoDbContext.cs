using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Models
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _mongoDb;

        public MongoDbContext()
        {
            var client = new MongoClient("");
            _mongoDb = client.GetDatabase("");
        }

        public IMongoCollection<Giveaway> GetCollectionEntries(string name)
        {
            return _mongoDb.GetCollection<Giveaway>(name);
        }
    }
}
