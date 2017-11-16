using Prism.Commands;
using Prism.Mvvm;
using System;
using Prism.Unity;
using Prism.Navigation;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace TabbedPages.Models
{
    public class ScheduleModel: BindableBase
    {
        private DelegateCommand _openToDoPageCommand;
        public ScheduleModel()
        {
            _openToDoPageCommand =  new DelegateCommand(() => 
            {
                var eventAggregator = ((PrismApplication.Current as PrismApplication).Container as UnityContainer)
                .Resolve<IEventAggregator>();
                eventAggregator.GetEvent<OpenScheduleEvent>().Publish(this);
            });
        }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public DelegateCommand OpenToDoPageCommand
        {
            get  {  return _openToDoPageCommand;  }            
        }
    }
}
