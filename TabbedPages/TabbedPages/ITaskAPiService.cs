using System.Collections.Generic;
using System.Threading.Tasks;
using TabbedPages.Daos;

namespace TabbedPages
{
    public interface ITaskAPiService
    {
        Task<TaskDao> SaveToDoItemAsync(TaskDao item);

        Task<IEnumerable<TaskDao>> FindAllAsync();
    }
}