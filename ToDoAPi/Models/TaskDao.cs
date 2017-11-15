using System;

namespace ToDoAPi.Models
{
    public class TaskDao
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

    }
}