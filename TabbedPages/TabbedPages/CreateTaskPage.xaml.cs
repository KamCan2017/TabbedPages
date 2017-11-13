using TabbedPages.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreateTaskPage : ContentPage
	{
		public CreateTaskPage ()
		{
			InitializeComponent ();
            var vm = new CreateTaskPageViewModel(Navigation);
            BindingContext = vm;
		}  
    }
}