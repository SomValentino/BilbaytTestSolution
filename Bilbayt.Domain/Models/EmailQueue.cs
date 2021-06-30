using Bilbayt.Domain.Enums;
using Bilbayt.Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilbayt.Domain.Models
{
    public class EmailQueue : IModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public EmailStatus Status { get; set; }
        [BsonRequired]
        public string Subject { get; set; }
        [BsonRequired]
        public string ToAddresses { get; set; }
        [BsonRequired]
        public string FromAddress { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        [BsonRequired]
        public string Body { get; set; }
        public int SentAttempts { get; set; }
        [BsonRequired]
        public DateTime CreatedDate { get; set; }
    }
}
