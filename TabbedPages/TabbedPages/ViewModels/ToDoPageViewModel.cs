using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TabbedPages.Mappper;
using TabbedPages.Models;

namespace TabbedPages.ViewModels
{
    public class ToDoPageViewModel: ViewModelBase
    {
        private ObservableCollection<TaskModel> _tasks;
        private DelegateCommand _goToCreateTaskPageCommand;
        private readonly ITaskAPiService _taskAPiService;
        private ITaskMapper _taskMapper = new TaskMapper();


        public ToDoPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator,
             ITaskAPiService taskAPiService) :
            base(navigationService, eventAggregator)
        {
            _taskAPiService = taskAPiService;

            _goToCreateTaskPageCommand = new DelegateCommand(async () => 
            {
                await NavigationService.NavigateAsync("CreateTaskPage");
            });

            EventAggregator.GetEvent<AddTaskEvent>().Subscribe(item => AddTask(item));
            EventAggregator.GetEvent<RemoveTaskEvent>().Subscribe(async (item) => 
            {
                    await RemoveTask(item);
            }
            );
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
        
        private async Task RemoveTask(TaskModel item)
        {
            await _taskAPiService.DeleteToDoItemAsync(item.ID.ToString());
            await LoadData();
        }

        private async Task LoadData()
        {
            var tasks = await _taskAPiService.FindAllAsync();
            Tasks = new ObservableCollection<TaskModel>(_taskMapper.Convert(tasks));
        }

        public async override void OnNavigatedTo(NavigationParameters parameters)
        {
            await LoadData();
        }
    }
}
