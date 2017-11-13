using Prism.Commands;
using System.Collections.ObjectModel;
using TabbedPages.Models;
using Xamarin.Forms;

namespace TabbedPages.ViewModels
{
    public class ToDoPageViewModel
    {
        private ObservableCollection<TaskModel> _tasks;
        private DelegateCommand _goToCreateTaskPageCommand;
        private readonly INavigation _navigation;
        public ToDoPageViewModel(INavigation navigation)
        {
            _navigation = navigation;

            MessagingCenter.Subscribe<TaskModel>(this, Events.ToDoTaskEvent, item => AddTask(item));

            _goToCreateTaskPageCommand = new DelegateCommand(async () => 
            {
                await _navigation.PushAsync(new CreateTaskPage());
            });


            MessagingCenter.Subscribe<TaskModel>(this, Events.RemoveTaskEvent, item => RemoveTask(item));

            Tasks = new ObservableCollection<TaskModel>();
        }

        public DelegateCommand GoToCreateTaskPageCommand { get { return _goToCreateTaskPageCommand; } }


        public ObservableCollection<TaskModel> Tasks
        {
            get { return _tasks; }
            set
            {
                _tasks = value;
            }
        }

        private void AddTask(TaskModel item)
        {
            Tasks.Add(item);
        }   
        
        private void RemoveTask(TaskModel item)
        {
            Tasks.Remove(item);
        }
    }
}
