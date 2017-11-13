namespace TabbedPages.Models
{
    public class TaskModel
    {
        //[PrimaryKey, AutoIncrement]
        //public int ID { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsValid { get { return !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Description); } }
    }
}
