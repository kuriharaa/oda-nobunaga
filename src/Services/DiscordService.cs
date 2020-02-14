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
        public async Task Send()
        {
            using (var client = new DiscordWebhookClient(""))
            {
                var embed = new EmbedBuilder
                {
                    Title = "Test Embed",
                    Description = "Test Description"
                };

                await client.SendMessageAsync(text: "Send a message to this webhook!", embeds: new[] { embed.Build() });
            }
        }
    }
}
