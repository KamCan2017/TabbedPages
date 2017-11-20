using SQLite;
using SQLite.Net;
using System.Threading.Tasks;
using TabbedPages.Models;
using Xamarin.Forms;

namespace TabbedPages.Db
{
    public class DataBaseHelper
    {
        //private SQLiteAsyncConnection _database;

        //public DataBaseHelper()
        //{
        //    string path = DependencyService.Get<ISQLite>().GetConnectionPath();
        //   _database = new SQLiteAsyncConnection(path);
        //    _database.CreateTableAsync<TaskModel>();
        //}

        //public SQLiteConnection GetConnection()
        //{
        //    SQLiteConnection sqlitConnection;
        //    var sqliteFilename = "Employee.db3";
        //    IFolder folder = FileSystem.Current.LocalStorage;
        //    string path = PortablePath.Combine(folder.Path.ToString(), sqliteFilename);
        //    sqlitConnection = new SQLite.SQLiteConnection(path);
        //    return sqlitConnection;
        //}

        //public async Task<string> InsertUpdateData(TaskModel data)
        //{
        //    try
        //    {
        //        if (await _database.InsertAsync(data) != 0)
        //            await _database.UpdateAsync(data);
        //        return "Single data file inserted or updated";
        //    }
        //    catch (SQLiteException ex)
        //    {
        //        return ex.Message;
        //    }
        //}

        //public async Task<bool> DeleteData(TaskModel data)
        //{
        //    var result = await _database.DeleteAsync(data);
        //    return result == 1;
        //}
    }
}
