using TabbedPages.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ToDoPage : ContentPage
	{
		public ToDoPage ()
		{
			InitializeComponent ();
            var vm = new ToDoPageViewModel(Navigation);
            BindingContext = vm;
		}          
    }
}