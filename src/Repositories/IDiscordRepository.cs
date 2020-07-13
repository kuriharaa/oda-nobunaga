using GiveawayFreeSteamBot.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveawayFreeSteamBot.Repositories
{
    public interface IDiscordRepository
    {
        IMongoCollection<Channel> GetChannelCollection();
        Task<List<Channel>> GetChannels();
        IFindFluent<Channel, Channel> GetChannelEntry(IMongoCollection<Channel> collection, Channel channel);
    }
}
