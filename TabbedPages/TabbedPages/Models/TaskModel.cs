using Prism.Mvvm;
using System;

namespace TabbedPages.Models
{
    public class TaskModel: BindableBase
    {
        private string _name;
        private string _description;
        private DateTime _start;
        private DateTime _end;

        public TaskModel()
        {
            Start = DateTime.Now;
            End = Start.AddDays(30);
        }

        public Guid ID { get; set; }
        public string Name
        {
            get { return _name; }
            set
            {
                SetProperty(ref _name, value, nameof(Name));
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                SetProperty(ref _description, value, nameof(Description));
            }
        }

        public DateTime Start
        {
            get { return _start; }
            set
            {
                SetProperty(ref _start, value);
            }
        }

        public DateTime End
        {
            get { return _end; }
            set
            {
                SetProperty(ref _end, value);
            }
        }

        public bool IsValid {
            get
            {
                return !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Description)
                       && Start <= End;
            }
        }

        public bool IsDeleted { get; set; }
    }
}
