using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TabbedPages.Daos;
using TabbedPages.Models;

namespace TabbedPages.ViewModels
{
    public class SchedulePageViewModel: ViewModelBase, IPageLoaderViewModel
    {
        private ObservableCollection<ScheduleModel> _schedules;
        private readonly ITaskAPiService _taskAPiService;
        private bool _isBusy;
        private DelegateCommand _loadPageCommand;

        public SchedulePageViewModel(INavigationService navigationService, IEventAggregator eventAggregator,
             ITaskAPiService taskAPiService) :
            base(navigationService, eventAggregator)
        {
            _taskAPiService = taskAPiService;
            _loadPageCommand = new DelegateCommand(async () => await LoadData());
        }

        public DelegateCommand LoadCommand { get { return _loadPageCommand; } }

        public ObservableCollection<ScheduleModel> Schedules
        {
            get { return _schedules; }
            set
            {
                SetProperty(ref _schedules, value);
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

        private async Task LoadData()
        {
            IsBusy = true;
            var tasks = await _taskAPiService.FindAllAsync();
            var schedules = new List<ScheduleModel>();
            foreach(TaskDao task in tasks)
            {
                var obj = schedules.FirstOrDefault(p => p.Start == task.Start && p.End == task.End);
                if (obj != null)
                    continue;
                ScheduleModel schedule = new ScheduleModel()
                {
                    Start = task.Start,
                    End = task.End
                };
                schedules.Add(schedule);
            }

            Schedules = new ObservableCollection<ScheduleModel>(schedules);
            IsBusy = false;
        }

        public async override void OnNavigatedTo(NavigationParameters parameters)
        {
            await LoadData();
        }
    }
}
