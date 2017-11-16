using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TabbedPages.Mappper;
using TabbedPages.Models;

namespace TabbedPages.ViewModels
{
    public class ToDoPageViewModel : ViewModelBase, IPageLoaderViewModel
    {
        private ObservableCollection<TaskModel> _tasks;
        private DelegateCommand _goToCreateTaskPageCommand;
        private readonly ITaskAPiService _taskAPiService;
        private ITaskMapper _taskMapper = new TaskMapper();
        private bool _isBusy;
        private DelegateCommand _refreshTaskPageCommand;
        private DelegateCommand _loadPageCommand;
        private ScheduleModel _selectedSchedule;

        public ToDoPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator,
             ITaskAPiService taskAPiService) :
            base(navigationService, eventAggregator)
        {
            _taskAPiService = taskAPiService;

            _goToCreateTaskPageCommand = new DelegateCommand(async () => 
            {
                await NavigationService.NavigateAsync("CreateTaskPage");
            });

            _refreshTaskPageCommand = new DelegateCommand(async () =>
            {
                _selectedSchedule = null;
                await LoadData();
            });
            _loadPageCommand = new DelegateCommand(async () => await LoadData());

            EventAggregator.GetEvent<AddTaskEvent>().Subscribe(item => AddTask(item));
            EventAggregator.GetEvent<RemoveTaskEvent>().Subscribe(async (item) => 
            {
                    await RemoveTask(item);
            }
            );
            EventAggregator.GetEvent<EditTaskEvent>().Subscribe(async (item) =>
            {
                await NavigateToEditPage(item);
            }
            );

            EventAggregator.GetEvent<CloneTaskEvent>().Subscribe(async (item) =>
            {
                IsBusy = true;
                var cloneObj = _taskMapper.Convert(item);
                cloneObj.Name += "_cloned";
                cloneObj.ID = Guid.Empty;
                await _taskAPiService.SaveToDoItemAsync(cloneObj);
                await LoadData();
            }
           );

            eventAggregator.GetEvent<OpenScheduleEvent>().Subscribe( (item) =>
            {
                _selectedSchedule = item;
            });
        }


        public DelegateCommand GoToCreateTaskPageCommand { get { return _goToCreateTaskPageCommand; } }

        public DelegateCommand RefreshTaskPageCommand { get { return _refreshTaskPageCommand; } }

        public DelegateCommand LoadCommand { get { return _loadPageCommand; } }


        public ObservableCollection<TaskModel> Tasks
        {
            get { return _tasks; }
            set
            {
                SetProperty(ref _tasks, value);
            }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                SetProperty(ref _isBusy, value);
            }
        }

        private async Task NavigateToEditPage(TaskModel item)
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add(Events.EditTaskEvent, item);

            await NavigationService.NavigateAsync("CreateTaskPage", parameters);
        }

        private void AddTask(TaskModel item)
        {
            if(!Tasks.Contains(item))
               Tasks.Add(item);
        }   
        
        private async Task RemoveTask(TaskModel item)
        {
            IsBusy = true;
            await _taskAPiService.DeleteToDoItemAsync(item.ID.ToString());
            await LoadData();
            IsBusy = false;
        }

        private async Task LoadData()
        {
            IsBusy = true;
            var tasks = await _taskAPiService.FindAllAsync();
            Tasks = new ObservableCollection<TaskModel>(_taskMapper.Convert(tasks));

            if (_selectedSchedule != null)
            {
                var relatedTasks = Tasks.Where(p => p.Start == _selectedSchedule.Start && p.End == _selectedSchedule.End);
                Tasks = new ObservableCollection<TaskModel>(relatedTasks);
            }
            IsBusy = false;
        }

        public  override void OnNavigatedTo(NavigationParameters parameters)
        {
            //await LoadData();
        }
    }
}
