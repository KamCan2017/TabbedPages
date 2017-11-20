using System.Collections.Generic;
using System.Threading.Tasks;
using TabbedPages.Daos;

namespace TabbedPages
{
    public interface ITaskService
    {
        Task<TaskDao> SaveToDoItemAsync(TaskDao item);

        Task<IEnumerable<TaskDao>> FindAllAsync();

        Task<TaskDao> FindByIdAsync(string id);

        Task<bool> DeleteToDoItemAsync(string id);

        Task<IEnumerable<TaskDao>> FindDeletedItemsAsync();

    }
}