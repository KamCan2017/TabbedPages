using System.IO;
using TabbedPages.Db;
using Windows.Storage;

namespace TabbedPages.UWP
{
    public class SQLiteWinPhone : ISQLite
    {
        public SQLiteWinPhone()
        {
        }

        #region ISQLite implementation

        public string GetConnectionPath()
        {
            var fileName = "ToDoDb.db3";
            var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, fileName);

            return path;
        }

        #endregion
    }

}
