using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Web.Models;
using System.Data.Common;
using Core.Utils;

namespace Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        ViewBag.UserName = GetUserName();
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private string GetUserName()
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        try
        {
            string sql = "Select Login from WebSocial.Account;"; 

            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();

            // Сочетать Command с Connection.
            cmd.Connection = conn;
            cmd.CommandText = sql;
            var username = string.Empty; 

            
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    
                    while (reader.Read())
                    {
                        // Индекс (index) столбца Emp_ID в команде SQL.
                        int index = reader.GetOrdinal("Login");
                        username = reader.GetValue(index).ToString();
                    }
                }
            }
            return username;
        }
        catch (Exception e)
        {
            return e.ToString();
        }
        finally
        {
            // Закрыть соединение.
            conn.Close();
            // Уничтожить объект, освободить ресурс.
            conn.Dispose();
        }
    }
}
