using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using System;
using TabbedPages.Models;
using Xamarin.Forms;

namespace TabbedPages.ViewModels
{
    public class CreateTaskPageViewModel: ViewModelBase, IDisposable
    {
        private DelegateCommand _saveTaskCommand;
        private DelegateCommand _goBackCommand;
        private TaskModel _model;

        public CreateTaskPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator) :
            base(navigationService, eventAggregator)
        {
            _saveTaskCommand = new DelegateCommand(SaveTask);
            _goBackCommand = new DelegateCommand(async () => { await NavigationService.GoBackAsync(); });
            Model = new TaskModel();
        }

        ~CreateTaskPageViewModel()
        {
            Dispose();
        }


        public DelegateCommand SaveTaskCommand { get { return _saveTaskCommand; } }

        public DelegateCommand GoBackCommand { get { return _goBackCommand; } }

        public TaskModel Model
        {
            get { return _model; }
            set
            {
                _model = value;
                SetProperty(ref _model, value, nameof(Model));
            }
        }


        private void SaveTask()
        {
            if (Model.IsValid)
            {

                MessagingCenter.Send(Model, Events.ToDoTaskEvent);
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
