using SQLite.Net;
using System.IO;
using TabbedPages.Db;
using TabbedPages.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(SQLiteDroid))]
namespace TabbedPages.Droid
{
    public class SQLiteDroid : ISQLite
    {
        public SQLiteDroid()
        {
        }


        #region ISQLite implementation

        public SQLiteConnection GetConnection()
        {
            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.
              SpecialFolder.Personal), Const.PhoneDataBaseName);

            var platformAndroid = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
            return new SQLiteConnection(platformAndroid, path);
        }


        #endregion
    }
}