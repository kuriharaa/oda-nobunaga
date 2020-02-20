using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveawayFreeSteamBot.Models
{
    public class Config
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string name { get; set; }
        public string value { get; set; }
    }
}
