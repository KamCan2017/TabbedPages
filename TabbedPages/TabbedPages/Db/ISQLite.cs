using SQLite.Net;

namespace TabbedPages.Db
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
