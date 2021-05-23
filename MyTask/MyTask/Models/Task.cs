using System;
using System.Collections.Generic;
using LiteDB;

namespace MyTask.Models
{
    public class Task
    {
        [BsonId]
        [BsonField("id")]
        public string Id { get; set; }
        [BsonField("name")]
        public string Name { get; set; }
        [BsonField("description")]
        public string Description { get; set; }
        [BsonField("created_at")]
        public DateTime CreatedAt { get; private set; }
        [BsonField("deadline")]
        public DateTime? Deadline { get; set; } = null;

        public List<string> Labels;

        public Task()
        {
            CreatedAt = DateTime.Now;
            Labels = new List<string>();
        }
    }
}