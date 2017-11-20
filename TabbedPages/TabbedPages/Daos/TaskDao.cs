using SQLite.Net.Attributes;
using System;

namespace TabbedPages.Daos
{
    [Table("Tasks")]
    public class TaskDao
    {
        [PrimaryKey, AutoIncrement]
        public Guid ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

    }

}
