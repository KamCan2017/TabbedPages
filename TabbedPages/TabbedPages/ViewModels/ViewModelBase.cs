using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;

namespace TabbedPages.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }

        protected IEventAggregator EventAggregator { get; private set; }

        public ViewModelBase(INavigationService navigationService, IEventAggregator eventAggregator)
        {
            NavigationService = navigationService;
            EventAggregator = eventAggregator;
        }

        public virtual void Destroy()
        {
        }

        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public virtual  void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}
