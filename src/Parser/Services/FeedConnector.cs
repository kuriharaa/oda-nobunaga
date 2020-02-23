using GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Models;
using GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Parser.Exceptions;
using GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Repositories;
using HtmlAgilityPack;
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
                    string feedItemUrl = feedParser.GetItemLink(feedItemHtml);
                    HtmlDocument innerHtml = await _feedProvider.GetFeedHtml(feedItemUrl);
                    //html decode
                    var feedItem = new Giveaway
                    {
                        ExternalId = feedParser.GetItemId(feedItemHtml),
                        Url = feedItemUrl,
                        Sent = false,
                        PhotoUrl = feedParser.GetItemPhotoUrl(feedItemHtml),
                        Store = feedParser.GetItemStore(feedItemHtml),
                        Title = feedParser.GetItemTitle(feedItemHtml),
                        DirectUrl = feedParser.GetItemDirectUrl(innerHtml)
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
