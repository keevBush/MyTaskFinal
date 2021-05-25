using System;
using LiteDB;

namespace MyTask.Models
{
    public class Step
    {
        [BsonId]
        [BsonField("id")]
        public string Id { get; set; }
        [BsonField("name")]
        public string Name { get; set; }
        [BsonField("deadline")]
        private DateTime? Deadline { get; set; }
        [BsonField("remember_date")]
        private DateTime? RememberDate { get; set; }
        [BsonField("repeat")]
        private bool? Repeat { get; set; }

        public Step()
        {
            Id = Guid.NewGuid().ToString();
            Name = "New step";
        }
        
        [BsonRef("Tasks")]
        public Task Task { get; set; }
    }
}