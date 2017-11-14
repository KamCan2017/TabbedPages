using Prism.Events;
using TabbedPages.Models;

namespace TabbedPages
{
    public static class Events
    {
        public const string ToDoTaskEvent = "ToDoTaskEvent";
        public const string EditTaskEvent = "EditTaskEvent";
    }

    public class RemoveTaskEvent : PubSubEvent<TaskModel>
    {

    }

    public class EditTaskEvent : PubSubEvent<TaskModel>
    {

    }

}
