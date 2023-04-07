using Core.Entities;
using Dapper;
using MoreLinq;
using MySqlConnector;

namespace Core.Utils;

public class DBUtils
{
    private static string _host;
    private static string _database;
    private static string _password;

    static DBUtils()
    {
        _host = Environment.GetEnvironmentVariable("DB_HOST");
        _database = Environment.GetEnvironmentVariable("DB_NAME");
        _password = Environment.GetEnvironmentVariable("DB_ROOT_PASSWORD");

        Initialize();
    }  

    public static MySqlConnection GetDBConnection()
    {
        return DBMySqlUtils.GetDBConnection(_host, _database, _password);        
    }

    #region initialize db

    private static void Initialize()
    {
        InitializeDatabase();
        InitializeTables();
        AccountGenerate();
    }

    private static void InitializeDatabase()
    {
        const string sql = @"CREATE DATABASE IF NOT EXISTS `WebSocial`;";

        using (var connection = DBMySqlUtils.GetDBConnection(_host, _password))
        {
            connection.Execute(sql);
        }
    }

    private static void InitializeTables()
    {
        const string sqltables = @"
                            CREATE TABLE if not exists WebSocial.`Account` (
                              `Id` varchar(36) NOT NULL,
                              `Login` varchar(45) NOT NULL,
                              `Password` varchar(100) NOT NULL,
                              `FirstName` varchar(45) NOT NULL,
                              `LastName` varchar(45) NOT NULL,
                              `Age` tinyint NOT NULL,
                              `Gender` tinyint(1) NOT NULL,
                              `Interests` varchar(255) NOT NULL,
                              `City` varchar(100) NOT NULL,
                              `CreateDate` datetime NOT NULL,
                              PRIMARY KEY (`Id`),
                              UNIQUE KEY `Id_UNIQUE` (`Id`),
                              UNIQUE KEY `Login_UNIQUE` (`Login`)
                            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

                            CREATE TABLE if not exists WebSocial.`Friendship` (
                              `RequesterId` varchar(36) NOT NULL,
                              `AddresserId` varchar(36) NOT NULL,
                              `Created` datetime NOT NULL,
                              `Status` tinyint(1) NOT NULL DEFAULT (0),
                              PRIMARY KEY (`RequesterId`,`AddresserId`)
                            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;";

        using (var connection = DBMySqlUtils.GetDBConnection(_host, _database, _password))
        {
            connection.Execute(sqltables);
        }
    }
    #endregion

    #region accountgenerate
    private static void AccountGenerate()
    {
        using (var connection = DBMySqlUtils.GetDBConnection(_host, _database, _password))
        {
            var count = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Account");
            if (count > 1000)
                return;
        }

        var _faker = new AccountFaker();
        var users = _faker.GetAccountGenerator().Generate(1000000);//1000000

        const string sql =
            @"insert into Account (Id, Login, Password, FirstName, LastName, Age, Gender, Interests, City, CreateDate) 
                  values (@Id, @Login, @Password, @FirstName, @LastName, @Age, @Gender, @Interests, @City, @CreateDate);";


        foreach (var batch in users.Batch(500))
            AddUsers(batch.ToList());

        void AddUsers(List<Account> users)
        {
            using (var connection = GetDBConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    foreach (var user in users)
                        connection.Execute(sql, user, transaction);
                    transaction.Commit();
                }
            }
        }
    }
    #endregion 
}
