using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Models
{
    public class MongoDbContext<T>
    {
        private readonly IMongoDatabase _mongoDb;

        public MongoDbContext(string connectionString, string dbName)
        {
            var client = new MongoClient(connectionString);
            _mongoDb = client.GetDatabase(dbName);
        }

        public IMongoCollection<T> GetCollectionEntries(string collectionName)
        {
            return _mongoDb.GetCollection<T>(collectionName);
        }

    }
}
