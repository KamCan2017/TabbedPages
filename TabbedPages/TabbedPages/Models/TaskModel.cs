using Prism.Mvvm;

namespace TabbedPages.Models
{
    public class TaskModel: BindableBase
    {
        private string _name;
        private string _description;

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

        public bool IsValid { get { return !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Description); } }
    }
}
