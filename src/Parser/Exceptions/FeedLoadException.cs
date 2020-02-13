using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Parser.Exceptions
{
    public class FeedLoadException : Exception
    {
        public FeedLoadException() { }
        public FeedLoadException(string message) : base(message) { }
        public FeedLoadException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
