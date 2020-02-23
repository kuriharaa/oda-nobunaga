using Discord;
using Discord.Webhook;
using GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Models;
using GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Services
{
    public class DiscordService : IDiscordService
    {
        private readonly IGiveawayRepository _giveawayRepository;
        private readonly IConfiguration _configuration;
        public DiscordService(IGiveawayRepository giveawayRepository, IConfiguration configuration)
        {
            _giveawayRepository = giveawayRepository;
            _configuration = configuration;
        }

        public async Task Send(Giveaway giveaway)
        {
            using (var client = new DiscordWebhookClient(MongoConfig.webhook))
            {
                var embed = new EmbedBuilder
                {
                    Title = giveaway.Title,
                    Color = Color.Magenta,
                    ImageUrl = giveaway.PhotoUrl,
                    Url = string.IsNullOrEmpty(giveaway.DirectUrl) ? giveaway.Url : giveaway.DirectUrl,
                    Author = new EmbedAuthorBuilder().WithName(giveaway.Store)
                };

                try
                {
                    await client.SendMessageAsync(text: $"@everyone \n its free real estate", embeds: new[] { embed.Build() }, isTTS: true);
                }
                catch(Exception e)
                {
                    throw new DiscordSendException($"failed to send giveaway {giveaway.ExternalId}", e);
                }
                finally
                {
                    await _giveawayRepository.UpdateStatus(giveaway);
                }
            }
        }
    }
}
