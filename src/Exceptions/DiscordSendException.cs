using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveawayFreeSteamBot.GiveawayDiscordNotifier.src
{
    public class DiscordSendException : Exception
    {
        public DiscordSendException() { }
        public DiscordSendException(string message) : base(message) { }
        public DiscordSendException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
