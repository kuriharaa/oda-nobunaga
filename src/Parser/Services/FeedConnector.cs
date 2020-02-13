using GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Models;
using GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Parser.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Parser
{
    public class FeedConnector : IFeedConnector
    {
        private readonly IFeedProvider _feedProvider;

        public FeedConnector(IFeedProvider feedProvider)
        {
            _feedProvider = feedProvider;
        }

        public async Task<List<Giveaway>> ParseFeed(string resource)
        {
            try
            {
                var feedHtml = await _feedProvider.GetFeedHtml(resource);
                var feedParser = new FeedParser();
                var feedItemsHtml = feedParser.GetItems(feedHtml);
                var feedItemsList = new List<Giveaway>();

                foreach (var feedItemHtml in feedItemsHtml)
                {
                    var feedItem = new Giveaway
                    {
                        Id = feedParser.GetItemId(feedItemHtml),
                        Url = feedParser.GetItemLink(feedItemHtml),
                        Status = feedParser.GetItemStatus(feedItemHtml)
                    };

                    feedItemsList.Add(feedItem);
                }

                return feedItemsList;
            }
            catch (Exception e)
            {
                throw new FeedParsingException("failed to parse html", e);
            }
        }

    }
}
