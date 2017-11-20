using System.Collections.Generic;
using System.Threading.Tasks;
using TabbedPages.Daos;

namespace TabbedPages
{
    public class TaskSQLiteService : ITaskService
    {

        private ITaskRepository _taskRepository;

        public TaskSQLiteService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<bool> DeleteToDoItemAsync(string id)
        {
            return await _taskRepository.DeleteToDoItemAsync(id);
        }

        public async Task<IEnumerable<TaskDao>> FindAllAsync()
        {
            return await _taskRepository.FindAllAsync();
        }

        public async Task<TaskDao> FindByIdAsync(string id)
        {
            return await _taskRepository.FindByIdAsync(id);
        }

        public async Task<TaskDao> SaveToDoItemAsync(TaskDao item)
        {
            return await _taskRepository.SaveToDoItemAsync(item);
        }
    }
}
