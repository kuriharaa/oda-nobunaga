using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Parser
{
    public interface IFeedProvider
    {
        Task<HtmlDocument> GetFeedHtml(string feedUrl);
    }
}
