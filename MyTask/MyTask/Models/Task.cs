using System;
using System.Collections.Generic;
using LiteDB;

namespace MyTask.Models
{
    public class Task
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime Deadline { get; set; }
        public List<string> Labels { get; set; }

        //[BsonRef("Steps")]
        public virtual List<Step> Steps { get; set; }

        public Task()
        {
            CreatedAt = DateTime.Now;
            Deadline = DateTime.MaxValue;
            Labels = new List<string>();
        }
    }
}