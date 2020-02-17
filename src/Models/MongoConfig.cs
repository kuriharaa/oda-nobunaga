using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Models
{
    public class MongoConfig
    {
        public string connectionString { get; set; }
        public string dbName { get; set; }
        public string collectionName { get; set; }

    }
}
