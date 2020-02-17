using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Parser
{
    public class FeedParser : IFeedParser
    {
        public string GetItemId(HtmlNode html)
        {
            return html.Attributes["id"]?.Value;
        }
        public string GetItemLink(HtmlNode html)
        {
            return html.ChildNodes
                       .FindFirst("div")?
                       .ChildNodes
                       .FindFirst("a")?
                       .Attributes["href"]?
                       .Value;
        }
        //public string GetItemStatus(HtmlNode html)
        //{
        //    return (html.ChildNodes
        //               .FindFirst("div")?
        //               .ChildNodes
        //               .FindFirst("header")?
        //               .ChildNodes
        //               .FindFirst("div")?
        //               .ChildNodes
        //               .FindFirst("span")?
        //               .InnerText?
        //               .Trim()
        //               .Split('/')
        //               .LastOrDefault()
        //               );
        //}
        public List<HtmlNode> GetItems(HtmlDocument html)
        {
            return html.DocumentNode
                       .SelectNodes("//article")
                       .Where(c =>
                       c.InnerHtml.Contains("/active/") ||
                       c.InnerHtml.Contains("/interesnoe/")).ToList();
        }
    }
}
