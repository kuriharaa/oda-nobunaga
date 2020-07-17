using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Parser
{
    public class FeedParser : IFeedParser
    {
        public string GetItemDirectUrl(HtmlDocument html)
        {
            return html.DocumentNode
                       .SelectSingleNode("//div[@class='entry-content']")?
                       .Descendants()?
                       .ToList()
                       .Where(n => n.InnerText.Contains("Страница раздачи:"))?
                       .FirstOrDefault()?
                       .LastChild?
                       .Attributes["href"]?
                       .Value;
        }

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

        public string GetItemPhotoUrl(HtmlNode html)
        {
            return html.ChildNodes
                       .FindFirst("div")?
                       .ChildNodes
                       .FindFirst("a")?
                       .ChildNodes
                       .FindFirst("img")?
                       .Attributes["data-src"]?
                       .Value;
        }

        public List<HtmlNode> GetItems(HtmlDocument html)
        {
            return html.DocumentNode
                       .SelectNodes("//article")
                       .Where(c =>
                       c.InnerHtml.Contains("/active/") ||
                       c.InnerHtml.Contains("/interesnoe/")).ToList();
        }

        public string GetItemStore(HtmlNode html)
        {
            return html.ChildNodes
                       .FindFirst("div")?
                       .ChildNodes
                       .FindFirst("header")?
                       .ChildNodes
                       .FindFirst("div")?
                       .ChildNodes
                       .FindFirst("span")?
                       .ChildNodes
                       .FindFirst("a")?
                       .InnerText;
        }

        public string GetItemTitle(HtmlNode html)
        {
            return html.ChildNodes
                       .FindFirst("div")?
                       .ChildNodes
                       .FindFirst("header")?
                       .ChildNodes
                       .FindFirst("h2")?
                       .ChildNodes
                       .FindFirst("a")?
                       .InnerText;
        }
    }
}
