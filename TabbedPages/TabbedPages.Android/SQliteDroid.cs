using System.IO;
using TabbedPages.Db;

namespace TabbedPages.Droid
{
    public class SQLiteDroid : ISQLite
    {
        public SQLiteDroid()
        {
        }

        #region ISQLite implementation

        public string GetConnectionPath()
        {
            var fileName = "ToDoDb.db3";
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, fileName);
        }

        #endregion
    }
}