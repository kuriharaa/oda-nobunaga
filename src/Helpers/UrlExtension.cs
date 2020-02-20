using GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Models;
using GiveawayFreeSteamBot.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveawayFreeSteamBot
{
    public static class UrlExtension
    {
        public static string GetUrl(string name)
        {
            MongoDbContext<Config> _context = new MongoDbContext<Config>(MongoConfig.connectionString, MongoConfig.dbName);
            IMongoCollection<Config> collection = _context.GetCollectionEntries(MongoConfig.config);

            return collection.Find(x => x.name.Equals(name))
                             .First()
                             .value;
        }
    }
}
