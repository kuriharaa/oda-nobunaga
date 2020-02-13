using GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Parser.Exceptions;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Parser
{
    public class FeedProvider : IFeedProvider
    {
        public async Task<HtmlDocument> GetFeedHtml(string feedUrl)
        {
            try
            {
                var web = new HtmlWeb();
                var doc = await web.LoadFromWebAsync(feedUrl);
                return doc;
            }
            catch (Exception e)
            {
                throw new FeedLoadException($"Failed to load html {feedUrl} source", e);
            }
        }
    }
}
