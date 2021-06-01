using System;
using LiteDB;

namespace MyTask.Models
{
    public class Step
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime? RememberDate { get; set; }
        public bool Repeat { get; set; }

        public Step()
        {
            Deadline = DateTime.MaxValue;
            RememberDate = DateTime.MaxValue;
            Repeat = false;
        }
        
        [BsonRef("Tasks")]
        public virtual Task Task { get; set; }
    }
}