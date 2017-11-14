using System;
using TabbedPages.Models;
using TabbedPages.ViewModels;
using Xamarin.Forms;

namespace TabbedPages
{
    public static class DeleteButtonBehavior
    {
        public static readonly BindableProperty AttachBehaviorProperty =
        BindableProperty.CreateAttached(
            "AttachBehavior",
            typeof(bool),
            typeof(DeleteButtonBehavior),
            false,
            propertyChanged: OnAttachBehaviorChanged);

        public static bool GetAttachBehavior(BindableObject view)
        {
            return (bool)view.GetValue(AttachBehaviorProperty);
        }

        public static void SetAttachBehavior(BindableObject view, bool value)
        {
            view.SetValue(AttachBehaviorProperty, value);
        }

        static void OnAttachBehaviorChanged(BindableObject view, object oldValue, object newValue)
        {
            var button = view as Button;
            if (button == null)
            {
                return;
            }

            bool attachBehavior = (bool)newValue;
            if (attachBehavior)
            {
                button.Clicked += Bindable_Clicked;
            }
            else
            {
                button.Clicked -= Bindable_Clicked;
            }
        }

        
        static async void Bindable_Clicked(object sender, EventArgs e)
        {
            var result = await Application.Current.MainPage.DisplayAlert(
                          "Delete Task",
                          "Delete the selected task?",
                          "Yes", "No");
            if (result)
            {
                var vm = (Application.Current.MainPage as NavigationPage).CurrentPage.BindingContext as MainPageViewModel;
                vm.GetEventAggregator()
                .GetEvent<RemoveTaskEvent>().Publish((sender as Button).BindingContext as TaskModel);
            }
        }
    }
}
