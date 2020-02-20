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
                //var embed = new EmbedBuilder
                //{
                //    Title = "",
                //    Description = ""
                //};

                try
                {
                    await client.SendMessageAsync(text: giveaway.Url/*, embeds: new[] { embed.Build() }*/);
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
