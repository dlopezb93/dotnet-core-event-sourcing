using Amazon.DynamoDBv2.DataModel;
using System;

namespace User.Infrastructure.Data.Models
{
    [DynamoDBTable("events")]
    public class EventStream
    {
        public EventStream()
        {
        }

        [DynamoDBHashKey]
        [DynamoDBProperty("StreamId")]
        public string StreamId { get; set; }

        [DynamoDBRangeKey]
        [DynamoDBProperty("EventNumber")]
        public long EventNumber { get; set; }

        [DynamoDBProperty("EventType")]
        public string EventType { get; set; }

        [DynamoDBProperty("CreateAtUTC")]
        public DateTime CreateAt { get; set; }

        [DynamoDBProperty("Payload")]
        public string Payload { get; set; }
    }
}
