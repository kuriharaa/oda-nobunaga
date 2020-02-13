using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Parser.Exceptions
{
    public class FeedParsingException : Exception
    {
        public FeedParsingException() { }
        public FeedParsingException(string message) : base(message) { }
        public FeedParsingException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
