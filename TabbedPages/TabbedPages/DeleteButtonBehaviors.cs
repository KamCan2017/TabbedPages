using System;
using TabbedPages.Models;
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

        
        static void Bindable_Clicked(object sender, EventArgs e)
        {            
            MessagingCenter.Send((sender as Button).BindingContext as TaskModel, Events.RemoveTaskEvent);
        }
    }
}
