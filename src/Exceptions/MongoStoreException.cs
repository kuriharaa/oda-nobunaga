using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Repositories
{
    public class MongoStoreException : Exception
    {
        public MongoStoreException() { }
        public MongoStoreException(string message) : base(message) { }
        public MongoStoreException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
