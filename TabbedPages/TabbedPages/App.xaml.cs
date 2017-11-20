using Microsoft.Practices.Unity;
using Prism.Unity;
using TabbedPages.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TabbedPages
{
    public partial class App : PrismApplication
    {
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<ToDoPage>();
            Container.RegisterTypeForNavigation<CreateTaskPage>();
            Container.RegisterTypeForNavigation<SchedulePage>();

            //Container.RegisterType<ITaskService, TaskAPiService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ITaskService, TaskSQLiteService>(new ContainerControlledLifetimeManager());
        }
    }
}
