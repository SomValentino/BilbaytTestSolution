using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bilbayt.Domain.Interfaces;

namespace Bilbayt.Domain
{
    public class ApplicationUser : IModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRequired]
        public string Username { get; set; }
        [BsonRequired]
        public string Email { get; set; }
        [BsonRequired]
        public string Password { get; set; }
        [BsonRequired]
        public string FullName { get; set; }
    }
}
