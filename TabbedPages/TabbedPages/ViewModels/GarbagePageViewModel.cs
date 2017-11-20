using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TabbedPages.Mappper;
using TabbedPages.Models;
using Xamarin.Forms;

namespace TabbedPages.ViewModels
{
    public class GarbagePageViewModel: ViewModelBase, IPageLoaderViewModel
    {
        private ObservableCollection<TaskModel> _tasks;
        private readonly ITaskService _taskAPiService;
        private bool _isBusy;
        private DelegateCommand _loadPageCommand;
        private ITaskMapper _taskMapper;
        private TaskModel _selectedTask;

        public GarbagePageViewModel(INavigationService navigationService, IEventAggregator eventAggregator,
             ITaskService taskAPiService, ITaskMapper taskMapper) :
            base(navigationService, eventAggregator)
        {
            _taskAPiService = taskAPiService;
            _taskMapper = taskMapper;
            _loadPageCommand = new DelegateCommand(async () => await LoadData());
        }

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
        
        public TaskModel SelectedTask
        {
            get { return _selectedTask; }

            set
            {
                SetProperty(ref _selectedTask, value);
                if (_selectedTask != null)
                {
                    Task.Run(async () =>
                    {
                        var result = await Application.Current.MainPage.DisplayAlert(
                                        "Restore Task",
                                        "Restore the selected task?",
                                        "Yes", "No");

                        if (result)
                        {
                            IsBusy = true;
                            _selectedTask.IsDeleted = false;
                            await _taskAPiService.SaveToDoItemAsync(_taskMapper.Convert(_selectedTask));
                            await LoadData();
                        }

                    });
                }
            }
        }

        private async Task LoadData()
        {
            IsBusy = true;
            var tasks = await _taskAPiService.FindDeletedItemsAsync();
            Tasks = new ObservableCollection<TaskModel>(_taskMapper.Convert(tasks));
            IsBusy = false;
        }

        public async override void OnNavigatedTo(NavigationParameters parameters)
        {
            await LoadData();
        }
    }
}
