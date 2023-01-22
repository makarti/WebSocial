using MySql.Data.MySqlClient;

namespace Core.Utils;

public class DBUtils
{
    public static MySqlConnection GetDBConnection()
    {
        string host = "localhost";
        int port = 3306;
        string database = "WebSocial";
        string username = "root";
        string password = "Password123#@!";

        return DBMySqlUtils.GetDBConnection(host, port, database, username, password);        
    }
}
