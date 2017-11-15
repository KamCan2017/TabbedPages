using System.Collections.Generic;
using TabbedPages.Daos;
using TabbedPages.Models;

namespace TabbedPages.Mappper
{
    public interface ITaskMapper
    {
        TaskModel Convert(TaskDao dao);

        TaskDao Convert(TaskModel model);

        IEnumerable<TaskDao> Convert(IEnumerable<TaskModel> model);

        IEnumerable<TaskModel> Convert(IEnumerable<TaskDao> daos);
    }
}