using Core.Configuration;
using MySql.Data.MySqlClient;

namespace Core.Utils;

public class DBUtils
{
    public static MySqlConnection GetDBConnection()
    {
        string host = ConfigurationManager.AppSetting["ConnectionStrings:Host"];
        int port = int.Parse(ConfigurationManager.AppSetting["ConnectionStrings:Port"]);
        string database = ConfigurationManager.AppSetting["ConnectionStrings:Database"];
        string username = ConfigurationManager.AppSetting["ConnectionStrings:UserName"];
        string password = ConfigurationManager.AppSetting["ConnectionStrings:Password"];
        //string host = "localhost";
        //int port = 3306;
        //string database = "WebSocial";
        //string username = "root";
        //string password = "Password123#@!";

        return DBMySqlUtils.GetDBConnection(host, port, database, username, password);        
    }
}
