using SQLite;
using System.Threading.Tasks;
using TabbedPages.Models;
using Xamarin.Forms;

namespace TabbedPages.Db
{
    public class TodoItemDatabase
    {
        private SQLiteAsyncConnection _database;

        public TodoItemDatabase()
        {
            string path = DependencyService.Get<ISQLite>().GetConnectionPath();
           _database = new SQLiteAsyncConnection(path);
            _database.CreateTableAsync<TaskModel>();
        }

        public async Task<string> InsertUpdateData(TaskModel data)
        {
            try
            {
                if (await _database.InsertAsync(data) != 0)
                    await _database.UpdateAsync(data);
                return "Single data file inserted or updated";
            }
            catch (SQLiteException ex)
            {
                return ex.Message;
            }
        }

        public async Task<bool> DeleteData(TaskModel data)
        {
            var result = await _database.DeleteAsync(data);
            return result == 1;
        }
    }
}
