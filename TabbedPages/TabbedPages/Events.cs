using Prism.Events;
using TabbedPages.Models;

namespace TabbedPages
{
    public static class Events
    {
        public const string AddTaskEvent = "AddTaskEvent";
        public const string EditTaskEvent = "EditTaskEvent";
    }

    public class RemoveTaskEvent : PubSubEvent<TaskModel>
    {

    }

    public class EditTaskEvent : PubSubEvent<TaskModel>
    {

    }

    public class AddTaskEvent : PubSubEvent<TaskModel>
    {

    }

    public class CloneTaskEvent : PubSubEvent<TaskModel>
    {

    }

}
