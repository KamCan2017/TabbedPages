using Prism.Events;
using Prism.Navigation;

namespace TabbedPages.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator) : base(navigationService, eventAggregator)
        {
        }

        public IEventAggregator GetEventAggregator()
        {
            return EventAggregator;
        }

        public INavigationService GetNavigationService()
        {
            return NavigationService;
        }
    }
}
