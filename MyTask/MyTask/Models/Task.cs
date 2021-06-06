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
        public DateTime CreatedAt { get; set; }
        public DateTime Deadline { get; set; }
        public List<string> Labels { get; set; }

        public virtual List<Step> Steps { get; set; }

        public Task()
        {
            Deadline = DateTime.MaxValue;
            Labels = new List<string>();
        }
    }
}