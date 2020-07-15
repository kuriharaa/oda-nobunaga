using Discord;
using Discord.Webhook;
using GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Models;
using GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Repositories;
using GiveawayFreeSteamBot.Models;
using GiveawayFreeSteamBot.Repositories;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Services
{
    public class DiscordService : IDiscordService
    {
        private readonly IGiveawayRepository _giveawayRepository;
        private readonly IDiscordRepository _discordRepository;
        private readonly IConfiguration _configuration;
        public DiscordService(IGiveawayRepository giveawayRepository, IDiscordRepository discordRepository, IConfiguration configuration)
        {
            _giveawayRepository = giveawayRepository;
            _discordRepository = discordRepository;
            _configuration = configuration;
        }

        public async Task<List<Channel>> GetChannels()
        {
            return await _discordRepository.GetChannels();
        }

        public async Task Add(Channel channel)
        {
            IMongoCollection<Channel> collection = _discordRepository.GetChannelCollection();
            IFindFluent<Channel, Channel> entry = _discordRepository.GetChannelEntry(collection, channel);
            if (!entry.Any())
            {
                try
                {
                    await collection.InsertOneAsync(channel);
                }
                catch (Exception e)
                {
                    throw new MongoStoreException($"fatal error happened when adding channel data to mongodb", e);
                }
            }
        }

        public async Task Send(Giveaway giveaway, string webhook)
        {
            using (var client = new DiscordWebhookClient(webhook))
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
                    await client.SendMessageAsync(
                                                    text: $"@everyone \n its free real estate \n add notifier to your [discord server](http://nobunaga.surge.sh)", 
                                                    embeds: new[] { embed.Build() }, 
                                                    isTTS: true
                    );
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
