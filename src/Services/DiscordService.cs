using Discord;
using Discord.Webhook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Services
{
    public class DiscordService : IDiscordService
    {
        public async Task Send(string url)
        {
            using (var client = new DiscordWebhookClient(""))
            {
                var embed = new EmbedBuilder
                {
                    Title = "",
                    Description = ""
                };

                await client.SendMessageAsync(text: url, embeds: new[] { embed.Build() });
            }
        }
    }
}
