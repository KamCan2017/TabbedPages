using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabbedPages.Daos;
using Xamarin.Forms;

namespace TabbedPages
{
    public class TaskRepository : ITaskRepository
    {
        private SQLiteConnection _database;

        private IEnumerable<TaskDao> Tasks => _database.Table<TaskDao>().ToList();

        public TaskRepository()
        {
            _database = DependencyService.Get<Db.ISQLite>().GetConnection();
            _database.CreateTable<TaskDao>();
        }

        public async Task<bool> DeleteToDoItemAsync(string id)
        {
            var dao = await FindByIdAsync(id);
            int res = _database.Delete(dao);
            return res == 1;
        }

        public async Task<IEnumerable<TaskDao>> FindAllAsync()
        {
            var tasks = await Task.Run(() => {
                return Tasks.Where(p => !p.IsDeleted);
            });

            return tasks;
        }

        public async Task<TaskDao> FindByIdAsync(string id)
        {
            var dao = await Task.Run(() =>
            {
                return Tasks.FirstOrDefault(d => d.ID == Guid.Parse(id));
            });

            return dao;
        }

       public async Task<IEnumerable<TaskDao>> FindDeletedItemsAsync()
        {
            var tasks = await Task.Run(() => {
                return Tasks.Where(p => p.IsDeleted);
            });

            return tasks;
        }

        public async Task<TaskDao> SaveToDoItemAsync(TaskDao item)
        {
            var dao = await Task.Run(() =>
            {

                if (item.ID == Guid.Empty)
                {
                    item.ID = Guid.NewGuid();
                    _database.Insert(item);
                }

                else
                {
                    _database.Update(item);
                }

                return Tasks.FirstOrDefault(d => d.ID == item.ID);
            });

            return dao;
        }

    }
}
