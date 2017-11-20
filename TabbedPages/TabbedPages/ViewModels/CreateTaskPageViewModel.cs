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
        private DelegateCommand _deleteTaskCommand;
        private DelegateCommand _resetTaskCommand;
        private TaskModel _model;
        private readonly ITaskService _taskAPiService;
        private ITaskMapper _taskMapper = new TaskMapper();

        public CreateTaskPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator,
            ITaskService taskAPiService) :
            base(navigationService, eventAggregator)
        {
            _taskAPiService = taskAPiService;
            _saveTaskCommand = new DelegateCommand(async () => await SaveTask(), CanSaveExecuted);
            _goBackCommand = new DelegateCommand(async () => { await NavigationService.GoBackAsync(); });
            _deleteTaskCommand = new DelegateCommand(async () =>
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
            }, () => {return (Model != null && Model.IsValid); });

            _resetTaskCommand = new DelegateCommand(async() => 
            {
                var obj = await _taskAPiService.FindByIdAsync(Model.ID.ToString());
                if (obj == null)
                {
                    Model.Name = string.Empty;
                    Model.Description = string.Empty;
                    Model.Start = DateTime.Now;
                    Model.End = DateTime.Now.AddDays(30);
                }
                else
                    Model = _taskMapper.Convert(obj);
            }, () => { return Model != null; });
        }

        private bool CanSaveExecuted()
        {
            return Model != null && Model.IsValid;
        }

        ~CreateTaskPageViewModel()
        {
            Dispose();
        }


        public DelegateCommand SaveTaskCommand { get { return _saveTaskCommand; } }

        public DelegateCommand GoBackCommand { get { return _goBackCommand; } }

        public DelegateCommand DeleteTaskCommand { get { return _deleteTaskCommand; } }

        public DelegateCommand ResetTaskCommand { get { return _resetTaskCommand; } }

        public TaskModel Model
        {
            get { return _model; }
            set
            {
                SetProperty(ref _model, value, nameof(Model));
                if(_model != null)
                    _model.PropertyChanged += _model_PropertyChanged;
                SaveTaskCommand.RaiseCanExecuteChanged();
                DeleteTaskCommand.RaiseCanExecuteChanged();
                ResetTaskCommand.RaiseCanExecuteChanged();
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
            SaveTaskCommand.RaiseCanExecuteChanged();
            DeleteTaskCommand.RaiseCanExecuteChanged();
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
            if(parameters.TryGetValue(Events.EditTaskEvent, out model))
                Model = model;
            else
                Model = new TaskModel();
        }

    }
}
