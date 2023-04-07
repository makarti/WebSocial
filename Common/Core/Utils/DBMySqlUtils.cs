using MySqlConnector;

namespace Core.Utils;

public class DBMySqlUtils
{
    public static MySqlConnection GetDBConnection(string host, string database, string password)
    {
        string connString = $"Server={host};Database={database};port=3306;user=root;password={password}";

        MySqlConnection conn = new MySqlConnection(connString);
        
        return conn;
    }
    public static MySqlConnection GetDBConnection(string host, string password)
    {
        string connString = $"Server={host};port=3306;user=root;password={password}";

        MySqlConnection conn = new MySqlConnection(connString);
        
        return conn;
    }
}