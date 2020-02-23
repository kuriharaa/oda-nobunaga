using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Parser
{
    interface IFeedParser
    {
        string GetItemId(HtmlNode html);
        string GetItemLink(HtmlNode html);
        string GetItemTitle(HtmlNode html);
        string GetItemStore(HtmlNode html);
        string GetItemDirectUrl(HtmlDocument html);
        string GetItemPhotoUrl(HtmlNode html);
        List<HtmlNode> GetItems(HtmlDocument html);
    }
}
