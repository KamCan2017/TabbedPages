using SQLite.Net;
using System.IO;
using TabbedPages.Db;
using TabbedPages.UWP;
using Windows.Storage;

[assembly: Xamarin.Forms.Dependency(typeof(SQLiteWinPhone))]
namespace TabbedPages.UWP
{
    public class SQLiteWinPhone : ISQLite
    {
        public SQLiteWinPhone()
        {
        }

        #region ISQLite implementation

        public SQLiteConnection GetConnection()
        {
            var path = Path.Combine(ApplicationData.
              Current.LocalFolder.Path, Const.PhoneDataBaseName);

            var platformWindowsPhone = new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT();
            
            return new SQLiteConnection(platformWindowsPhone, path);
        }
        #endregion
    }

}
