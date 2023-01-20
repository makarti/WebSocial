using MySql.Data.MySqlClient;

namespace Core.Utils;

public class DBMySqlUtils
{
    public static MySqlConnection GetDBConnection(string host, int port, string database, string username, string password)
    {
        string connString = $"Server=\"{host}\";Database=\"{database}\";port=\"{port}\";user=\"{username}\";password=\"{password}\"";

        MySqlConnection conn = new MySqlConnection(connString);
        
        return conn;
    }
}