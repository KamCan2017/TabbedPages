using Prism.Commands;
using System;
using System.ComponentModel;
using TabbedPages.Models;
using Xamarin.Forms;

namespace TabbedPages.ViewModels
{
    public class CreateTaskPageViewModel: INotifyPropertyChanged, IDisposable
    {
        private readonly INavigation _navigation;
        private DelegateCommand _saveTaskCommand;
        private DelegateCommand _goBackCommand;
        private TaskModel _model;

        public CreateTaskPageViewModel(INavigation navigation)
        {
            _navigation = navigation;
            _saveTaskCommand = new DelegateCommand(SaveTask);
            _goBackCommand = new DelegateCommand(async () => { await _navigation.PopAsync(); });
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
                NotifyChange(nameof(Model));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void SaveTask()
        {
            if (Model.IsValid)
            {

                MessagingCenter.Send(Model, Events.ToDoTaskEvent);
                GoBackCommand.Execute();
            }
        }

        private void NotifyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Dispose()
        {
        }
    }
}
