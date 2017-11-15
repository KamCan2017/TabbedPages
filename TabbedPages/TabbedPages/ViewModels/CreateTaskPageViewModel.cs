using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using System;
using System.Threading.Tasks;
using TabbedPages.Daos;
using TabbedPages.Mappper;
using TabbedPages.Models;
using Xamarin.Forms;

namespace TabbedPages.ViewModels
{
    public class CreateTaskPageViewModel: ViewModelBase, IDisposable
    {
        private DelegateCommand _saveTaskCommand;
        private DelegateCommand _goBackCommand;
        private DelegateCommand _removeTaskCommand;
        private TaskModel _model;
        private readonly ITaskAPiService _taskAPiService;
        private ITaskMapper _taskMapper = new TaskMapper();

        public CreateTaskPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator,
            ITaskAPiService taskAPiService) :
            base(navigationService, eventAggregator)
        {
            _taskAPiService = taskAPiService;
            _saveTaskCommand = new DelegateCommand(async () => await SaveTask());
            _goBackCommand = new DelegateCommand(async () => { await NavigationService.GoBackAsync(); });
            _removeTaskCommand = new DelegateCommand(async () => 
            {
                if (Model.IsValid)
                {
                    var result = await Application.Current.MainPage.DisplayAlert(
                                 "Delete Task",
                                 "Delete the selected task?",
                                 "Yes", "No");
                    if (result)
                    {
                        await _taskAPiService.DeleteToDoItemAsync(Model.ID.ToString());
                        GoBackCommand.Execute();
                    }
                }
            });
            Model = new TaskModel();
        }

        ~CreateTaskPageViewModel()
        {
            Dispose();
        }


        public DelegateCommand SaveTaskCommand { get { return _saveTaskCommand; } }

        public DelegateCommand GoBackCommand { get { return _goBackCommand; } }

        public DelegateCommand RemoveTaskCommand { get { return _removeTaskCommand; } }

        public TaskModel Model
        {
            get { return _model; }
            set
            {
                SetProperty(ref _model, value, nameof(Model));
                if(_model != null)
                    _model.PropertyChanged += _model_PropertyChanged;
            }
        }


        private void _model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(Model.Start) || e.PropertyName == nameof(Model.End))
            {
                if(Model.Start > Model.End)
                {
                   Application.Current.MainPage.DisplayAlert(
                                  "Error",
                                  "End  date must be equal to or greater than Start date",
                                  "cancel");
                }
            }
        }

        private async Task SaveTask()
        {
            if (Model.IsValid)
            {
                TaskDao dao = _taskMapper.Convert(Model);

                var obj = await _taskAPiService.SaveToDoItemAsync(dao);
                GoBackCommand.Execute();
            }
        }


        public void Dispose()
        {
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            TaskModel model;
            parameters.TryGetValue(Events.EditTaskEvent, out model);
            if(model != null)
               Model = model;
        }

    }
}
