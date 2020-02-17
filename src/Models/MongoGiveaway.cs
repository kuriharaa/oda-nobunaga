using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Models
{
    public class MongoGiveaway
    {
        public IMongoCollection<Giveaway> collection { get; set; }
        public FilterDefinition<Giveaway> filter { get; set; }
        public IFindFluent<Giveaway, Giveaway> entry { get; set; }
    }
}
