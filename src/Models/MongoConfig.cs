using GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveawayFreeSteamBot.GiveawayDiscordNotifier.src.Models
{
    public class MongoConfig
    {
        public const string connectionString = "mongodb+srv://admin:kEpMPxedaf99AN$@cluster0-1fa7r.azure.mongodb.net/test?retryWrites=true&w=majority";
        public const string dbName = "FreeGiveaways";
        public const string collection = "Giveaways";
        public const string config = "Config";
        public const string channels = "Channels";
        public static string feedUrl = UrlExtension.GetUrl("feedUrl");
        public static string webhook = "https://discord.com/api/webhooks";
        public static string webhookOld = "https://discordapp.com/api/webhooks";

    }
}
