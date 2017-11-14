using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using System.Collections.ObjectModel;
using TabbedPages.Models;
using Xamarin.Forms;

namespace TabbedPages.ViewModels
{
    public class ToDoPageViewModel: ViewModelBase
    {
        private ObservableCollection<TaskModel> _tasks;
        private DelegateCommand _goToCreateTaskPageCommand;

        public ToDoPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator):
            base(navigationService, eventAggregator)
        {

            MessagingCenter.Subscribe<TaskModel>(this, Events.ToDoTaskEvent, item => AddTask(item));

            _goToCreateTaskPageCommand = new DelegateCommand(async () => 
            {
                await NavigationService.NavigateAsync("CreateTaskPage");
            });

            EventAggregator.GetEvent<RemoveTaskEvent>().Subscribe(item => RemoveTask(item));
            EventAggregator.GetEvent<EditTaskEvent>().Subscribe(async (item) => 
            {
                NavigationParameters parameters = new NavigationParameters();
                parameters.Add(Events.EditTaskEvent, item);

                await NavigationService.NavigateAsync("CreateTaskPage", parameters);
            }
            );

            Tasks = new ObservableCollection<TaskModel>();
        }

        public DelegateCommand GoToCreateTaskPageCommand { get { return _goToCreateTaskPageCommand; } }


        public ObservableCollection<TaskModel> Tasks
        {
            get { return _tasks; }
            set
            {
                SetProperty(ref _tasks, value);
            }
        }
       
        private void AddTask(TaskModel item)
        {
            if(!Tasks.Contains(item))
               Tasks.Add(item);
        }   
        
        private void RemoveTask(TaskModel item)
        {
            Tasks.Remove(item);
        }
    }
}
