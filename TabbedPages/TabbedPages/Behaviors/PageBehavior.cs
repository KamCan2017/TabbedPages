using System;
using TabbedPages.ViewModels;
using Xamarin.Forms;

namespace TabbedPages.Behaviors
{
    public class PageBehavior: Behavior<ContentPage>
    {
        private IPageLoaderViewModel _vm;
        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.Appearing += Bindable_Appearing;
            _vm = bindable.BindingContext as IPageLoaderViewModel;
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.Appearing -= Bindable_Appearing;
        }

        private void Bindable_Appearing(object sender, EventArgs e)
        {
            if (_vm != null)
            {
                _vm.LoadCommand.Execute();
            }
        }
    }
}
