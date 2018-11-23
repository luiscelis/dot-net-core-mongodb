using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators
;

namespace TodoApi.Models
{
    public class User
    {
        [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        public ObjectId InternalId { get; set; }

        public string Id { get; set; }

        public string Body { get; set; }

        [BsonDateTimeOptions]
        public DateTime updateTime { get; set; } = DateTime.Now;

        [BsonDateTimeOptions]
        public DateTime createTime { get; set; } = DateTime.Now;

        [BsonDateTimeOptions]
        // attribute to gain control on datetime serialization
        public DateTime UpdatedOn { get; set; } = DateTime.Now;

        public int UserId { get; set; }

    }
}
